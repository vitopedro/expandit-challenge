import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { emptyPaginator } from 'src/app/models/dtos/emptyPaginator';
import { PaginatorDTO } from 'src/app/models/dtos/PaginatorDTO';
import { Group } from 'src/app/models/Group';
import { GroupsService } from 'src/app/services/GroupsService';

@Component({
  selector: 'app-groups-list',
  templateUrl: './groups-list.component.html',
  styleUrls: ['./groups-list.component.scss']
})
export class GroupsListComponent implements OnInit {

  public groups: PaginatorDTO<Group> = emptyPaginator;
  public currentPage = 0;
  public order = "asc";
  public orderLabel = "Ascendente";

  public form: FormGroup = new FormGroup({
    name: new FormControl(''),
    contact: new FormControl('')
  });

  constructor(
    private groupsService: GroupsService,
    private router: Router
  ) {
    this.search();
  }

  ngOnInit(): void {
  }

  public async search(): Promise<void> {
    this.groups = await this.groupsService.getAll(this.currentPage, this.form.value, this.order);
    this.currentPage = this.groups.currentPage;
  }

  public view(id: number): void {
    this.router.navigate([`group/view/${id}`]);
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
    this.router.navigate([`group/create`]);
  }

  public changeOrder() {
    if (this.order == 'asc') {
      this.order = 'desc';
      this.orderLabel = 'Descendente';
    } else {
      this.order = 'asc';
      this.orderLabel = 'Ascendente';
    }
    this.search();
  }
}
