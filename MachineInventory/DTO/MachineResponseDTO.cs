using MachineInventory.Models;

namespace MachineInventory.DTO
{
    public class MachineResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public DateTime LastMaintained { get; set; }
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
    }
}
