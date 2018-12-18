using System.Collections.Generic;
using eBidder.Domain;
using eBidder.Repositories;
using Moq;

namespace eBidder.UnitTests.Mocks
{
    public class TransactionLogRepositoryMock
    {
        private ICollection<TransactionLog> _logs;

        public ITransactionLogRepository CreateRepository()
        {
            _logs = new List<TransactionLog>();

            var fakeRepository = new Mock<ITransactionLogRepository>();
            // TODO (ispit): Add missing methods mock implementations

            fakeRepository.Setup(x => x.GetTransactionLogs())
                .Returns(() => _logs);

            return fakeRepository.Object;
        }
    }
}