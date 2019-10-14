using eBidder.Domain;
using System.Collections.Generic;

namespace eBidder.Repositories
{
    public interface IAuditRepository
    {
        IEnumerable<AuctionRecord> GetAuditRecords();

        IEnumerable<AuctionRecord> GetAuditRecordsForUser(string username);

        void CreateRecord(string username, string reference, string action, string details);

        void CreateRecord(string username, string reference, string action);
    }
}