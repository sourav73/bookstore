export interface Book {
  bookId: number;
  name: string;
  author: Author;
  category: Category;
}

export interface AddOrUpdateBook {
  bookId?: number;
  name: string;
  categoryId: number;
  authorId: number;
}
export interface Author {
  authorId: number;
  name: string;
}

export interface Category {
  categoryId: number;
  name: string;
}

export interface AddCategoryOrAuthor {
  name: string;
}

// export interface Category {
//   categoryId: number;
//   name: string;
//   books: Book[] | null;
// }

// export interface Author {
//   authorId: number;
//   name: string;
//   books: Book[] | null;
// }

export interface ServiceResponse<T> {
  data: T;
  message: string;
  success: boolean;
}
