import { createSelector } from '@ngrx/store';
import * as fromFeed from './feed/reducers/feed.reducers';

export interface AppState {
  feedLst: fromFeed.FeedLstState
}

export const selectFeedLst = (state: AppState) => state.feedLst;

export const selectFeedLstResult = createSelector(
  selectFeedLst,
  (state: fromFeed.FeedLstState) => {
    if (!state || state.content === null) {
      return null;
    }

    return state.content;
  }
);

export const appReducer = {
  feedLst: fromFeed.getFeedLstReducer
};
