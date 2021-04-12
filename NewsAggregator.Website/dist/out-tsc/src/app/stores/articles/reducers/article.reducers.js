import { createReducer, on } from "@ngrx/store";
import { SearchArticlesResult } from "../../articles/models/search-article.model";
import { completeSearchArticlesInDatasource } from '../actions/article.actions';
export const initialSearchArticlesInDatasource = {
    content: new SearchArticlesResult(),
    isLoading: true,
    isErrorLoadOccured: false
};
const searchArticlesInFeedReducer = createReducer(initialSearchArticlesInDatasource, on(completeSearchArticlesInDatasource, (state, { content }) => ({ content: content, isLoading: false, isErrorLoadOccured: false })));
export function getSearchArticlesInFeedReducer(state, action) {
    return searchArticlesInFeedReducer(state, action);
}
//# sourceMappingURL=article.reducers.js.map