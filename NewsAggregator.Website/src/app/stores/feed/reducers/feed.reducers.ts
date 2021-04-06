import { Action, createReducer, on } from "@ngrx/store";
import { completeSearchFeeds, completeDeleteDatasources, DeleteDatasource } from '../actions/feed.actions';
import { Feed } from "../models/feed.model";
import { SearchFeedsResult } from "../models/search-feed.model";

export interface FeedLstState {
  isLoading: boolean;
  isErrorLoadOccured: boolean;
  content: SearchFeedsResult;
}

export const initialFeedLstState: FeedLstState = {
  content: new SearchFeedsResult(),
  isLoading: true,
  isErrorLoadOccured: false
};

const feedLstReducer = createReducer(
  initialFeedLstState,
  on(completeSearchFeeds, (state, { content }) => ({ content: content, isLoading: false, isErrorLoadOccured: false })),
  on(completeDeleteDatasources, (state, { parameters }) => {
    const content = state.content.content.filter((f: Feed) => {
      return parameters.filter((df: DeleteDatasource) => {
        return f.datasourceId === df.datasourceId && f.feedId === df.feedId;
      }).length === 0;
    });
    const result: SearchFeedsResult = { content: content, startIndex: state.content.startIndex, count : state.content.count, totalLength: state.content.totalLength };
    return { content: result, isLoading: false, isErrorLoadOccured: false };
  })
);

export function getFeedLstReducer(state: FeedLstState | undefined, action: Action) {
  return feedLstReducer(state, action);
}
