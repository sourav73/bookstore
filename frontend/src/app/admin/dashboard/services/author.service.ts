import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  AddCategoryOrAuthor,
  Author,
  ServiceResponse,
} from '../types/books.interface';

@Injectable({
  providedIn: 'root',
})
export class AuthorService {
  baseUrl: string = 'http://localhost:5293';
  constructor(private http: HttpClient) {}

  addAuthor(author: AddCategoryOrAuthor) {
    return this.http.post<ServiceResponse<Author>>(
      `${this.baseUrl}/Author`,
      author
    );
  }

  getAuthors() {
    return this.http.get<ServiceResponse<Author[]>>(this.baseUrl + '/Author');
  }

  getAuthorById(id: number) {
    return this.http.get<ServiceResponse<Author>>(
      `${this.baseUrl}/Author/${id}`
    );
  }

  deleteAuthor(id: number) {
    return this.http.delete<ServiceResponse<Author>>(
      `${this.baseUrl}/Author/${id}`
    );
  }

  updateAuthor(author: AddCategoryOrAuthor, id: number) {
    return this.http.put<ServiceResponse<Author>>(
      `${this.baseUrl}/Author/${id}`,
      author
    );
  }
}
