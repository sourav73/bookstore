export interface Author {
  authorId: number;
  name: string;
}

export interface Category {
  authorId: number;
  name: string;
}

export interface Book {
  bookId: number;
  name: string;
  author: Author;
  category: Category;
}

export interface Category {
  categoryId: number;
  name: string;
  books: Book[] | null;
}

export interface Author {
  authorId: number;
  name: string;
  books: Book[] | null;
}

export interface ServiceResponse<T> {
  data: T;
  message: string;
  success: boolean;
}
