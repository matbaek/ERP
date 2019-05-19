using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Application
{
    public class OfferRepository
    {
        private Offer offer = new Offer();

        //Methods
        public void AddOffer(Offer offer)
        {
            offer.AddOffer(offer.Customer, offer.DateOfOffer, offer.TotalPrice);
        }

        public void EditOffer(Offer offer)
        {
            offer.EditOffer(offer.OfferID, offer.Customer, offer.DateOfOffer, offer.TotalPrice);
        }

        public void DeleteOffer(int offerID)
        {
            offer.DeleteOffer(offerID);
        }

        public List<Offer> DisplayOffers()
        {
            return offer.GetOffers();
        }

        public List<Offer> DisplaySpecificOffers(Offer offer)
        {
            return offer.GetSpecificOffers(offer.OfferID, offer.Customer, offer.DateOfOffer, offer.TotalPrice);
        }
        public Offer DisplayOffer(int offerID)
        {
            return offer.GetOffer(offerID);
        }

        public int DisplayLastOfferID()
        {
            return offer.GetLastOfferID();
        }
    }
}

