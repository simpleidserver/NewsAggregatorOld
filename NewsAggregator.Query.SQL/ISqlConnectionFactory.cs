using System.Data;

namespace NewsAggregator.Query.SQL
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}
