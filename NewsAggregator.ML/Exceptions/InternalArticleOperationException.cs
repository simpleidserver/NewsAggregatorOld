using System;

namespace NewsAggregator.ML.Exceptions
{
    public class InternalArticleOperationException : Exception
    {
        public InternalArticleOperationException(string message) : base(message) { }
    }
}
