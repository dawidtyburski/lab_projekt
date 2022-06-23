using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Models
{


    public class Truck
    {

        public int TruckId { get; set; }
        public int DriverID { get; set; }
        public string Plate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Vin { get; set; }

        public DateTime Insurance { get; set; }
        public DateTime TechReview { get; set; }
        public DateTime TachoLeg { get; set; }

    }
}
