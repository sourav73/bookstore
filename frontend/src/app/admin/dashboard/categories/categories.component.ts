import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import { Category } from '../types/books.interface';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CategoryService } from '../services/category.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css'],
})
export class CategoriesComponent implements OnInit, OnDestroy {
  categories$?: Observable<Category[]>;
  editForm: FormGroup = new FormGroup({});
  formMode: string = 'edit';
  editCategoryId: number = 0;
  categorySubscription$: Subscription = new Subscription();
  updatedCategorySubscription$: Subscription = new Subscription();
  addedCategorySubscription$: Subscription = new Subscription();
  deleteCategorySubscription$: Subscription = new Subscription();

  constructor(
    private fb: FormBuilder,
    private categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    // initializing edit form
    this.editForm = this.fb.group({
      name: [null, Validators.required],
    });

    //getting categories
    this.categories$ = this.categoryService
      .getCategories()
      .pipe(map((res) => res.data));
  }

  // Showing appropriate modal according to form mode
  showModal(mode: string, id?: number) {
    this.formMode = mode;
    if (mode === 'edit') {
      this.editCategoryId = id!;
      this.categorySubscription$ = this.categoryService
        .getCategoryById(this.editCategoryId!)
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

  // adding or editing category
  submitModalForm(categoryName: string) {
    // editing category
    if (this.formMode === 'edit') {
      this.updatedCategorySubscription$ = this.categoryService
        .updateCategory({
          categoryId: this.editCategoryId!,
          name: categoryName,
        })
        .subscribe(() => {
          this.categories$ = this.categories$?.pipe(
            map((categories) => {
              return categories;
            })
          );
        });
    } else {
      // adding category
      this.addedCategorySubscription$ = this.categoryService
        .addCategory({ name: categoryName })
        .subscribe(() => {
          this.categories$ = this.categories$?.pipe(
            map((categories) => {
              return categories;
            })
          );
        });
    }
  }

  // Deleting category
  onDelete(id: number) {
    this.deleteCategorySubscription$ = this.categoryService
      .deleteCategory(id)
      .subscribe();
    this.categories$ = this.categories$?.pipe(
      map((categories) => {
        return categories.filter((category) => category.categoryId !== id);
      })
    );
  }

  ngOnDestroy(): void {
    this.categorySubscription$.unsubscribe();
    this.addedCategorySubscription$.unsubscribe();
    this.updatedCategorySubscription$.unsubscribe();
    this.deleteCategorySubscription$.unsubscribe();
  }
}
