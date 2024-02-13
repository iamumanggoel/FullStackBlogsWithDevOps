import { Routes } from '@angular/router';
import { AddPostComponent } from './components/add-post/add-post.component';
import { BlogDetailsComponent } from './components/blog-details/blog-details.component';
import { BlogPostsComponent } from './components/blog-posts/blog-posts.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { authGuard } from './guards/auth.guard';
import { UserInfoComponent } from './components/user-info/user-info.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'blogposts', component: BlogPostsComponent, canActivate: [authGuard] },
    { path: 'blogposts/:id', component: BlogDetailsComponent, canActivate: [authGuard] },
    { path: 'add-post', component: AddPostComponent, canActivate: [authGuard] },
    { path: 'profile', component: UserInfoComponent, canActivate: [authGuard]}, 
    { path: '', redirectTo: '/blogposts', pathMatch: 'full' },
    { path: '**', redirectTo: '/blogposts' },
];

