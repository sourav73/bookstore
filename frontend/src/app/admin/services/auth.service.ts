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
  baseUrl: string = 'http://localhost:5293/Auth';
  constructor(private http: HttpClient) {}

  login(userInfo: User) {
    return this.http.post(this.baseUrl + '/login', userInfo);
  }

  refreshToken(token: string) {
    return this.http.post(this.baseUrl + '/refresh-token', {
      refreshToken: token,
    });
  }
}
