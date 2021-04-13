import { DetailedFeed } from "./detailedfeed.model";

export class SearchFeedsResult {
  constructor() {
    this.content = [];
  }

  startIndex: number;
  count: number;
  totalLength: number;
  content: DetailedFeed[];
}
