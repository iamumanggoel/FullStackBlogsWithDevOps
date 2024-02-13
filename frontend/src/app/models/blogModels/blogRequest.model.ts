export class BlogRequest {
    constructor(
      public title: string,
      public shortDescription?: string,
      public content?: string,
      public featuredImageUrl?: string,
      public publishedDate?: Date,
      public author?: string,
      public userEmail?: string
    ) {}
  }
  