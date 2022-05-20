using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Driver
    {
        public int DriverId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
    }
}