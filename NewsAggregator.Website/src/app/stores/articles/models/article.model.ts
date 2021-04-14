export class Article {
  id: string;
  externalId: string;
  title: string;
  summary: string;
  language: string;
  publishDate: Date;
  datasourceId: string;
  datasourceTitle: string;
  nbViews: number;
  nbLikes: number;
  likeActionDateTime: Date | null;
}
