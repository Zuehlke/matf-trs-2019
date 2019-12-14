using eBidder.Domain;
using eBidder.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eBidder.UnitTests.Mocks
{
    internal class DeliveryRepositoryMock
    {
        private ICollection<Delivery> _deliveries;

        public IDeliveryRepository CreateRepository()
        {
            _deliveries = new List<Delivery>();

            var fakeRepository = new Mock<IDeliveryRepository>();

            fakeRepository.Setup(d => d.Create(It.IsAny<Delivery>()))
                .Callback((Delivery delivery) => _deliveries.Add(delivery))
                .Returns((Delivery delivery) => _deliveries.FirstOrDefault(d => d.DeliveryId == delivery.DeliveryId));
            
            fakeRepository.Setup(d => d.GetAll()).Returns(() => _deliveries);

            // Napravite mock-ove koji nedostaju

            return fakeRepository.Object;
        }
    }
}
