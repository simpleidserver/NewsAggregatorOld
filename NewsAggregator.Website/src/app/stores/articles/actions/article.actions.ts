import { createAction, props } from '@ngrx/store';
import { SearchArticlesResult } from '../../articles/models/search-article.model';

export const startSearchArticlesInDatasource = createAction("[Articles] START_SEARCH_ARTICLES_IN_DATASOURCE", props<{ order: string, direction: string, count: number, startIndex: number, datasourceId: string }>());
export const errorSearchArticlesInDatasource = createAction("[Articles] ERROR_SEARCH_ARTICLES_IN_DATASOURCE");
export const completeSearchArticlesInDatasource = createAction("[Articles] COMPLETE_SEARCH_ARTICLES_IN_DATASOURCE", props<{ content: SearchArticlesResult}>());
export const startSearchArticlesInFeed = createAction("[Articles] START_SEARCH_ARTICLES_IN_FEED", props<{ order: string, direction: string, count: number, startIndex: number, feedId: string }>());
export const errorSearchArticlesInFeed = createAction("[Articles] ERROR_SEARCH_ARTICLES_IN_FEED");
export const completeSearchArticlesInFeed = createAction("[Articles] COMPLETE_SEARCH_ARTICLES_IN_FEED", props<{ content: SearchArticlesResult }>());
export const startLikeArticle = createAction("[Articles] START_LIKE_ARTICLE", props<{ articleId: string }>());
export const completeLikeArticle = createAction("[Articles] COMPLETE_LIKE_ARTICLE", props<{ articleId: string }>());
export const errorLikeArticle = createAction("[Articles] ERROR_LIKE_ARTICLE");
export const startUnlikeArticle = createAction("[Articles] START_UNLIKE_ARTICLE", props<{ articleId: string }>());
export const completeUnLikeArticle = createAction("[Articles] COMPLETE_UNLIKE_ARTICLE", props<{ articleId: string }>());
export const errorUnLikeArticle = createAction("[Articles] ERROR_UNLIKE_ARTICLE");
export const startReadArticle = createAction("[Articles] START_READ_ARTICLE", props<{ articleId: string }>());
export const completeReadArticle = createAction("[Articles] COMPLETE_READ_ARTICLE", props<{ articleId: string }>());
export const errorReadArticle = createAction("[Articles] ERROR_READ_ARTICLE");
export const startUnreadArticle = createAction("[Articles] START_UNREAD_ARTICLE", props<{ articleId: string }>());
export const completeUnreadArticle = createAction("[Articles] COMPLETE_UNREAD_ARTICLE", props<{ articleId: string }>());
export const errorUnreadArticle = createAction("[Articles] ERROR_UNREAD_ARTICLE");
export const startReadAndHideArticle = createAction("[Articles] START_READ_AND_HIDE_ARTICLE", props<{ articleId: string }>());
export const completeReadAndHideArticle = createAction("[Articles] COMPLETE_READ_AND_HIDE_ARTICLE", props<{ articleId: string }>());
export const errorReadAndHideArticle = createAction("[Articles] ERROR_READ_AND_HIDE_ARTICLE");
export const startSearchRecommendations = createAction("[Articles] START_SEARCH_RECOMMENDATIONS", props<{ count: number, startIndex: number }>());
export const errorSearchRecommendations = createAction("[Articles] ERROR_SEARCH_RECOMMENDATIONS");
export const completeSearchRecommendations = createAction("[Articles] COMPLETE_SEARCH_RECOMMENDATIONS", props<{ content: SearchArticlesResult }>());


