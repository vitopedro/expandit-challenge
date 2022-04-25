import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Contact } from 'src/app/models/Contact';
import { ContactsService } from 'src/app/services/ContactsService';

@Component({
  selector: 'app-contacts-view',
  templateUrl: './contacts-view.component.html',
  styleUrls: ['./contacts-view.component.scss']
})
export class ContactsViewComponent implements OnInit {

  private contactId: number = 0;
  public contact?: Contact;
  public initials: string = "";
  public hasPhoto: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private contactService: ContactsService
  ) {
    let paramId = this.route.snapshot.paramMap.get('id');
    let id = paramId ? paramId : '0';
    this.contactId = parseInt(id);
    this.initContact();
  }

  private async initContact(): Promise<void> {
    this.contact = await this.contactService.getOne(this.contactId);
    this.setInitials();
  }

  private setInitials(): void {
    this.initials = "PV";
  }

  public edit(): void {
    this.router.navigate([`/contact/update/${this.contactId}`]);
  }

  public back(): void {
    this.router.navigate(['/contacts']);
  }

  public delete(): void {
    if (!confirm("Tem a certeza que quer apagar este contacto? este processo é irreversível")) {
      return;
    }

    this.contactService.deleteOne(this.contactId);

    alert("contacto eliminado");

    this.router.navigate(['/contacts']);
  }

  ngOnInit(): void {
  }

}
