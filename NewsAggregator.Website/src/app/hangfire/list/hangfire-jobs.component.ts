import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import * as fromAppState from '@app/stores/appstate';
import * as fromHangfire from '@app/stores/hangfire/actions/hangfire.actions';
import { HangfireJob } from '@app/stores/hangfire/models/hangfirejob.model';
import { select, Store } from '@ngrx/store';
import { merge } from 'rxjs';
import { SearchHangfireJobsResult } from '../../stores/hangfire/models/searchhangfirejobs';

class HangfireJobRow {
  constructor(public id: number, public stateName: string, public createdAt: Date, public invocationData: string) { }
}

@Component({
  selector: 'app-hangfire-jobs',
  templateUrl: './hangfire-jobs.component.html',
  styleUrls: ['./hangfire-jobs.component.sass']
})
export class HangfireJobsComponent implements OnInit, OnDestroy {
  listener: any;
  isLoading: boolean;
  jobs: HangfireJobRow[] = [];
  displayedColumns: string[] = ['id', 'stateName', 'createdAt', 'invocationData'];
  length: number;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  constructor(
    private store: Store<fromAppState.AppState>) { }

  ngOnInit(): void {
    this.listener = this.store.pipe(select(fromAppState.selectHangfireJobsResult)).subscribe((r: SearchHangfireJobsResult | null) => {
      if (!r) {
        return;
      }

      this.isLoading = false;
      this.length = r.count;
      this.jobs = r.content.map((j: HangfireJob) => {
        return new HangfireJobRow(j.id, j.stateName, j.createdAt, JSON.parse(j.invocationData)["Type"]);
      });
    });
    this.refresh();
  }

  launchArticlesExtraction() {
    const request = fromHangfire.startExtractArticles();
    this.store.dispatch(request);
  }

  launchRecommendationExtraction() {
    const request = fromHangfire.startExtractRecommendations();
    this.store.dispatch(request);
  }

  ngAfterViewInit() {
    merge(this.paginator.page).subscribe(() => this.refresh());
  }

  ngOnDestroy(): void {
    if (this.listener) {
      this.listener.unsubscribe();
    }
  }

  refresh() {
      let startIndex: number = 0;
      let count: number = 5;
      if (this.paginator.pageIndex && this.paginator.pageSize) {
        startIndex = this.paginator.pageIndex * this.paginator.pageSize;
      }

      if (this.paginator.pageSize) {
        count = this.paginator.pageSize;
      }

      const request = fromHangfire.startSearchJobs({ startIndex: startIndex, count: count });
      this.store.dispatch(request);
  }
}
