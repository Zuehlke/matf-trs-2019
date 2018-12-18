using System;
using System.Collections.Generic;
using eBidder.Domain;
using eBidder.Repositories;

namespace eBidder.Services
{
    public class WalletService : IWalletService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITransactionLogRepository _transactionLogRepository;

        public WalletService()
        {
            var unitOfWork = new UnitOfWork();
            _userRepository = unitOfWork.UserRepository;
        }

        public WalletService(IUserRepository userRepository, ITransactionLogRepository transactionLogRepository)
        {
            _userRepository = userRepository;
            _transactionLogRepository = transactionLogRepository;
        }

        public void AddMoney(string username, double amount)
        {
            if (username == null)
            {
                throw new ArgumentNullException("Username cant be null");
            }

            var user = GetUser(username);
            if (user == null)
            {
                throw new InvalidOperationException("User cannot be found");
            }

            _userRepository.AddMoney(username, amount);

        }

        public void RemoveMoney(string username, double amount)
        {
            if (username == null)
            {
                throw new ArgumentNullException("Username cant be null");
            }

            var user = GetUser(username);
            if (user == null)
            {
                throw new InvalidOperationException("User cannot be found");
            }

            if (user.Money < amount)
            {
                throw new InvalidOperationException("Not enough money");
            }

            _userRepository.RemoveMoney(username, amount);
        }

        public void Transfer(string fromUser, string toUser, double amount)
        {

            if (fromUser == null || toUser == null)
            {
                throw new ArgumentNullException("Usernames can't be null");
            }

            var userFrom = GetUser(fromUser);
            var userTo = GetUser(toUser);

            if (userFrom == null || userTo == null)
            {
                _transactionLogRepository.LogUnsuccessfulTransaction(fromUser, toUser, amount);
                throw new InvalidOperationException("User cannot be found");
            }

            if (amount <= 0)
            {
                _transactionLogRepository.LogUnsuccessfulTransaction(fromUser, toUser, amount);
                throw new InvalidOperationException("Cannot transfer negative value");
            }

            if (userFrom.Money < amount)
            {
                _transactionLogRepository.LogUnsuccessfulTransaction(fromUser, toUser, amount);
                throw new InvalidOperationException("Not enough money");
            }

            _userRepository.TransferMoney(fromUser, toUser, amount);
            _transactionLogRepository.LogSuccessfulTransaction(fromUser, toUser, amount);
        }

        public IEnumerable<TransactionLog> GetTransactionLogs()
        {
            return _transactionLogRepository.GetTransactionLogs();
        }

        public double GetMoney(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException("Username cant be null");
            }

            var user = GetUser(username);
            if (user == null)
            {
                throw new InvalidOperationException("User cannot be found");
            }

            return _userRepository.GetMoney(username);
        }

        private User GetUser(string username)
        {
            return _userRepository.GetByUsername(username);
        }
    }
}