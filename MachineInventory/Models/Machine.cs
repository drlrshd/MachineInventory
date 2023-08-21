using MachineInventory.DTO;

namespace MachineInventory.Models
{
    public class Machine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public DateTime LastMaintained { get; set; }

        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        public Machine(string name, float price, DateTime lastMaintained, int manufacturerId)
        {
            Name = name;

            if (price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative!");
            }
            Price = price;

            if (lastMaintained > DateTime.Now)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(lastMaintained),
                    "Date has to be in the past!"
                );
            }
            LastMaintained = lastMaintained;

            ManufacturerId = manufacturerId;
        }

        public static MachineResponseDTO ToResponse(Machine machine)
        {
            return new MachineResponseDTO
            {
                Id = machine.Id,
                Name = machine.Name,
                Price = machine.Price,
                LastMaintained = machine.LastMaintained,
                ManufacturerId = machine.Manufacturer.Id,
                ManufacturerName = machine.Manufacturer.Name,
            };
        }

        public static Machine FromRequest(MachineRequestDTO request)
        {
            return new Machine(
                request.Name,
                request.Price,
                request.LastMaintained,
                request.ManufacturerId
            );
        }
    }
}
