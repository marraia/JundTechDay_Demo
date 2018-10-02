using System;

namespace MSP_Demo.Domain
{
    public class GasStation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Location Location { get; set; }
    }
}
