using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eBidder.Domain;

namespace eBidder.Repositories
{
    public class TransactionLogRepository : ITransactionLogRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<TransactionLog> GetTransactionLogs()
        {
            return _context.TransactionLogs.ToList();
        }

        public void LogSuccessfulTransaction(string fromUser, string toUser, double amount)
        {
            var newSuccessfulTransactionLog = new TransactionLog
            {
                FromUser = fromUser,
                ToUser = toUser,
                Amount = amount,
                TransactionStatus = TransactionStatus.Successful
            };

            _context.TransactionLogs.Add(newSuccessfulTransactionLog);
            _context.SaveChanges();
        }

        public void LogUnsuccessfulTransaction(string fromUser, string toUser, double amount)
        {
            var newUnsuccessfulTransactionLog = new TransactionLog
            {
                FromUser = fromUser,
                ToUser = toUser,
                Amount = amount,
                TransactionStatus = TransactionStatus.Unsuccessful
            };

            _context.TransactionLogs.Add(newUnsuccessfulTransactionLog);
            _context.SaveChanges();
        }
    }
}