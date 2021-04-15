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
export const startViewArticle = createAction("[Articles] START_VIEW_ARTICLE", props<{ articleId: string }>());
export const completeViewArticle = createAction("[Articles] COMPLETE_VIEW_ARTICLE", props<{ articleId: string }>());
export const errorViewArticle = createAction("[Articles] ERROR_VIEW_ARTICLE");
export const startSearchRecommendations = createAction("[Articles] START_SEARCH_RECOMMENDATIONS", props<{ count: number, startIndex: number }>());
export const errorSearchRecommendations = createAction("[Articles] ERROR_SEARCH_RECOMMENDATIONS");
export const completeSearchRecommendations = createAction("[Articles] COMPLETE_SEARCH_RECOMMENDATIONS", props<{ content: SearchArticlesResult }>());


