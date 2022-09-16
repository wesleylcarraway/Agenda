import { Enumeration } from "./enumeration";
import { Register } from "./register";


export class Phone extends Register {
  phoneType!: Enumeration;
  phoneTypeId?: number;
  description!: string;
  formattedNumber!: string;
}
