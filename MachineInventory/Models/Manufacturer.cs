using MachineInventory.DTO;

namespace MachineInventory.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public ICollection<Machine> Machines { get; set; }

        public Manufacturer(string name, string location)
        {
            // Can enforce constraints here
            Name = name;
            Location = location;
        }

        public static ManufacturerResponseDTO ToResponse(Manufacturer manufacturer)
        {
            return new ManufacturerResponseDTO
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name,
                Location = manufacturer.Location,
                Machines = manufacturer.Machines.ToDictionary(m => m.Id, m => m.Name)
            };
        }

        public static Manufacturer FromRequest(ManufacturerRequestDTO request)
        {
            return new Manufacturer(request.Name, request.Location);
        }
    }
}
