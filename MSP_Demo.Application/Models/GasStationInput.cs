using System;
using System.Collections.Generic;
using System.Text;
using MSP_Demo.Domain;

namespace MSP_Demo.Application.Models
{
    public class GasStationInput
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Location Location { get; set; }
    }
}
