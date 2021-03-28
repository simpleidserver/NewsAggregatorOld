using Microsoft.Extensions.Logging;
using NewsAggregator.ML.Articles.Operations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Transactions;

namespace NewsAggregator.ML.Articles
{
    public class ArticleEnlistment : IEnlistmentNotification
    {
        private readonly ILogger<ArticleManager> _logger;
        private readonly List<IOperation> _operations;

        public ArticleEnlistment(ILogger<ArticleManager> logger, Transaction transaction)
        {
            _logger = logger;
            _operations = new List<IOperation>();
            transaction.EnlistVolatile(this, EnlistmentOptions.None);
        }

        public void Enlist(IOperation operation)
        {
            operation.Execute(CancellationToken.None).Wait();
            _operations.Add(operation);
        }

        public void Commit(Enlistment enlistment)
        {
            enlistment.Done();
        }

        public void InDoubt(Enlistment enlistment)
        {
            Rollback(enlistment);
        }

        public void Prepare(PreparingEnlistment preparingEnlistment)
        {
            preparingEnlistment.Prepared();
        }

        public void Rollback(Enlistment enlistment)
        {
            try
            {
                for(int i = _operations.Count - 1; i >= 0; i--)
                {
                    _operations[i].Rollback();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            enlistment.Done();
        }
    }
}
