using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAPI.Models
{
    public class Dog
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }
        public int Speed {  get; set; }
    }
}
