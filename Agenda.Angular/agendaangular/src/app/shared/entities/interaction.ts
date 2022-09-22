import { Register } from "./register";

export class Interaction extends Register{
  userId!: number;
  interactionTypeId!: number;
  message!: string;
}
