import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { LoginRequest } from '../../models/userModels/loginUser.model';
import { RegisterUser } from '../../models/userModels/registerUser.model';
@Injectable({
  providedIn: 'root'
})
export class AuthUserService {
  private apiBaseUrl = environment.userBaseUrl;

  constructor(private httpClient: HttpClient) { }

  registerUser(formData: RegisterUser): Observable<any> {
    return this.httpClient.post(`${this.apiBaseUrl}/api/Users/register`, formData);
  }

  loginUser(formData: LoginRequest): Observable<any> {
    return this.httpClient.post(`${this.apiBaseUrl}/api/Users/login`, formData);
  }
}

