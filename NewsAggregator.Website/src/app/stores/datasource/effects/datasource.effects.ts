import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import {
    completeGetDatasource, errorGetDatasource, startGetDatasource
} from '../actions/datasource.actions';
import { DatasourceService } from '../services/datasource.service';

@Injectable()
export class DatasourcesEffects {
  constructor(
    private actions$: Actions,
    private datasourceService: DatasourceService
  ) { }

  @Effect()
  getDatasource$ = this.actions$
    .pipe(
      ofType(startGetDatasource),
      mergeMap((evt) => {
        return this.datasourceService.get(evt.datasourceId)
          .pipe(
            map(datasource => completeGetDatasource({ content: datasource })),
            catchError(() => of(errorGetDatasource()))
          );
      }
      )
  );
}
