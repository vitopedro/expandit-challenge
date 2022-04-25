import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Contact } from 'src/app/models/Contact';
import { ContactsService } from 'src/app/services/ContactsService';

@Component({
  selector: 'app-contacts-form',
  templateUrl: './contacts-form.component.html',
  styleUrls: ['./contacts-form.component.scss']
})
export class ContactsFormComponent implements OnInit {

  private contactId: number = 0;
  public contact?: Contact;

  public numbersGroup: FormGroup[] = [
    new FormGroup({
      number: new FormControl('', [Validators.required]),
      label: new FormControl('', [Validators.required]),
    })
  ];

  public form: FormGroup = new FormGroup({
    name: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    address: new FormControl('', [Validators.required]),
    phoneNumbers: new FormArray(this.numbersGroup),
  });

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private contactService: ContactsService
  ) {
    let paramId = this.route.snapshot.paramMap.get('id');
    this.contactId = paramId ? parseInt(paramId) : 0;

    if (this.contactId != 0) {
      this.initContact();
    }
  }

  private async initContact(): Promise<void> {
    this.contact = await this.contactService.getOne(this.contactId);
    this.setFormUpdate();
  }

  private setFormUpdate(): void {
    if (!this.contact) {
      return;
    }
    this.numbersGroup = [];

    for (let i = 0; i < this.contact.phoneNumbers.length; i++) {
      this.numbersGroup.push(new FormGroup({
        number: new FormControl(this.contact.phoneNumbers[i].number),
        label: new FormControl(this.contact.phoneNumbers[i].label),
      }));
    }

    this.form = new FormGroup({
      name: new FormControl(this.contact.name, [Validators.required]),
      email: new FormControl(this.contact.email, [Validators.required, Validators.email]),
      address: new FormControl(this.contact.address, [Validators.required]),
      phoneNumbers: new FormArray(this.numbersGroup),
    });
  }

  public addNumber() {
    this.numbersGroup.push(
      new FormGroup({
        number: new FormControl('', [Validators.required]),
        label: new FormControl('', [Validators.required]),
      })
    );
    this.form.controls['phoneNumbers'] = new FormArray(this.numbersGroup);
  }

  public removeNumber(index: number) {
    this.numbersGroup.splice(index, 1);
    this.form.controls['phoneNumbers'] = new FormArray(this.numbersGroup);
  }

  public submit() {
    if (this.form.invalid) {
      alert("Todos os campossão obrigatórios");
      return;
    }

    if (this.contactId == 0) {
      this.create();
    } else {
      this.update();
    }
  }

  public cancel() {
    this.router.navigate(['contacts']);
  }

  protected async create() {
    let value = this.form.value;

    let contact = await this.contactService.createOne(value);

    alert("Contacto criado");

    this.router.navigate([`contact/view/${contact.id}`]);
  }

  protected async update() {
    let value = this.form.value;

    let contact = await this.contactService.updateOne(this.contactId, value);

    alert("Contacto atualizado");

    this.router.navigate([`contact/view/${contact.id}`]);
  }

  ngOnInit(): void {
  }

}
