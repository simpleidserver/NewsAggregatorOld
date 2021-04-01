using NewsAggregator.Core.QueryResults;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Core.Repositories
{
    public interface ISessionQueryRepository
    {
        string GetSessionActionsSQL(string sessionId);
        Task<IEnumerable<SessionQueryResult>> GetAll(CancellationToken cancellationToken);
    }
}
