import { createAction, props } from '@ngrx/store';
import { DetailedFeed } from '../models/detailedfeed.model';
import { Feed } from '../models/feed.model';
import { SearchFeedsResult } from '../models/search-feed.model';

export class DeleteDatasource {
  constructor(public feedId: string, public datasourceId: string) { }
}

export const startSearchFeeds = createAction("[Feeds] START_SEARCH_FEEDS", props<{ order: string, direction: string, count: number | null, startIndex: number | null, feedTitle: string, datasourceIds: string[], followersFilter: number | null, storiesFilter: number | null, isPaginationEnabled: boolean}>());
export const errorSearchFeeds = createAction("[Feeds] ERROR_SEARCH_FEED");
export const completeSearchFeeds = createAction("[Feeds] COMPLETE_SEARCH_FEEDS", props <{content: SearchFeedsResult}>());
export const startDeleteDatasources = createAction("[Feeds] START_DELETE_DATASOURCES", props<{ parameters: DeleteDatasource[] }>());
export const errorDeleteDatasources = createAction("[Feeds] ERROR_DELETE_DATASOURCES");
export const completeDeleteDatasources = createAction("[Feeds] COMPLETE_DELETE_DATASOURCES");
export const startGetFeed = createAction("[Feeds] START_GET_FEED", props<{ feedId: string }>());
export const errorGetFeed = createAction("[Feeds] ERROR_GET_FEED");
export const completeGetFeed = createAction("[Feeds] COMPLETE_GET_FEED", props<{ content: Feed }>());
export const startAddFeed = createAction("[Feeds] START_ADD_FEED", props<{ feedTitle: string, datasourceIds: string[] }>());
export const errorAddFeed = createAction("[Feeds] ERROR_ADD_FEED");
export const completeAddFeed = createAction("[Feeds] COMPLETE_ADD_FEED", props<{ content: string }>());
export const startGetAllFeeds = createAction("[Feeds] START_GET_ALL_FEEDS");
export const errorGetAllFeeds = createAction("[Feeds] ERROR_GET_ALL_FEEDS");
export const completeGetAllFeeds = createAction("[Feeds] COMPLETE_GET_ALL_FEEDS", props<{ content: DetailedFeed[] }>());
export const startDeleteFeed = createAction("[Feeds] START_DELETE_FEED", props<{ feedId: string }>());
export const completeDeleteFeed = createAction("[Feeds] COMPLETE_DELETE_FEED")();
export const errorDeleteFeed = createAction("[Feeds] ERROR_DELETE_FEED");
