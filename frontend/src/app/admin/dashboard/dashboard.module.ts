import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BooksComponent } from './books/books.component';
import { CategoriesComponent } from './categories/categories.component';
import { AuthorsComponent } from './authors/authors.component';
import { DashboardRoutingModule } from './dashboard.routing.module';
import { BookItemComponent } from './books/book-item/book-item.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    BooksComponent,
    CategoriesComponent,
    AuthorsComponent,
    BookItemComponent,
  ],
  imports: [CommonModule, DashboardRoutingModule, ReactiveFormsModule],
})
export class DashboardModule {}
