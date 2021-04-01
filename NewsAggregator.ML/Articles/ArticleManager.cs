using NewsAggregator.ML.Articles.Operations;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.Articles
{
    public class ArticleManager : IArticleManager
    {
        private readonly IEnumerable<IArticleOperation> _articleOperations;

        public ArticleManager(IEnumerable<IArticleOperation> articleOperations)
        {
            _articleOperations = articleOperations;
        }

        public async Task TrainArticles(IEnumerable<string> languages, CancellationToken cancellationToken)
        {
            foreach(var articleOperation in _articleOperations)
            {
                foreach(var language in languages)
                {
                    await articleOperation.Execute(language, cancellationToken);
                }
            }
        }
    }
}
