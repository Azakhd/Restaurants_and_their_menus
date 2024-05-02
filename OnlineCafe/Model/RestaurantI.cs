using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCafe.Model
{
    public class RestaurantI
    {
        public int id;
        public string? Name { get; set; }
        public string? Chef { get; set; }
        public List<Employees>? Employees;
        public decimal? Service { get; set; }
        public List<Dishes>? menu;
    }
}
