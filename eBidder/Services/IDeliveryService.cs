using System.Collections.Generic;
using eBidder.Models;

namespace eBidder.Services
{
    public interface IDeliveryService
    {
        DeliveryViewModel CreateDelivery(int deliveryId, string recepient, string item);
        
        void RemoveDelivery(int deliveryId);

        IEnumerable<DeliveryViewModel> GetAllDeliveries();
        
        IEnumerable<DeliveryViewModel> GetDeliveriesForUser(string username);
        
        DeliveryViewModel ChangeRecepient(int deliveryId, string newRecepient);
    }
}