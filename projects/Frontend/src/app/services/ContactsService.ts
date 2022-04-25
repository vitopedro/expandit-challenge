import { Injectable } from "@angular/core";
import { Contact } from "../models/Contact";
import { PaginatorDTO } from "../models/dtos/PaginatorDTO";
import { ChallengeApi } from "./ChallengeApi";

@Injectable()
export class ContactsService extends ChallengeApi {

    public getAll(page: number = 0, search:{[param:string]:string} = {}): Promise<PaginatorDTO<Contact>> {
        search['currentPage'] = page.toString();
        return this.get<PaginatorDTO<Contact>>("/contacts", search);
    }

    public getOne(id: number): Promise<Contact> {
        return this.get<Contact>(`/contacts/${id}`);
    }

    public createOne(contact: Contact): Promise<Contact> {
        return this.post<Contact>("/contacts", contact);
    }

    public updateOne(id: number, contact: Contact): Promise<Contact> {
        return this.put<Contact>(`/contacts/${id}`, contact);
    }

    public deleteOne(id: number): Promise<void> {
        return this.delete<void>(`/contacts/${id}`);
    }
}