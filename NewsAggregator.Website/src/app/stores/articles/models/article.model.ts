export class Article {
  id: string;
  externalId: string;
  title: string;
  summary: string;
  language: string;
  publishDate: Date;
  datasourceId: string;
  datasourceTitle: string;
  nbRead: number;
  nbLikes: number;
  likeActionDateTime: Date | null;
  readActionDateTime: Date | null;
}
