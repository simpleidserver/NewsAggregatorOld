import { HangfireJobState } from "./hangfirejobstate.model";

export class SearchHangfireJobStatesResult {
  constructor() {
    this.content = [];
  }

  startIndex: number;
  count: number;
  totalLength: number;
  content: HangfireJobState[];
}
