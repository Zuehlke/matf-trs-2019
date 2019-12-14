using eBidder.Domain;
using eBidder.Models;

namespace eBidder.Mappers
{
    public static class DeliveryMapper
    {
        public static DeliveryViewModel ToDeliveryViewModel(this Delivery delivery)
        {
            return new DeliveryViewModel
            {
                DeliveryId = delivery.DeliveryId,
                Recepient = delivery.Recepient,
                DeliveryItem = delivery.DeliveryItem,
                DeliveredDate = delivery.DeliveredDate,
                SentDate = delivery.SentDate,
                Status = (int)delivery.Status
            };
        }

        public static Delivery ToDelivery(this DeliveryViewModel deliveryViewModel)
        {
            return new Delivery
            {
                DeliveryId = deliveryViewModel.DeliveryId,
                Recepient = deliveryViewModel.Recepient,
                DeliveryItem = deliveryViewModel.DeliveryItem,
                DeliveredDate = deliveryViewModel.DeliveredDate,
                SentDate = deliveryViewModel.SentDate,
                Status = (DeliveryStatus) deliveryViewModel.Status
            };
        }
    }
}