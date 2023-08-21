using MachineInventory.Models;

namespace MachineInventory.Data
{
    public static class DbInitializer
    {
        public static void Initialize(InventoryContext context)
        {
            // Check if DB already has any entries
            if (context.Machines.Any())
            {
                return; // end method if DB already has entries
            }

            // Else fill it with some things
            var manufacturers = new Manufacturer[]
            {
                new Manufacturer("AB", "Aberdeen"),
                new Manufacturer("BE", "Berlin")
            };

            foreach (Manufacturer manufacturer in manufacturers)
            {
                context.Manufacturers.Add(manufacturer);
            }

            context.SaveChanges();

            var machines = new Machine[]
            {
                new Machine("Metal Cutter MC01", 999.9f, DateTime.UtcNow, manufacturers[0].Id),
                new Machine("Wood Chopper WC01", 9999.9f, DateTime.UtcNow, manufacturers[0].Id),
                new Machine(
                    "Paper Maker P3A",
                    666.6f,
                    DateTime.Parse("2020-02-20 20:20"),
                    manufacturers[1].Id
                )
            };

            foreach (Machine machine in machines)
            {
                context.Machines.Add(machine);
            }

            context.SaveChanges();
        }
    }
}
