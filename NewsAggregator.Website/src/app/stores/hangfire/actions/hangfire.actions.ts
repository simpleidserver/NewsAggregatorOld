import { createAction, props } from '@ngrx/store';
import { SearchHangfireJobsResult } from '../models/searchhangfirejobs';
import { SearchHangfireJobStatesResult } from '../models/searchhangfirejobstates';

export class DeleteDatasource {
  constructor(public feedId: string, public datasourceId: string) { }
}

export const startSearchJobs = createAction("[Hangfire] START_SEARCH_JOBS", props<{ count: number, startIndex: number}>());
export const errorSearchJobs = createAction("[Hangfire] ERROR_SEARCH_JOBS");
export const completeSearchJobs = createAction("[Hangfire] COMPLETE_SEARCH_JOBS", props<{ content: SearchHangfireJobsResult }>());
export const startSearchJobStates = createAction("[Hangfire] START_SEARCH_JOB_STATES", props<{ count: number, startIndex: number, jobId: number }>());
export const errorSearchJobStates = createAction("[Hangfire] ERROR_SEARCH_JOB_STATES");
export const completeSearchJobStates = createAction("[Hangfire] COMPLETE_SEARCH_JOB_STATES", props<{ content: SearchHangfireJobStatesResult }>());
export const startExtractArticles = createAction("[Hangfire] START_EXTRACT_ARTICLES");
export const errorExtractArticles = createAction("[Hangfire] ERROR_EXTRACT_ARTICLES");
export const completeExtractArticles = createAction("[Hangfire] COMPLETE_EXTRACT_ARTICLES");
export const startExtractRecommendations = createAction("[Hangfire] START_EXTRACT_RECOMMENDATIONS");
export const errorExtractRecommendations = createAction("[Hangfire] ERROR_EXTRACT_RECOMMENDATIONS");
export const completeExtractRecommendations = createAction("[Hangfire] COMPLETE_EXTRACT_RECOMMENDATIONS");
