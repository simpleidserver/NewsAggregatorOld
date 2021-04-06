import { Feed } from "./feed.model";

export class SearchFeedsResult {
  constructor() {
    this.content = [];
  }

  startIndex: number;
  count: number;
  totalLength: number;
  content: Feed[];
}
