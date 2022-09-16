import { HttpParams } from "@angular/common/http";

export class BaseParams extends HttpParams {
  [key: string]: any;
  take = 6;
  skip = 0;
}
