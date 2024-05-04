using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserRegistration.Models
{
    public class TourismDestination
    {
        public int Id { get; set; } // Auto-incrementing ID as primary key
        public string Country { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}