import { Action, createReducer, on } from "@ngrx/store";
import { completeSearchJobs, completeSearchJobStates } from "../actions/hangfire.actions";
import { SearchHangfireJobsResult } from "../models/searchhangfirejobs";
import { SearchHangfireJobStatesResult } from "../models/searchhangfirejobstates";

export interface SearchHangfireJobsState {
  isLoading: boolean;
  isErrorLoadOccured: boolean;
  content: SearchHangfireJobsResult;
}

export interface SearchHangfireJobStates {
  isLoading: boolean;
  isErrorLoadOccured: boolean;
  content: SearchHangfireJobStatesResult;
}

export const initialSearchHangfireJobsState: SearchHangfireJobsState = {
  content: new SearchHangfireJobsResult(),
  isLoading: true,
  isErrorLoadOccured: false
};

export const initialSearchHangfireJobStates: SearchHangfireJobStates = {
  content: new SearchHangfireJobStatesResult(),
  isLoading: true,
  isErrorLoadOccured: false
};

const searchHangfireJobsReducer = createReducer(
  initialSearchHangfireJobsState,
  on(completeSearchJobs, (state, { content }) => ({ content: content, isLoading: false, isErrorLoadOccured: false }))
);

const searchHangfireJobStatesReducer = createReducer(
  initialSearchHangfireJobStates,
  on(completeSearchJobStates, (state, { content }) => ({ content: content, isLoading: false, isErrorLoadOccured: false }))
);

export function getSearchHangfireJobsReducer(state: SearchHangfireJobsState | undefined, action: Action) {
  return searchHangfireJobsReducer(state, action);
}

export function getSearchHangfireJobStatesReducer(state: SearchHangfireJobStates | undefined, action: Action) {
  return searchHangfireJobStatesReducer(state, action);
}

