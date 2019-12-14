using System.Collections.Generic;
using eBidder.Domain;

namespace eBidder.Repositories
{
    public interface IDeliveryRepository
    {
        Delivery Create(Delivery delivery);

        IEnumerable<Delivery> GetAll();

        Delivery GetById(int deliveryId);
        
        void Remove(Delivery delivery);

        IEnumerable<Delivery> GetByUsername(string username);

        Delivery ChangeRecepient(Delivery delivery, string newRecepient);
    }
}