import { Article } from "./article.model";

export class SearchArticlesResult{
  constructor() {
    this.content = [];
  }

  startIndex: number;
  count: number;
  totalLength: number;
  content: Article[];
}
