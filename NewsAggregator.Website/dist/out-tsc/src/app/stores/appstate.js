import { createSelector } from '@ngrx/store';
import * as fromArticle from './articles/reducers/article.reducers';
import * as fromFeed from './feed/reducers/feed.reducers';
export const selectSearchFeeds = (state) => state.searchFeeds;
export const selectAllFeeds = (state) => state.allFeeds;
export const selectFeedArticleLst = (state) => state.feedArticleLst;
export const selectFeed = (state) => state.feed;
export const selectSearchArticlesInDatasource = (state) => state.searchArticlesInDatasource;
export const selectFeedSearchResult = createSelector(selectSearchFeeds, (state) => {
    if (!state || state.content === null) {
        return null;
    }
    return state.content;
});
export const selectAllFeedsResult = createSelector(selectAllFeeds, (state) => {
    if (!state || state.content === null) {
        return null;
    }
    return state.content;
});
export const selectFeedArticleResult = createSelector(selectFeedArticleLst, (state) => {
    if (!state || state.content === null) {
        return null;
    }
    return state.content;
});
export const selectFeedResult = createSelector(selectFeed, (state) => {
    if (!state || state.content === null) {
        return null;
    }
    return state.content;
});
export const selectSearchArticlesInDatasourceResult = createSelector(selectSearchArticlesInDatasource, (state) => {
    if (!state || state.content === null) {
        return null;
    }
    return state.content;
});
export const appReducer = {
    searchFeeds: fromFeed.getSearchFeedsReducer,
    allFeeds: fromFeed.getAllFeedsReducer,
    feedArticleLst: fromFeed.getFeedArticleLstReducer,
    feed: fromFeed.getFeedReducer,
    searchArticlesInDatasource: fromArticle.getSearchArticlesInFeedReducer
};
//# sourceMappingURL=appstate.js.map