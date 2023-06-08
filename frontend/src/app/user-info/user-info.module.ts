import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserDetailsComponent } from './user-details/user-details.component';
import { UserInfoRoutingModule } from './user-info.routing.module';
import { ProfileComponent } from '../profile/profile.component';
import { UserBooksComponent } from './user-books/user-books.component';
import { BookCategoryComponent } from './book-category/book-category.component';

@NgModule({
  declarations: [ProfileComponent, UserDetailsComponent, UserBooksComponent, BookCategoryComponent],
  imports: [CommonModule, UserInfoRoutingModule],
})
export class UserInfoModule {}
