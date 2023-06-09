import { Component, OnDestroy, OnInit } from '@angular/core';
import { Author, Book, Category } from '../types/books.interface';
import { BookService } from '../services/book.service';
import { Observable, Subscription } from 'rxjs';
import { delay, map, tap } from 'rxjs/operators';

import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css'],
})
export class BooksComponent implements OnInit, OnDestroy {
  books$?: Observable<Book[]>;
  filteredBooks$?: Observable<Book[]>;
  categories$?: Observable<Category[]>;
  authors$?: Observable<Author[]>;
  editForm: FormGroup = new FormGroup({});
  formMode: string = 'edit';
  isLoading: boolean = true;
  editBookId: number | undefined = 0;
  bookSubscription$: Subscription = new Subscription();
  updatedBookSubscription$: Subscription = new Subscription();
  addedBookSubscription$: Subscription = new Subscription();
  deleteBookSubscription$: Subscription = new Subscription();
  constructor(private bookService: BookService, private fb: FormBuilder) {}
  ngOnInit(): void {
    // initializing edit form
    this.editForm = this.fb.group({
      name: [null, Validators.required],
      categoryId: [null, this.dropdownValidator],
      authorId: [null, this.dropdownValidator],
    });

    //getting authors
    this.authors$ = this.bookService.getAuthors().pipe(map((res) => res.data));

    //getting categories
    this.categories$ = this.bookService
      .getCategories()
      .pipe(map((res) => res.data));

    // getting books
    this.books$ = this.bookService.getBooks().pipe(
      // delay(1000),
      tap(() => (this.isLoading = false)),
      map((res) => res.data)
    );
    this.filteredBooks$ = this.books$;
  }

  trackBook(index: number, book: Book) {
    return book.bookId;
    // return index;
  }

  // dropdown validator when value is 0
  dropdownValidator(control: FormControl) {
    const value = control.value;
    if (Number(value) === 0) {
      return { invalidValue: true };
    }
    return null;
  }
  // filtering books
  onFilter(category: string, author: string) {
    let categoryId = Number(category);
    let authorId = Number(author);
    this.filteredBooks$ = this.books$?.pipe(
      map((books) =>
        books
          .filter((book) => {
            if (categoryId === 0) {
              return true;
            }
            return book.category.categoryId === categoryId;
          })
          .filter((book) => {
            if (authorId === 0) {
              return true;
            }
            return book.author.authorId === authorId;
          })
      )
    );
  }

  // Showing appropriate modal according to form mode
  showModal(mode: string, id?: number) {
    this.formMode = mode;
    if (mode === 'edit') {
      this.editBookId = id;
      this.bookSubscription$ = this.bookService
        .getBookById(this.editBookId!)
        .pipe(map((response) => response.data))
        .subscribe((res) => {
          this.editForm.setValue({
            name: res?.name,
            categoryId: res?.category.categoryId,
            authorId: res?.author.authorId,
          });
        });
    } else {
      this.editForm.setValue({
        name: '',
        categoryId: 0,
        authorId: 0,
      });
    }
  }

  // adding or editing book
  submitModalForm(bookForm: FormGroup) {
    // editing book
    this.isLoading = true;
    if (this.formMode === 'edit') {
      this.updatedBookSubscription$ = this.bookService
        .updateBook({ bookId: this.editBookId, ...bookForm.value })
        .subscribe(() => {
          // this.filteredBooks$ = this.bookService
          //   .getBooks()
          //   .pipe(map((res) => res.data));
          this.filteredBooks$ = this.filteredBooks$?.pipe(
            map((books) => {
              return books;
            })
          );
        });
      this.isLoading = false;
    } else {
      // adding book
      this.addedBookSubscription$ = this.bookService
        .addBook(bookForm.value)
        .subscribe(() => {
          this.filteredBooks$ = this.filteredBooks$?.pipe(
            map((books) => {
              return books;
            })
          );
        });
      this.isLoading = false;
    }
  }

  // Deleting book
  onDelete(id: number) {
    this.isLoading = true;
    this.deleteBookSubscription$ = this.bookService.deleteBook(id).subscribe();
    this.filteredBooks$ = this.filteredBooks$?.pipe(
      map((books) => {
        return books.filter((book) => book.bookId !== id);
        // const index = books.findIndex((book) => book.bookId === id);
        // if (index !== -1) {
        //   books.splice(index, 1);
        // }
        // return books;
      })
    );
    this.isLoading = false;
  }

  ngOnDestroy(): void {
    this.bookSubscription$?.unsubscribe();
    this.updatedBookSubscription$.unsubscribe();
    this.addedBookSubscription$.unsubscribe();
    this.deleteBookSubscription$.unsubscribe();
  }
}
