using System;
using eBidder.Domain;

namespace eBidder.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private IAuctionRepository _auctionRepository;
        private IUserRepository _userRepository;
        private IAuditRepository _auditRepository;
        private ITransactionLogRepository _transactionLogRepository;

        public IAuctionRepository AuctionRepository => _auctionRepository ?? (_auctionRepository = new AuctionRepository(_context));

        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(_context));

        public IAuditRepository AuditRepository => _auditRepository ?? (_auditRepository = new AuditRepository(_context));

        public ITransactionLogRepository TransactionLogRepository => _transactionLogRepository ?? (_transactionLogRepository = new TransactionLogRepository(_context));


        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}