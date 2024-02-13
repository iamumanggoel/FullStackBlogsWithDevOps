import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { PostService } from './services/postservices/post.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, RouterModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Blogger';

  constructor(private postService: PostService, private router: Router){}

  logout(): void {
    this.postService.logout();
    this.router.navigate(['/login']);
  }
}
