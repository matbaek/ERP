using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Order
{
    public class Offer
    {
        //Property
        public int OfferNumber { get; set; }
        public DateTime OfferDate { get; set; }
        public DateTime OfferDateOfExpiration { get; set; }
        public double Price { get; set; }
    }
}
