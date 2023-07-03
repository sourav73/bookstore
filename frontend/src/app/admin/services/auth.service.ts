import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

interface User {
  userName: string;
  password: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl: string = 'http://localhost:5293/Auth/login';
  constructor(private http: HttpClient) {}

  login(userInfo: User) {
    return this.http.post(this.baseUrl, userInfo);
  }
}
