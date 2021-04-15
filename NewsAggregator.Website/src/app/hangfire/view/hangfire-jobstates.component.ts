import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { ActivatedRoute } from '@angular/router';
import * as fromAppState from '@app/stores/appstate';
import * as fromHangfire from '@app/stores/hangfire/actions/hangfire.actions';
import { HangfireJobState } from '@app/stores/hangfire/models/hangfirejobstate.model';
import { SearchHangfireJobStatesResult } from '@app/stores/hangfire/models/searchhangfirejobstates';
import { select, Store } from '@ngrx/store';
import { merge } from 'rxjs';

@Component({
  selector: 'app-hangfire-jobstates',
  templateUrl: './hangfire-jobstates.component.html',
  styleUrls: ['./hangfire-jobstates.component.sass']
})
export class HangfireJobStatesComponent implements OnInit, OnDestroy {
  listener: any;
  isLoading: boolean;
  jobStates: HangfireJobState[] = [];
  displayedColumns: string[] = ['name', 'reason', 'createdAt'];
  length: number;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  constructor(
    private store: Store<fromAppState.AppState>,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.listener = this.store.pipe(select(fromAppState.selectHangfireJobStatesResult)).subscribe((r: SearchHangfireJobStatesResult | null) => {
      if (!r) {
        return;
      }

      this.isLoading = false;
      this.length = r.count;
      this.jobStates = r.content;
    });
    this.refresh();
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

      const jobId = this.activatedRoute.snapshot.params['id'];
      const request = fromHangfire.startSearchJobStates({ startIndex: startIndex, count: count, jobId: jobId });
      this.store.dispatch(request);
  }
}
