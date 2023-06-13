import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import { Author } from '../types/books.interface';
import { AuthorService } from '../services/author.service';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.css'],
})
export class AuthorsComponent implements OnInit, OnDestroy {
  authors$?: Observable<Author[]>;
  editForm: FormGroup = new FormGroup({});
  formMode: string = 'edit';
  editAuthorId: number = 0;
  authorSubscription$: Subscription = new Subscription();
  updatedAuthorSubscription$: Subscription = new Subscription();
  addedAuthorSubscription$: Subscription = new Subscription();
  deleteAuthorSubscription$: Subscription = new Subscription();

  constructor(private fb: FormBuilder, private authorService: AuthorService) {}

  ngOnInit(): void {
    // initializing edit form
    this.editForm = this.fb.group({
      name: [null, Validators.required],
    });

    //getting categories
    this.authors$ = this.authorService
      .getAuthors()
      .pipe(map((res) => res.data));
  }

  // Showing appropriate modal according to form mode
  showModal(mode: string, id?: number) {
    this.formMode = mode;
    if (mode === 'edit') {
      this.editAuthorId = id!;
      this.authorSubscription$ = this.authorService
        .getAuthorById(this.editAuthorId!)
        .pipe(map((response) => response.data))
        .subscribe((res) => {
          this.editForm.setValue({
            name: res?.name,
          });
        });
    } else {
      this.editForm.setValue({
        name: '',
      });
    }
  }

  // adding or editing author
  submitModalForm(authorName: string) {
    // editing author
    if (this.formMode === 'edit') {
      this.updatedAuthorSubscription$ = this.authorService
        .updateAuthor(
          {
            name: authorName,
          },
          this.editAuthorId
        )
        .subscribe(() => {
          this.authors$ = this.authors$?.pipe(
            map((authors) => {
              return authors;
            })
          );
        });
    } else {
      // adding author
      this.addedAuthorSubscription$ = this.authorService
        .addAuthor({ name: authorName })
        .subscribe(() => {
          this.authors$ = this.authors$?.pipe(
            map((authors) => {
              return authors;
            })
          );
        });
    }
  }

  // Deleting author
  onDelete(id: number) {
    this.deleteAuthorSubscription$ = this.authorService
      .deleteAuthor(id)
      .subscribe();
    this.authors$ = this.authors$?.pipe(
      map((authors) => {
        return authors.filter((author) => author.authorId !== id);
      })
    );
  }

  ngOnDestroy(): void {
    this.authorSubscription$.unsubscribe();
    this.addedAuthorSubscription$.unsubscribe();
    this.updatedAuthorSubscription$.unsubscribe();
    this.deleteAuthorSubscription$.unsubscribe();
  }
}
