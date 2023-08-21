using MachineInventory.Data;
using MachineInventory.DTO;
using MachineInventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MachineInventory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManufacturerController : ControllerBase
    {
        private readonly InventoryContext _context;

        public ManufacturerController(InventoryContext context)
        {
            _context = context;
        }

        //  Get all manufacturer
        [HttpGet]
        public async Task<ActionResult<List<ManufacturerResponseDTO>>> GetManufacturers()
        {
            var manufacturers = await _context.Manufacturers
                .Include(m => m.Machines)
                .Select(m => Manufacturer.ToResponse(m))
                .ToListAsync();

            return Ok(manufacturers);
        }

        //  Get specific manufacturer by name
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ManufacturerResponseDTO>> GetManufacturerById(int id)
        {
            var manufacturer = await _context.Manufacturers
                .Include(m => m.Machines)
                .Where(m => m.Id == id)
                .Select(m => Manufacturer.ToResponse(m))
                .FirstOrDefaultAsync();

            if (manufacturer == null)
            {
                return BadRequest($"Manufacturer with id: {id} does not exist!");
            }

            return Ok(manufacturer);
        }

        //  Create a new manufacturer
        [HttpPost]
        public async Task<ActionResult<ManufacturerResponseDTO>> AddManufacturer(
            ManufacturerRequestDTO request
        )
        {
            Manufacturer manufacturer;

            try
            {
                manufacturer = Manufacturer.FromRequest(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            _context.Manufacturers.Add(manufacturer).Collection(m => m.Machines).Load();
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetManufacturerById),
                new { id = manufacturer.Id },
                Manufacturer.ToResponse(manufacturer)
            );
        }

        //  Edit a manufacturer
        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditMachine(int id, ManufacturerRequestDTO request)
        {
            // Check if Manufacturer exists
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            // Check if request has valid properties
            Manufacturer requestedManufacturer;
            try
            {
                requestedManufacturer = Manufacturer.FromRequest(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            // Edit mfctrer based on request
            manufacturer.Name = requestedManufacturer.Name;
            manufacturer.Location = requestedManufacturer.Location;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //  Delete a specific manufacturer
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachine(int id)
        {
            // Check if manufacturer exists
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer == null)
            {
                return BadRequest($"Machine with id: {id} does not exist!");
            }

            _context.Manufacturers.Remove(manufacturer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
