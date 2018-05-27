using Buzzilio.Begrip.Infrastructure.Transaction.Interfaces;
using System.Collections.Generic;

namespace Buzzilio.Begrip.Infrastructure.Transaction
{
    public class ViewModelSession<T> : IViewModelSession
    {
        public List<Transaction<T>> Transactions { get; set; }

        public ViewModelSession()
        {
            Transactions = new List<Transaction<T>>();
        }
    }
}
