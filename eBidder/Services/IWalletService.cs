using System.Collections.Generic;
using eBidder.Domain;

namespace eBidder.Services
{
    public interface IWalletService
    {
        void AddMoney(string username, double amount);

        double GetMoney(string username);

        void RemoveMoney(string username, double amount);

        void Transfer(string fromUser, string toUser, double amount);

        IEnumerable<TransactionLog> GetTransactionLogs();
    }
}