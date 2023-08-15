namespace MachineInventory.Models
{
    public class Machine
    {
        private int id;
        private string name;
        private float price;
        private DateTime lastMaintained;
        private Manufacturer manufacturer;

        public int Id { get; }
        public string Name { get; }
        public float Price { get; }
        public DateTime LastMaintained { get; }
        public Manufacturer Manufacturer { get; }

        private Machine(int id, string name, float price, DateTime lastMaintained, Manufacturer manufacturer)
        {
            Id = id;
            Name = name;
            Price = price;
            LastMaintained = lastMaintained;
            Manufacturer = manufacturer;
        }
    }
}