using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCafe.Model
{
    public  class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public  decimal Gramprice { get; set; }
        public string? TypeProduct { get; set; }
       
    }

}
