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

            fakeRepository.Setup(x =>
                    x.LogUnsuccessfulTransaction(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>()))
                .Callback((string userFrom, string userTo, double amount) => _logs.Add(new TransactionLog
                {
                    FromUser = userFrom,
                    ToUser = userTo,
                    Amount = amount,
                    TransactionStatus = TransactionStatus.Unsuccessful
                }));

            fakeRepository.Setup(x =>
                    x.LogSuccessfulTransaction(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>()))
                .Callback((string userFrom, string userTo, double amount) => _logs.Add(new TransactionLog
                {
                    FromUser = userFrom,
                    ToUser = userTo,
                    Amount = amount,
                    TransactionStatus = TransactionStatus.Successful
                }));

            return fakeRepository.Object;
        }
    }
}