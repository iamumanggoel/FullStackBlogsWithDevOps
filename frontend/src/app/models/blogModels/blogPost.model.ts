export class BlogPost {
    constructor(
      public blogId: string,
      public title: string,
      public content: string,
      public publishedDate: Date,
      public author: string,
      public userEmail: string,
      public shortDescription?: string,
      public featuredImageUrl?: string
    ) {}
  }
  