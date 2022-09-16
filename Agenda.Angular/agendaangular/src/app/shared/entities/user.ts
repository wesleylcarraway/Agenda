import { Enumeration } from "./enumeration";
import { Register } from "./register";

export class User extends Register{
  name!: string;
  username!: string;
  password?: string;
  email!: string;
  userRole!: Enumeration;
  userRoleId?: number;
}
