using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{


    public class Truck
    {
        
        public int Id { get; set; }
        
        public string Plate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string VIN { get; set; }
        [ForeignKey("DriverId")]
        public int DriverId { get; set; }
        public Driver Driver { get; set; }

        //public virtual List<Repair> Repairs { get; set; }


        public DateTime Insurance { get; set; }
        public DateTime TechReview { get; set; }
        public DateTime TachoLeg { get; set; }

    }
}
