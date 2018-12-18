using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eBidder.Domain;

namespace eBidder.Repositories
{
    public interface ITransactionLogRepository
    {
        IEnumerable<TransactionLog> GetTransactionLogs();

        void LogSuccessfulTransaction(string fromUser, string toUser, double amount);

        void LogUnsuccessfulTransaction(string fromUser, string toUser, double amount);
    }
}