import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  AddCategoryOrAuthor,
  Category,
  ServiceResponse,
} from '../types/books.interface';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  baseUrl: string = 'http://localhost:5293';
  constructor(private http: HttpClient) {}

  addCategory(category: AddCategoryOrAuthor) {
    return this.http.post<ServiceResponse<Category>>(
      `${this.baseUrl}/Category`,
      category
    );
  }

  getCategories() {
    return this.http.get<ServiceResponse<Category[]>>(
      this.baseUrl + '/Category'
    );
  }

  getCategoryById(id: number) {
    return this.http.get<ServiceResponse<Category>>(
      `${this.baseUrl}/Category/${id}`
    );
  }

  deleteCategory(id: number) {
    return this.http.delete<ServiceResponse<Category>>(
      `${this.baseUrl}/Category/${id}`
    );
  }

  updateCategory(category: Category) {
    return this.http.put<ServiceResponse<Category>>(
      `${this.baseUrl}/Category`,
      category
    );
  }
}
