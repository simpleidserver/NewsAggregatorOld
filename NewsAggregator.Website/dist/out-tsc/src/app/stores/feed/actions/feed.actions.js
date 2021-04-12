import { createAction, props } from '@ngrx/store';
export class DeleteDatasource {
    constructor(feedId, datasourceId) {
        this.feedId = feedId;
        this.datasourceId = datasourceId;
    }
}
export const startSearchFeeds = createAction("[Feeds] START_SEARCH_FEEDS", props());
export const errorSearchFeeds = createAction("[Feeds] ERROR_SEARCH_FEED");
export const completeSearchFeeds = createAction("[Feeds] COMPLETE_SEARCH_FEEDS", props());
export const startDeleteDatasources = createAction("[Feeds] START_DELETE_DATASOURCES", props());
export const errorDeleteDatasources = createAction("[Feeds] ERROR_DELETE_DATASOURCES");
export const completeDeleteDatasources = createAction("[Feeds] COMPLETE_DELETE_DATASOURCES");
export const startSearchFeedArticles = createAction("[Feeds] START_SEARCH_FEED_ARTICLES", props());
export const errorSearchFeedArticles = createAction("[Feeds] ERROR_SEARCH_FEED_ARTICLES");
export const completeSearchFeedArticles = createAction("[Feeds] COMPLETE_SEARCH_FEED_ARTICLES", props());
export const startGetFeed = createAction("[Feeds] START_GET_FEED", props());
export const errorGetFeed = createAction("[Feeds] ERROR_GET_FEED");
export const completeGetFeed = createAction("[Feeds] COMPLETE_GET_FEED", props());
export const startAddFeed = createAction("[Feeds] START_ADD_FEED", props());
export const errorAddFeed = createAction("[Feeds] ERROR_ADD_FEED");
export const completeAddFeed = createAction("[Feeds] COMPLETE_ADD_FEED", props());
export const startGetAllFeeds = createAction("[Feeds] START_GET_ALL_FEEDS");
export const errorGetAllFeeds = createAction("[Feeds] ERROR_GET_ALL_FEEDS");
export const completeGetAllFeeds = createAction("[Feeds] COMPLETE_GET_ALL_FEEDS", props());
//# sourceMappingURL=feed.actions.js.map