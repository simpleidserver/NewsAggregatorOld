import { createSelector } from '@ngrx/store';
import * as fromFeed from './feed/reducers/feed.reducers';

export interface AppState {
  searchFeeds: fromFeed.SearchFeedsState,
  allFeeds: fromFeed.AllFeedsState,
  feedArticleLst: fromFeed.FeedArticleLstState,
  feed: fromFeed.FeedState
}

export const selectSearchFeeds = (state: AppState) => state.searchFeeds;
export const selectAllFeeds = (state: AppState) => state.allFeeds;
export const selectFeedArticleLst = (state: AppState) => state.feedArticleLst;
export const selectFeed = (state: AppState) => state.feed;

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

export const selectFeedArticleResult = createSelector(
  selectFeedArticleLst,
  (state: fromFeed.FeedArticleLstState) => {
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

export const appReducer = {
  searchFeeds: fromFeed.getSearchFeedsReducer,
  allFeeds: fromFeed.getAllFeedsReducer,
  feedArticleLst: fromFeed.getFeedArticleLstReducer,
  feed: fromFeed.getFeedReducer
};
