import { SearchingForm } from "./searching-form";

export class SearchingInputConfig {
  params!: SearchingInputParams[];
  searchAction!: (query: SearchingForm) => void;
}

class SearchingInputParams {
  name!: string;
  label!: string
}
