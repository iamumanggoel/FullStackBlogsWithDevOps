import { Component, OnInit } from '@angular/core';
import { User } from '../../models/userModels/user.model';
import { PostService } from '../../services/postservices/post.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-info',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-info.component.html',
  styleUrl: './user-info.component.css'
})
export class UserInfoComponent implements OnInit {
  user: User;

  constructor(private postService: PostService) {}

  ngOnInit(): void {
    this.getInfo();
  }

  getInfo(): void {
    this.postService.getUserInfo().subscribe({
      next: (response) => {
        this.user = response;
        console.log("user info", this.user);
      },
      error: (error) => {
        console.error('Error fetching User Details:', error);
      }
    });
  }
}
