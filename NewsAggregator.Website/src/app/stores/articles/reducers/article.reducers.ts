import { Action, createReducer, on } from "@ngrx/store";
import { SearchArticlesResult } from "../../articles/models/search-article.model";
import { completeSearchArticlesInDatasource } from '../actions/article.actions';

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

const searchArticlesReducer = createReducer(
  initialSearchArticles,
  on(completeSearchArticlesInDatasource, (state, { content }) => ({ content: content, isLoading: false, isErrorLoadOccured: false }))
);

export function getSearchArticlesReducer(state: SearchArticlesState | undefined, action: Action) {
  return searchArticlesReducer(state, action);
}
