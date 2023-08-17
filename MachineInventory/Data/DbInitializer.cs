using MachineInventory.Models;

namespace MachineInventory.Data
{
    public static class DbInitializer
    {
        public static void Initialize(InventoryContext context)
        {
            context.Database.EnsureCreated();

            // Check if DB already has any entries
            if (context.Machines.Any())
            {
                return; // end method if DB already has entries
            }

            // Else fill it with some things
            var manufacturers = new Manufacturer[]
            {
                new Manufacturer { Name = "AB", Location = "Aberdeen" },
                new Manufacturer { Name = "BE", Location = "Berlin" }
            };

            foreach (Manufacturer manufacturer in manufacturers)
            {
                context.Manufacturers.Add(manufacturer);
            }

            context.SaveChanges();

            var machines = new Machine[]
            {
                new Machine(0, "Metal Cutter MC01", 999.9f, DateTime.UtcNow, "AB"),
                new Machine(0, "Wood Chopper WC01", 9999.9f, DateTime.UtcNow, "AB"),
                new Machine(0, "Paper Maker P3A", 666.6f, DateTime.Parse("2020-02-20 20:20"), "BE")
            };

            foreach (Machine machine in machines)
            {
                context.Machines.Add(machine);
            }

            context.SaveChanges();
        }
    }
}
