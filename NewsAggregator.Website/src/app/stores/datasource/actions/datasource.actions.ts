import { createAction, props } from '@ngrx/store';
import { Datasource } from '../models/datasource.model';

export const startGetDatasource = createAction("[Datasources] START_GET_DATASOURCE", props<{ datasourceId: string }>());
export const errorGetDatasource = createAction("[Datasources] ERROR_GET_DATASOURCE");
export const completeGetDatasource = createAction("[Datasources] COMPLETE_GET_DATASOURCE", props<{ content: Datasource}>());
