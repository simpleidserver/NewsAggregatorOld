import { Action, createReducer, on } from "@ngrx/store";
import { SearchArticlesResult } from "../../articles/models/search-article.model";
import { completeDeleteDatasources, completeGetAllFeeds, completeGetFeed, completeSearchFeedArticles, completeSearchFeeds, DeleteDatasource } from '../actions/feed.actions';
import { Feed } from "../models/feed.model";
import { SearchFeedsResult } from "../models/search-feed.model";

export interface SearchFeedsState {
  isLoading: boolean;
  isErrorLoadOccured: boolean;
  content: SearchFeedsResult;
}

export interface AllFeedsState {
  isLoading: boolean;
  isErrorLoadOccured: boolean;
  content: Feed[];
}

export interface FeedArticleLstState {
  isLoading: boolean;
  isErrorLoadOccured: boolean;
  content: SearchArticlesResult;
}

export interface FeedState {
  isLoading: boolean;
  isErrorLoadOccured: boolean;
  content: Feed;
}

export const initialSearchFeeds: SearchFeedsState = {
  content: new SearchFeedsResult(),
  isLoading: true,
  isErrorLoadOccured: false
};

export const initialAllFeeds: AllFeedsState = {
  content: [],
  isLoading: true,
  isErrorLoadOccured: false
};

export const initialFeedArticleLstState: FeedArticleLstState = {
  content: new SearchArticlesResult(),
  isLoading: true,
  isErrorLoadOccured: false
};

export const initialFeedState: FeedState = {
  content: new Feed(),
  isLoading: true,
  isErrorLoadOccured: false
};

const searchFeedsReducer = createReducer(
  initialSearchFeeds,
  on(completeSearchFeeds, (state, { content }) => ({ content: content, isLoading: false, isErrorLoadOccured: false })),
  on(completeDeleteDatasources, (state, { parameters }) => {
    const content = state.content.content.filter((f: Feed) => {
      return parameters.filter((df: DeleteDatasource) => {
        return f.datasourceId === df.datasourceId && f.feedId === df.feedId;
      }).length === 0;
    });
    const result: SearchFeedsResult = { content: content, startIndex: state.content.startIndex, count: state.content.count, totalLength: state.content.totalLength };
    return { content: result, isLoading: false, isErrorLoadOccured: false };
  })
);

const allFeedsReducer = createReducer(
  initialAllFeeds,
  on(completeGetAllFeeds, (state, { content }) => ({ content: content, isLoading: false, isErrorLoadOccured: false }))
);

const feedArticleLstReducer = createReducer(
  initialFeedArticleLstState,
  on(completeSearchFeedArticles, (state, { content }) => ({ content: content, isLoading: false, isErrorLoadOccured: false }))
);

const feedReducer = createReducer(
  initialFeedState,
  on(completeGetFeed, (state, { content }) => ({ content: content, isLoading: false, isErrorLoadOccured: false }))
);

export function getSearchFeedsReducer(state: SearchFeedsState | undefined, action: Action) {
  return searchFeedsReducer(state, action);
}

export function getAllFeedsReducer(state: AllFeedsState | undefined, action: Action) {
  return allFeedsReducer(state, action);
}

export function getFeedArticleLstReducer(state: FeedArticleLstState | undefined, action: Action) {
  return feedArticleLstReducer(state, action);
}

export function getFeedReducer(state: FeedState | undefined, action: Action) {
  return feedReducer(state, action);
}
