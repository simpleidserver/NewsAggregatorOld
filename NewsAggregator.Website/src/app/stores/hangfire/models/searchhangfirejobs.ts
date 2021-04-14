import { HangfireJob } from "./hangfirejob.model";

export class SearchHangfireJobsResult {
  constructor() {
    this.content = [];
  }

  startIndex: number;
  count: number;
  totalLength: number;
  content: HangfireJob[];
}
