import { Contact } from "./contact";
import { User } from "./user";

export class AdminContact extends Contact {
  user!: User;
  userId!: number;
}
