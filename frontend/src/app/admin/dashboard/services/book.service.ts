import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  Author,
  Book,
  Category,
  ServiceResponse,
} from '../types/books.interface';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  baseUrl: string = 'http://localhost:5293';
  constructor(private http: HttpClient) {}

  getBooks() {
    return this.http.get<ServiceResponse<Book[]>>(this.baseUrl + '/Book');
  }

  getBookById(id: number) {
    return this.http.get<ServiceResponse<Book>>(`${this.baseUrl}/Book/${id}`);
  }

  deleteBook(id: number) {
    return this.http.delete<ServiceResponse<Book>>(
      `${this.baseUrl}/Book/${id}`
    );
  }

  getCategories() {
    return this.http.get<ServiceResponse<Category[]>>(
      this.baseUrl + '/Category'
    );
  }

  getAuthors() {
    return this.http.get<ServiceResponse<Author[]>>(this.baseUrl + '/Author');
  }
}
