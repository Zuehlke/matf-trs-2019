namespace eBidder.Services
{
    public interface IWalletService
    {
        void AddMoney(string username, double amount);

        double GetMoney(string username);

        void RemoveMoney(string username, double amount);
    }
}