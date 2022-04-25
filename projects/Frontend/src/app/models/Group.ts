import { Contact } from "./Contact";

export interface Group {
    id: number;
    name: string;
    contacts: ContactMinimal[];
}

export interface ContactMinimal {
    id: number;
    name: string;
    email?: string;
}
