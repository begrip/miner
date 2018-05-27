using Buzzilio.Begrip.Infrastructure.Transaction.Enumerations;
using Buzzilio.Begrip.Infrastructure.Transaction.Interfaces;
using System;

namespace Buzzilio.Begrip.Infrastructure.Transaction
{
    public class Transaction<T> : ITransaction
    {
        public string Id { get; set; }
        public T Item { get; set; }
        public Enums.ViewModelTransaction Type { get; set; }
        public bool IsCommitted { get; set; }
        public Transaction()
        {
            Id = Guid.NewGuid().ToString();
            IsCommitted = false;
        }
    }
}
