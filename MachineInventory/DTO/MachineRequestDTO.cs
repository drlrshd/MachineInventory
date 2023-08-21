namespace MachineInventory.DTO
{
    public class MachineRequestDTO
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public DateTime LastMaintained { get; set; }
        public int ManufacturerId { get; set; }
    }
}
