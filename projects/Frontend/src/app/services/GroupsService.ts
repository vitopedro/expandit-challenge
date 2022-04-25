import { Injectable } from "@angular/core";
import { PaginatorDTO } from "../models/dtos/PaginatorDTO";
import { Group } from "../models/Group";
import { ChallengeApi } from "./ChallengeApi";

@Injectable()
export class GroupsService extends ChallengeApi {

    public getAll(page: number = 0, search:{[param:string]:string} = {}): Promise<PaginatorDTO<Group>> {
        search['currentPage'] = page.toString();
        return this.get<PaginatorDTO<Group>>("/groups", search);
    }

    public getOne(id: number): Promise<Group> {
        return this.get<Group>(`/groups/${id}`);
    }

    public createOne(contact: Group): Promise<Group> {
        return this.post<Group>("/groups", contact);
    }

    public updateOne(id: number, contact: Group): Promise<Group> {
        return this.put<Group>(`/groups/${id}`, contact);
    }

    public deleteOne(id: number): Promise<void> {
        return this.delete<void>(`/groups/${id}`);
    }
}
