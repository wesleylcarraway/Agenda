import { MatSnackBar } from "@angular/material/snack-bar";
import { BaseError } from "../http-service/base-error";

export function apiErrorHandler(snackBar: MatSnackBar, error: BaseError) {
  console.error(error);
  snackBar.open(error.errors[0].errorMessage, undefined, { duration: 3000 });
}
