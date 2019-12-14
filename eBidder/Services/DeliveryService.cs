using System.Collections.Generic;
using eBidder.Models;
using eBidder.Repositories;
using System;
using System.Linq;
using eBidder.Mappers;
using eBidder.Domain;

namespace eBidder.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IUserRepository _userRepository;

        public DeliveryService()
        {
            _deliveryRepository = new UnitOfWork().DeliveryRepository;
        }

        public DeliveryService(IDeliveryRepository deliveryRepository, IUserRepository userRepository)
        {
            _deliveryRepository = deliveryRepository;
            _userRepository = userRepository;
        }

        public DeliveryViewModel CreateDelivery(int deliveryId, string recepient, string item)
        {
            if (recepient == null)
            {
                throw new ArgumentNullException(nameof(recepient), "Recepient is null");
            }

            var user = GetUser(recepient);
            if (user == null)
            {
                throw new InvalidOperationException("User cannot be found");
            }

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item is null");
            }

            var newDelivery = new DeliveryViewModel
            {
                DeliveryId = deliveryId,
                Recepient = recepient,
                DeliveryItem = item,
                Status = (int) DeliveryStatus.InProgress,
                SentDate = DateTime.Now
            };

            return _deliveryRepository.Create(newDelivery.ToDelivery()).ToDeliveryViewModel();
        }

        public void RemoveDelivery(int deliveryId)
        {
            var delivery = _deliveryRepository.GetById(deliveryId);

            if (delivery == null)
            {
                throw new InvalidOperationException("Delivery doesn't exist");
            }

            _deliveryRepository.Remove(delivery);
        }

        public IEnumerable<DeliveryViewModel> GetAllDeliveries()
        {
            return _deliveryRepository.GetAll()?.Select(d => d.ToDeliveryViewModel());
        }

        public IEnumerable<DeliveryViewModel> GetDeliveriesForUser(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException(nameof(username), "Recepient is null");
            }

            var user = GetUser(username);
            if (user == null)
            {
                throw new InvalidOperationException("User cannot be found");
            }

            return _deliveryRepository.GetByUsername(user.Username).Select(d => d.ToDeliveryViewModel());
        }
        
        
        public DeliveryViewModel ChangeRecepient(int deliveryId, string newRecepient)
        {
            var delivery = _deliveryRepository.GetById(deliveryId);

            if (delivery == null)
            {
                throw new InvalidOperationException("Delivery doesn't exist");
            }

            if (newRecepient == null)
            {
                throw new ArgumentNullException(nameof(newRecepient), "Recepient is null");
            }

            var user = GetUser(newRecepient);
            if (user == null)
            {
                throw new InvalidOperationException("User doesn't exist");
            }

            return _deliveryRepository.ChangeRecepient(delivery, newRecepient).ToDeliveryViewModel();
        }

        private User GetUser(string username)
        {
            return _userRepository.GetByUsername(username);
        }
    }
}