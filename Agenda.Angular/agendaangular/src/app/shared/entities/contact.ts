import { Phone } from "./phone";
import { Register } from "./register";

export class Contact extends Register {
  name!: string;
  phones!: Phone[];
}
