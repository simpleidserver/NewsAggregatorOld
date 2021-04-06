import { Datasource } from "./datasource.model";

export class SearchDatasourcesResult {
  constructor() {
    this.content = [];
  }

  startIndex: number;
  count: number;
  totalLength: number;
  content: Datasource[];
}
