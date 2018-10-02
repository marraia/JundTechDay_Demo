using System;
using System.Collections.Generic;

namespace MSP_Demo.Domain
{
    public class Location
    {
        public string Type { get; set; }
        public List<double> Coordinates { get; set; }
    }
}
