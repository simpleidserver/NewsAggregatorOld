import { createReducer, on } from "@ngrx/store";
import { SearchArticlesResult } from "../../articles/models/search-article.model";
import { completeGetAllFeeds, completeGetFeed, completeSearchFeedArticles, completeSearchFeeds } from '../actions/feed.actions';
import { Feed } from "../models/feed.model";
import { SearchFeedsResult } from "../models/search-feed.model";
export const initialSearchFeeds = {
    content: new SearchFeedsResult(),
    isLoading: true,
    isErrorLoadOccured: false
};
export const initialAllFeeds = {
    content: [],
    isLoading: true,
    isErrorLoadOccured: false
};
export const initialFeedArticleLstState = {
    content: new SearchArticlesResult(),
    isLoading: true,
    isErrorLoadOccured: false
};
export const initialFeedState = {
    content: new Feed(),
    isLoading: true,
    isErrorLoadOccured: false
};
const searchFeedsReducer = createReducer(initialSearchFeeds, on(completeSearchFeeds, (state, { content }) => ({ content: content, isLoading: false, isErrorLoadOccured: false })));
const allFeedsReducer = createReducer(initialAllFeeds, on(completeGetAllFeeds, (state, { content }) => ({ content: content, isLoading: false, isErrorLoadOccured: false })));
const feedArticleLstReducer = createReducer(initialFeedArticleLstState, on(completeSearchFeedArticles, (state, { content }) => ({ content: content, isLoading: false, isErrorLoadOccured: false })));
const feedReducer = createReducer(initialFeedState, on(completeGetFeed, (state, { content }) => ({ content: content, isLoading: false, isErrorLoadOccured: false })));
export function getSearchFeedsReducer(state, action) {
    return searchFeedsReducer(state, action);
}
export function getAllFeedsReducer(state, action) {
    return allFeedsReducer(state, action);
}
export function getFeedArticleLstReducer(state, action) {
    return feedArticleLstReducer(state, action);
}
export function getFeedReducer(state, action) {
    return feedReducer(state, action);
}
//# sourceMappingURL=feed.reducers.js.map