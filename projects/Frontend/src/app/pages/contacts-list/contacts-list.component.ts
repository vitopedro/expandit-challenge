import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Contact } from 'src/app/models/Contact';
import { emptyPaginator } from 'src/app/models/dtos/emptyPaginator';
import { PaginatorDTO } from 'src/app/models/dtos/PaginatorDTO';
import { ContactsService } from 'src/app/services/ContactsService';

@Component({
  selector: 'app-contacts-list',
  templateUrl: './contacts-list.component.html',
  styleUrls: ['./contacts-list.component.scss']
})
export class ContactsListComponent implements OnInit {

  public contacts: PaginatorDTO<Contact> = emptyPaginator;
  public currentPage = 0;

  public form: FormGroup = new FormGroup({
    name: new FormControl(''),
    email: new FormControl(''),
    number: new FormControl(''),
  });

  constructor(
    private contactsService: ContactsService,
    private router: Router
  ) {
    this.search();
  }

  ngOnInit(): void {
  }

  public async search():Promise<void> {
    this.contacts = await this.contactsService.getAll(this.currentPage, this.form.value);
    this.currentPage = this.contacts.currentPage;
  }

  public view(id: number): void {
    this.router.navigate([`contact/view/${id}`]);
  }

  public next() {
    this.currentPage++;
    this.search();
  }

  public previous() {
    this.currentPage--;
    this.search();
  }

  public new() {
    this.router.navigate([`contact/create`]);
  }
}
