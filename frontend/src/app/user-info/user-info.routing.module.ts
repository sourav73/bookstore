import { RouterModule, Routes } from '@angular/router';
import { UserDetailsComponent } from './user-details/user-details.component';
import { NgModule } from '@angular/core';
import { ProfileComponent } from '../profile/profile.component';
import { UserBooksComponent } from './user-books/user-books.component';
import { BookCategoryComponent } from './book-category/book-category.component';

const routes: Routes = [
  {
    path: '',
    component: ProfileComponent,
    children: [
      { path: '', redirectTo: 'details', pathMatch: 'full' },
      { path: 'details', component: UserDetailsComponent },
      { path: 'category', component: BookCategoryComponent },
      { path: 'books', component: UserBooksComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserInfoRoutingModule {}
