import { Action, createReducer, on } from "@ngrx/store";
import { completeGetDatasource } from '../actions/datasource.actions';
import { Datasource } from "../models/datasource.model";

export interface DatasourceState {
  isLoading: boolean;
  isErrorLoadOccured: boolean;
  content: Datasource;
}

export const initialDatasource: DatasourceState = {
  content: new Datasource(),
  isLoading: true,
  isErrorLoadOccured: false
};

const datasourceReducer = createReducer(
  initialDatasource,
  on(completeGetDatasource, (state, { content }) => ({ content: content, isLoading: false, isErrorLoadOccured: false }))
);

export function getDatasourceReducer(state: DatasourceState | undefined, action: Action) {
  return datasourceReducer(state, action);
}
