import { Component, OnInit } from '@angular/core';
import {
  Author,
  Book,
  Category,
  ServiceResponse,
} from '../types/books.interface';
import { BookService } from '../services/book.service';
import { Observable, delay, filter, map, tap } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css'],
})
export class BooksComponent implements OnInit {
  books?: Observable<Book[]>;
  filteredBooks?: Observable<Book[]>;
  categories?: Observable<Category[]>;
  authors?: Observable<Author[]>;
  editForm: FormGroup = new FormGroup({});
  formMode: string = 'add';
  isLoading: boolean = true;

  constructor(private bookService: BookService, private fb: FormBuilder) {}
  ngOnInit(): void {
    // initializing edit form
    this.editForm = this.fb.group({
      name: ['', Validators.required],
      categoryId: [null, Validators.required],
      authorId: [null, Validators.required],
    });

    //getting authors
    this.authors = this.bookService.getAuthors().pipe(map((res) => res.data));

    //getting categories
    this.categories = this.bookService
      .getCategories()
      .pipe(map((res) => res.data));

    // getting books
    this.books = this.bookService.getBooks().pipe(
      // delay(1000),
      tap(() => (this.isLoading = false)),
      map((res) => res.data)
    );
    this.filteredBooks = this.books;
  }

  // filtering books
  onFilter(category: string, author: string) {
    this.filteredBooks = this.books?.pipe(
      map((books) =>
        books
          .filter((book) => {
            if (category === '') {
              return true;
            }
            return book.category.name === category;
          })
          .filter((book) => {
            if (author === '') {
              return true;
            }
            return book.author.name === author;
          })
      )
    );
  }

  showModal(id: number) {}
}
