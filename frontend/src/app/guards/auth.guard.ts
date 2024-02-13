import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { PostService } from '../services/postservices/post.service';

export const authGuard: CanActivateFn = (route, state) => {
  if (inject(PostService).isAuthenticated()) {
    return true;
  } else {
    inject(Router).navigate(['/login']);
    return false;
  }
};