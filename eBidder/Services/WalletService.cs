using System;
using eBidder.Domain;
using eBidder.Repositories;

namespace eBidder.Services
{
    public class WalletService : IWalletService
    {
        private readonly IUserRepository _userRepository;

        public WalletService()
        {
            var unitOfWork = new UnitOfWork();
            _userRepository = unitOfWork.UserRepository;
        }

        public WalletService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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