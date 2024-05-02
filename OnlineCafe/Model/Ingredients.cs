using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCafe.Model
{
    public class Ingredients
    {
        public string? _name;
        public decimal _weight;
        public string? _product_type;
        public Ingredients(string name,decimal weight,string product_type) 
        { 
            _name = name;
            _weight = weight;
            _product_type = product_type;
        }
    }
}
