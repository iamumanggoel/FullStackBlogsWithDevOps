import { CommonModule, DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ActivatedRoute, Router, RouterModule, RouterOutlet } from '@angular/router';
import { BlogPost } from '../../models/blogModels/blogPost.model';
import { CommentRequest } from '../../models/commentModels/commentRequest.model';
import { PostService } from '../../services/postservices/post.service';
import { BlogRequest } from '../../models/blogModels/blogRequest.model';
import { CommaExpr } from '@angular/compiler';
import { comment } from '../../models/commentModels/comment.model';

@Component({
  selector: 'app-blog-details',
  standalone: true,
  imports: [ DatePipe, FormsModule, RouterModule, CommonModule, RouterOutlet, ReactiveFormsModule ],
  templateUrl: './blog-details.component.html',
  styleUrl: './blog-details.component.css'
})
export class BlogDetailsComponent implements OnInit {
  blogPost: BlogRequest = new BlogRequest('', '', '', '', new Date(), '', '');
  featuredImageUrl: SafeResourceUrl;
  comments: comment[] = [];
  showComments: boolean = false;
  showAddCommentForm: boolean = false;
  commentForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private sanitizer: DomSanitizer, private postService: PostService) { }

  ngOnInit(): void {
    
    const postId = this.route.snapshot.paramMap.get('id');
    this.postService.getBlogPostById(postId).subscribe(blogPost => {
      this.blogPost = blogPost;

      this.featuredImageUrl = this.sanitizer.bypassSecurityTrustResourceUrl(blogPost.featuredImageUrl);
    });
  }

  viewComments() {
    const postId = this.route.snapshot.paramMap.get('id');
    this.postService.getComments(postId).subscribe(comments => {
      this.comments = comments;
      this.showComments = this.showComments ? false : true;
      console.log('Comments:', comments);
    });
  }

  toggleAddCommentForm() {
    this.showAddCommentForm = !this.showAddCommentForm;

    if(this.showAddCommentForm){
      this.commentForm = this.formBuilder.group({
        commentText: ['', Validators.required], 
        userEmail: ['test@test.co'],
        Name: ['test']
      });
    }
  }
  submitComment() {
    if (this.commentForm.valid) {
      const postId = this.route.snapshot.paramMap.get('id');
      const commentData: CommentRequest = this.commentForm.value;
      commentData.blogId = postId;

      this.postService.addComment(commentData, commentData.blogId).subscribe(() => {
        this.showAddCommentForm = !this.showAddCommentForm;
        this.showComments = false;
        this.viewComments();
      });
    }
  }

}