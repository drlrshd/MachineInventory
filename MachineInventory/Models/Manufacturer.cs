using Microsoft.AspNetCore.Components.Routing;

namespace MachineInventory.Models
{
    public class Manufacturer
    {
        private string name;
        private string location;

        public string Name { get; }
        public string Location { get; }

        private Manufacturer(string name, string location)
        {
            Name = name;
            Location = location;
        }

    }
}
