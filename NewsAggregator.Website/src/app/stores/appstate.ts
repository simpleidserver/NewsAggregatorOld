import { createSelector } from '@ngrx/store';
import * as fromArticle from './articles/reducers/article.reducers';
import * as fromDatasource from './datasource/reducers/datasource.reducers';
import * as fromFeed from './feed/reducers/feed.reducers';

export interface AppState {
  searchFeeds: fromFeed.SearchFeedsState,
  allFeeds: fromFeed.AllFeedsState,
  feed: fromFeed.FeedState,
  searchArticles: fromArticle.SearchArticlesState,
  datasource: fromDatasource.DatasourceState
}

export const selectSearchFeeds = (state: AppState) => state.searchFeeds;
export const selectAllFeeds = (state: AppState) => state.allFeeds;
export const selectFeed = (state: AppState) => state.feed;
export const selectSearchArticles = (state: AppState) => state.searchArticles;
export const selectDatasource = (state: AppState) => state.datasource;

export const selectFeedSearchResult = createSelector(
  selectSearchFeeds,
  (state: fromFeed.SearchFeedsState) => {
    if (!state || state.content === null) {
      return null;
    }

    return state.content;
  }
);

export const selectAllFeedsResult = createSelector(
  selectAllFeeds,
  (state: fromFeed.AllFeedsState) => {
    if (!state || state.content === null) {
      return null;
    }

    return state.content;
  }
);

export const selectFeedResult = createSelector(
  selectFeed,
  (state: fromFeed.FeedState) => {
    if (!state || state.content === null) {
      return null;
    }

    return state.content;
  }
);

export const selectSearchArticlesResult = createSelector(
  selectSearchArticles,
  (state: fromArticle.SearchArticlesState) => {
    if (!state || state.content === null) {
      return null;
    }

    return state.content;
  }
);

export const selectDatasourceResult = createSelector(
  selectDatasource,
  (state: fromDatasource.DatasourceState) => {
    if (!state || state.content === null) {
      return null;
    }

    return state.content;
  }
);

export const appReducer = {
  searchFeeds: fromFeed.getSearchFeedsReducer,
  allFeeds: fromFeed.getAllFeedsReducer,
  feed: fromFeed.getFeedReducer,
  searchArticles: fromArticle.getSearchArticlesReducer,
  datasource: fromDatasource.getDatasourceReducer
};
