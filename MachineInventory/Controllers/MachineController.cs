using MachineInventory.Data;
using MachineInventory.DTO;
using MachineInventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MachineInventory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MachineController : ControllerBase
    {
        private readonly InventoryContext _context;

        public MachineController(InventoryContext context)
        {
            _context = context;
        }

        //  Get all machines
        [HttpGet]
        public async Task<ActionResult<List<MachineResponseDTO>>> GetMachines()
        {
            var machines = await _context.Machines
                .Include(m => m.Manufacturer)
                .Select(m => Machine.ToResponse(m))
                .ToListAsync();

            return Ok(machines);
        }

        //  Get specific machine with id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MachineResponseDTO>> GetMachineById(int id)
        {
            var machine = await _context.Machines
                .Include(m => m.Manufacturer)
                .Where(m => m.Id == id)
                .Select(m => Machine.ToResponse(m))
                .FirstOrDefaultAsync();

            if (machine == null)
            {
                return BadRequest($"Machine with id: {id} does not exist!");
            }

            return Ok(machine);
        }

        //  Create a new machine
        [HttpPost]
        public async Task<ActionResult<MachineResponseDTO>> AddMachine(MachineRequestDTO request)
        {
            Machine machine;

            // Check if Manufacturer exists
            if (await _context.Manufacturers.FindAsync(request.ManufacturerId) == null)
            {
                return BadRequest("Manufacturer does not exist!");
            }

            // Create Machine entity
            try
            {
                machine = Machine.FromRequest(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            // Add entry to DB and explicit loading of Manufacturer reference
            _context.Machines.Add(machine).Reference(m => m.Manufacturer).Load();
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetMachineById),
                new { id = machine.Id },
                Machine.ToResponse(machine)
            );
        }

        //  Edit a machine
        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditMachine(int id, MachineRequestDTO request)
        {
            // Check if machine exists
            var machine = await _context.Machines.FindAsync(id);
            if (machine == null)
            {
                return NotFound();
            }

            // Check if Manufacturer exists
            if (await _context.Manufacturers.FindAsync(request.ManufacturerId) == null)
            {
                return BadRequest("Manufacturer does not exist!");
            }

            // Check if request has valid properties
            Machine requestedMachine;
            try
            {
                requestedMachine = Machine.FromRequest(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            // Edit machine based on request
            machine.Name = requestedMachine.Name;
            machine.Price = requestedMachine.Price;
            machine.LastMaintained = requestedMachine.LastMaintained;
            machine.ManufacturerId = requestedMachine.ManufacturerId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //  Delete a specific machine by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachine(int id)
        {
            // Check if machine exists
            var machine = await _context.Machines.FindAsync(id);
            if (machine == null)
            {
                return BadRequest($"Machine with id: {id} does not exist!");
            }

            _context.Machines.Remove(machine);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
