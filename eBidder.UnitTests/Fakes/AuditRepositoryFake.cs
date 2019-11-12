using System.Collections.Generic;
using System.Linq;
using eBidder.Domain;
using eBidder.Repositories;

namespace eBidder.UnitTests.Fakes
{
    public class AuditRepositoryFake : IAuditRepository
    {
        private readonly List<AuctionRecord> _auctionRecords = new List<AuctionRecord>();

        public IEnumerable<AuctionRecord> GetAuditRecords()
        {
            return _auctionRecords;
        }

        public IEnumerable<AuctionRecord> GetAuditRecordsForUser(string username)
        {
            return _auctionRecords.Where(x => x.User.Username == username);
        }

        public void CreateRecord(string username, string reference, string action, string details)
        {
            var newRecord = new AuctionRecord
            {
                Action = action,
                Details = details,
                Reference = reference,
                User = new User { Username = username }
            };

            _auctionRecords.Add(newRecord);
        }

        public void CreateRecord(string username, string reference, string action)
        {
            var newRecord = new AuctionRecord
            {
                Action = action,
                Reference = reference,
                User = new User { Username = username }
            };

            _auctionRecords.Add(newRecord);
        }
    }
}