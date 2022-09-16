export class PaginationResponse<T> {
  data!: T[];
  skip!: number;
  take!: number;
  total!: number;
}
