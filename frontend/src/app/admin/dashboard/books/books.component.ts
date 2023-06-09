import { Component, OnInit } from '@angular/core';
import {
  Author,
  Book,
  Category,
  ServiceResponse,
} from '../types/books.interface';
import { BookService } from '../services/book.service';
import { Observable, delay, map } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css'],
})
export class BooksComponent implements OnInit {
  books?: Observable<Book[]>;
  categories?: Observable<Category[]>;
  authors?: Observable<Author[]>;
  editForm: FormGroup = new FormGroup({});

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
    this.authors = this.bookService
      .getCategories()
      .pipe(map((res) => res.data));

    // getting books
    this.books = this.bookService.getBooks().pipe(
      map((res) => res.data),
      delay(0)
    );
  }

  showModal(id: number) {}
}
