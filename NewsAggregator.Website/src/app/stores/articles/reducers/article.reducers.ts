import { Action, createReducer, on } from "@ngrx/store";
import { SearchArticlesResult } from "../../articles/models/search-article.model";
import { completeLikeArticle, completeReadAndHideArticle, completeReadArticle, completeSearchArticlesInDatasource, completeSearchArticlesInFeed, completeSearchRecommendations, completeUnLikeArticle, completeUnreadArticle } from '../actions/article.actions';
import { Article } from "../models/article.model";

export interface SearchArticlesState {
  isLoading: boolean;
  isErrorLoadOccured: boolean;
  content: SearchArticlesResult;
}

export const initialSearchArticles: SearchArticlesState = {
  content: new SearchArticlesResult(),
  isLoading: true,
  isErrorLoadOccured: false
};

function getArticle(state: SearchArticlesState, articleId: string): { index: number, article: Article} {
  let index: number = 0;
  let art: Article = new Article();
  state.content.content.forEach((a, i) => {
    if (a.id === articleId) {
      index = i;
      art = a;
    }
  });

  return {
    index: index,
    article: art
  };
}

function copy(state: SearchArticlesState) {
  const result = {
    ...state,
    content: {
      ...state.content,
      content: [...state.content.content]
    }
  }

  return result;
}

const searchArticlesReducer = createReducer(
  initialSearchArticles,
  on(completeReadAndHideArticle, (state, { articleId }) => {
    let record = getArticle(state, articleId);
    const result = copy(state);
    result.content.content.splice(record.index, 1);
    return result;
  }),
  on(completeReadArticle, (state, { articleId }) => {
    const result = copy(state);
    result.content.content = result.content.content.map((art) => {
      if (art.id !== articleId) {
        return art;
      }

      const result = { ...art };
      result.nbRead++;
      result.readActionDateTime = new Date();
      return result;
    });
    return result;
  }),
  on(completeUnreadArticle, (state, { articleId }) => {
    const result = copy(state);
    result.content.content = result.content.content.map((art) => {
      if (art.id !== articleId) {
        return art;
      }

      const result = { ...art };
      result.nbRead--;
      result.readActionDateTime = null;
      return result;
    });
    return result;
  }),
  on(completeLikeArticle, (state, { articleId }) => {
    const result = copy(state);
    result.content.content = result.content.content.map((art) => {
      if (art.id !== articleId) {
        return art;
      }

      const result = { ...art };
      result.nbLikes++;
      result.likeActionDateTime = new Date();
      return result;
    });
    return result;
  }),
  on(completeUnLikeArticle, (state, { articleId }) => {
    const result = copy(state);
    result.content.content = result.content.content.map((art) => {
      if (art.id !== articleId) {
        return art;
      }

      const result = { ...art };
      result.nbLikes--;
      result.likeActionDateTime = null;
      return result;
    });
    return result;
  }),
  on(completeSearchArticlesInDatasource, (state, { content }) => ({ content: content, isLoading: false, isErrorLoadOccured: false })),
  on(completeSearchArticlesInFeed, (state, { content }) => ({ content: content, isLoading: false, isErrorLoadOccured: false })),
  on(completeSearchRecommendations, (state, { content }) => ({ content: content, isLoading: false, isErrorLoadOccured: false }))
);

export function getSearchArticlesReducer(state: SearchArticlesState | undefined, action: Action) {
  return searchArticlesReducer(state, action);
}
