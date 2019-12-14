using eBidder.Domain;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace eBidder.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly ApplicationDbContext _context;

        public DeliveryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Delivery Create(Delivery delivery)
        {
            var newDelivery = _context.Deliveries.Add(delivery);

            _context.SaveChanges();

            return newDelivery;
        }

        public IEnumerable<Delivery> GetByUsername(string username)
        {
            return _context.Deliveries.Where(d => d.Recepient.Equals(username, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public IEnumerable<Delivery> GetAll()
        {
            return _context.Deliveries.ToList();
        }

        public IEnumerable<Delivery> GetInProgress()
        {
            return _context.Deliveries.Where(d => d.Status == DeliveryStatus.InProgress).ToList();
        }

        public IEnumerable<Delivery> GetDelivered()
        {
            return _context.Deliveries.Where(d => d.Status == DeliveryStatus.Delivered).ToList();
        }

        public IEnumerable<Delivery> GetReturned()
        {
            return _context.Deliveries.Where(d => d.Status == DeliveryStatus.Returned).ToList();
        }

        public Delivery ChangeRecepient(Delivery delivery, string newRecepient)
        {
            delivery.Recepient = newRecepient;
            _context.Deliveries.AddOrUpdate(delivery);
            _context.SaveChanges();

            return delivery;
        }

        public Delivery GetById(int deliveryId)
        {
            return _context.Deliveries.FirstOrDefault(d => d.DeliveryId == deliveryId);
        }

        public void Remove(Delivery delivery)
        {
            _context.Deliveries.Remove(delivery);

            _context.SaveChanges();
        }
    }
}