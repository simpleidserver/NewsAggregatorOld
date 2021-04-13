import { Action, createReducer, on } from "@ngrx/store";
import { completeGetAllFeeds, completeGetFeed, completeSearchFeeds } from '../actions/feed.actions';
import { DetailedFeed } from "../models/detailedfeed.model";
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
  content: DetailedFeed[];
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

export const initialFeedState: FeedState = {
  content: new Feed(),
  isLoading: true,
  isErrorLoadOccured: false
};

const searchFeedsReducer = createReducer(
  initialSearchFeeds,
  on(completeSearchFeeds, (state, { content }) => ({ content: content, isLoading: false, isErrorLoadOccured: false }))
);

const allFeedsReducer = createReducer(
  initialAllFeeds,
  on(completeGetAllFeeds, (state, { content }) => ({ content: content, isLoading: false, isErrorLoadOccured: false }))
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

export function getFeedReducer(state: FeedState | undefined, action: Action) {
  return feedReducer(state, action);
}
