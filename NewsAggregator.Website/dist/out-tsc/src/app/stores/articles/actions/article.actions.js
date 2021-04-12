import { createAction, props } from '@ngrx/store';
export const startSearchArticlesInDatasource = createAction("[Feeds] START_SEARCH_ARTICLES_IN_DATASOURCE", props());
export const errorSearchArticlesInDatasource = createAction("[Feeds] ERROR_SEARCH_ARTICLES_IN_DATASOURCE");
export const completeSearchArticlesInDatasource = createAction("[Feeds] COMPLETE_SEARCH_ARTICLES_IN_DATASOURCE", props());
//# sourceMappingURL=article.actions.js.map