import { Component } from '@angular/core';
import { BlogRequest } from '../../models/blogModels/blogRequest.model';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PostService } from '../../services/postservices/post.service';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-post',
  standalone: true,
  imports: [ ReactiveFormsModule, FormsModule, CommonModule ],
  templateUrl: './add-post.component.html',
  styleUrl: './add-post.component.css'
})
export class AddPostComponent {
  addPostForm: FormGroup;
  selectedFile: File;
  imagePreview: SafeUrl;

  constructor(private fb: FormBuilder, private router: Router, private postService: PostService,private sanitizer: DomSanitizer) {
    this.addPostForm = this.fb.group({
      title: ['', Validators.required],
      shortDescription: ['', Validators.required],
      content: ['', Validators.required],
      featuredImageUrl: ['', Validators.required],
      author: ['TEST', Validators.required],
      userEmail: ['test@test.com'], 
    });
  }

  onSubmit() {
    if (this.addPostForm.valid && this.selectedFile) {
      const blogAdd: BlogRequest = this.addPostForm.value;
      blogAdd.publishedDate = new Date();
  
      this.postService.uploadImage(this.selectedFile).subscribe({
        next: (imageResponse) => {
          console.log('Image uploaded successfully:', imageResponse);
          blogAdd.featuredImageUrl = this.postService.getFullImageUrl(imageResponse.filePath);
          this.postService.addBlogPost(blogAdd).subscribe({
            next: (value) => {
              console.log('Blog post added successfully:', value);
              this.router.navigate(['/blogposts']);
            },
            error: (error) => {
              console.error('Failed to add blog post:', error);
            }
          });
        },
        error: (imageError) => {
          console.error('Failed to upload image:', imageError);
        }
      });
    }
  }
  
  onFileChange(event: any) {
    const file = event.target.files[0];
    this.selectedFile = file;
  
    this.addPostForm.patchValue({
      featuredImageUrl: file ? file.name : ''
    });
  
    this.imagePreview = file ? this.sanitizer.bypassSecurityTrustUrl(URL.createObjectURL(file)) : null;
  }
}