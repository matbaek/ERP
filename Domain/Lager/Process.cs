using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Process
    {
        //Property
        public string Name { get; set; }
        public double Weight { get; set; }
        public DateTime DateOfProcess { get; set; }
        public double Loss { get; set; }

        //Metods
        public bool BeansProcessed(string name, double weight, DateTime dateOfProcess, double loss)
        {
            return false;
        }
    }
}
