using System;

namespace MSP_Demo.Domain.ValueObject
{
    public class GasStationLocationValueObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Location Location { get; set; }
        public DistValueObject Dist { get; set; }
    }
}
