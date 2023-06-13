import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  AddOrUpdateBook,
  Author,
  Book,
  Category,
  ServiceResponse,
} from '../types/books.interface';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  baseUrl: string = 'http://localhost:5293';
  constructor(private http: HttpClient) {}

  addBook(book: AddOrUpdateBook) {
    return this.http.post<ServiceResponse<Book>>(`${this.baseUrl}/Book`, book);
  }

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

  updateBook(book: AddOrUpdateBook) {
    return this.http.put<ServiceResponse<Book>>(`${this.baseUrl}/Book`, book);
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
