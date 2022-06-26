using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Repair
    {

        public int Id { get; set; }
        public string Name { get; set; }
        
        [ForeignKey("TruckId")]
        public int TruckId { get; set; }
        public Truck Truck { get; set; }
        [Range(typeof(DateTime), "1/1/1900", "6/6/2079")]
        public DateTime Date { get; set; }

    }
}
