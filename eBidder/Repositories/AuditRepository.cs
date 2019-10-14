using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using eBidder.Domain;

namespace eBidder.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        private readonly ApplicationDbContext _context;

        public AuditRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateRecord(string username, string reference, string action, string details)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username must be defined");
            }

            var user = GetUser(username);

            if (user == null)
            {
                throw new ArgumentException("user with given username doesn't exist");
            }

            if (string.IsNullOrEmpty(reference))
            {
                throw new ArgumentNullException("reference must be defined");
            }

            if (string.IsNullOrEmpty(action))
            {
                throw new ArgumentNullException("action must be defined");
            }

            var auctionRecord = new AuctionRecord
            {
                Action = action,
                Details = details,
                Reference = reference,
                User = user
            };

            _context.AuctionRecords.Add(auctionRecord);
            _context.SaveChanges();
        }

        public void CreateRecord(string username, string reference, string action)
        {
            CreateRecord(username, reference, action, details: null);
        }

        public IEnumerable<AuctionRecord> GetAuditRecords()
        {
            return _context.AuctionRecords
                .Include(x => x.User)
                .ToList();
        }

        public IEnumerable<AuctionRecord> GetAuditRecordsForUser(string username)
        {
            var user = GetUser(username);

            if (user == null)
            {
                throw new ArgumentException("user with given username doesn't exist");
            }

            return _context.AuctionRecords
                .Include(x => x.User)
                .Where(a => a.User.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }


        private User GetUser(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}