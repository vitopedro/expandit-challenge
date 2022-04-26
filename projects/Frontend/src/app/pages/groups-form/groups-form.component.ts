import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Contact } from 'src/app/models/Contact';
import { emptyPaginator } from 'src/app/models/dtos/emptyPaginator';
import { PaginatorDTO } from 'src/app/models/dtos/PaginatorDTO';
import { ContactMinimal, Group } from 'src/app/models/Group';
import { ContactsService } from 'src/app/services/ContactsService';
import { GroupsService } from 'src/app/services/GroupsService';

@Component({
  selector: 'app-groups-form',
  templateUrl: './groups-form.component.html',
  styleUrls: ['./groups-form.component.scss']
})
export class GroupsFormComponent implements OnInit {

  private groupId: number = 0;
  public group?: Group;

  public form: FormGroup = new FormGroup({
    name: new FormControl('', [Validators.required]),
  });

  public contacts: PaginatorDTO<Contact> = emptyPaginator;

  public addedContacts: ContactMinimal[] = [];
  public addedContactIds: number[] = [];

  public contactsCurrentPage = 0;

  public formContact: FormGroup = new FormGroup({
    name: new FormControl(''),
  });

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private groupsService: GroupsService,
    private contactsService: ContactsService
  ) {
    let paramId = this.route.snapshot.paramMap.get('id');
    this.groupId = paramId ? parseInt(paramId) : 0;

    if (this.groupId != 0) {
      this.initGroup();
    }
    this.initContacts();
  }

  private async initGroup(): Promise<void> {
    this.group = await this.groupsService.getOne(this.groupId);
    this.setFormUpdate();
  }

  public async initContacts(): Promise<void> {
    this.contacts = await this.contactsService.getAll(this.contactsCurrentPage, this.formContact.value);
    this.contactsCurrentPage = this.contacts.currentPage;
  }

  private setFormUpdate(): void {
    if (!this.group) {
      return;
    }

    this.addedContacts = [];
    this.addedContactIds = [];

    for (let i = 0; i < this.group.contacts.length; i++) {
      this.addedContacts.push({
        id: this.group.contacts[i]['id'],
        name: this.group.contacts[i]['name']
      });

      this.addedContactIds.push(this.group.contacts[i]['id']);
    }


    this.form = new FormGroup({
      name: new FormControl(this.group.name, [Validators.required]),
    });
  }

  public nextContact() {
    this.contactsCurrentPage++;
    this.initContacts();
  }

  public previousContact() {
    this.contactsCurrentPage--;
    this.initContacts();
  }

  public submit() {
    if (this.form.invalid) {
      alert("Todos os campos são obrigatórios");
      return;
    }

    if (this.groupId == 0) {
      this.create();
    } else {
      this.update();
    }
  }

  public cancel() {
    this.router.navigate(['groups']);
  }

  protected async create() {
    let value = this.form.value;
    value.contacts = this.addedContacts;

    let grupo = await this.groupsService.createOne(value);

    alert("Grupo criado");

    this.router.navigate([`group/view/${grupo.id}`]);
  }

  protected async update() {
    let value = this.form.value;
    value.contacts = this.addedContacts;

    let group = await this.groupsService.updateOne(this.groupId, value);

    alert("Grupo atualizado");

    this.router.navigate([`group/view/${group.id}`]);
  }

  public addContact(c: Contact): void {
    this.addedContacts.push({
      id: c.id,
      name: c.name
    });
    this.addedContactIds.push(c.id);
  }

  public removeContact(c: ContactMinimal): void {
    this.addedContacts = this.addedContacts.filter(con => con.id != c.id);
    this.addedContactIds = this.addedContactIds.filter(con => con != c.id);
  }

  ngOnInit(): void {
  }

}
