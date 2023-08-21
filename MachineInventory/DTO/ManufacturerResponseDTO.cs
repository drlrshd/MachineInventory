using MachineInventory.Models;

namespace MachineInventory.DTO
{
    public class ManufacturerResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public Dictionary<int, string> Machines { get; set; }
    }
}
