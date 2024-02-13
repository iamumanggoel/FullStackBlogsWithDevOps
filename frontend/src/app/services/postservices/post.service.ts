import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { BlogPost } from '../../models/blogModels/blogPost.model';
import { BlogRequest } from '../../models/blogModels/blogRequest.model';
import { comment } from '../../models/commentModels/comment.model';
import { CommentRequest } from '../../models/commentModels/commentRequest.model';
import { User } from '../../models/userModels/user.model';

@Injectable({
  providedIn: 'root'
})
export class PostService  {
  private blogapiBaseUrl = environment.blogBaseUrl;
  private userapiBaseUrl = environment.userBaseUrl;
  private commetnapiBaseUrl = environment.commentBaseUrl;
  private imageapiBaseUrl = environment.imageBaseUrl;
  private accessToken: string | null = null;

  constructor(private http: HttpClient) { }

  setAccessToken(token: string): void {
    this.accessToken = token;
  }

  getAccessToken(): string | null {
    return this.accessToken;
  }

  isAuthenticated(): boolean {
    return !!this.accessToken;
  }

  logout(): void {
    this.accessToken = null;
  }
 
  private getHeaders(): HttpHeaders {
    const token = this.getAccessToken();

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.accessToken}`
    });
    return headers;
  }

  getUserInfo(): Observable<User>{
    const headers = this.getHeaders();
    return this.http.get<any>(`${this.userapiBaseUrl}/getInfo`, { headers });
  }
  
  getBlogPosts(): Observable<BlogPost[]> {
    const headers = this.getHeaders();
    return this.http.get<BlogPost[]>(`${this.blogapiBaseUrl}/api/Blogs`, { headers });
  }

  getBlogPostById(id: string): Observable<BlogPost> {
    const headers = this.getHeaders();
    return this.http.get<BlogPost>(`${this.blogapiBaseUrl}/api/Blogs/${id}`, { headers });
  }

  addBlogPost(blogAdd: BlogRequest): Observable<BlogRequest> {
    const headers = this.getHeaders();
    return this.http.post<BlogPost>(`${this.blogapiBaseUrl}/api/Blogs`, blogAdd, { headers });
  }

  getComments(blogId: string): Observable<comment[]>{
    const headers = this.getHeaders();
    return this.http.get<comment[]>(`${this.commetnapiBaseUrl}/api/Comments/${blogId}`, { headers })
  }

  addComment(comment:CommentRequest, blogId: string): Observable<CommentRequest>{
    const headers = this.getHeaders();
    return this.http.post<CommentRequest>(`${this.commetnapiBaseUrl}/api/Comments/${blogId}`, comment, { headers })
  }

  uploadImage(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file, file.name);
    return this.http.post(`${this.imageapiBaseUrl}/api/Images`, formData);
  }

  getFullImageUrl(path: string): string {
    const baseUrl = this.imageapiBaseUrl;
    return path.replace(/^\/app/, baseUrl);
  }
}
