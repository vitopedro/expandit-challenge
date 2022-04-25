import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Group } from 'src/app/models/Group';
import { GroupsService } from 'src/app/services/GroupsService';

@Component({
  selector: 'app-groups-view',
  templateUrl: './groups-view.component.html',
  styleUrls: ['./groups-view.component.scss']
})
export class GroupsViewComponent implements OnInit {

  private groupId: number = 0;
  public group?: Group;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private groupsService: GroupsService
  ) {
    let paramId = this.route.snapshot.paramMap.get('id');
    let id = paramId ? paramId : '0';
    this.groupId = parseInt(id);
    this.initGroup();
  }

  private async initGroup(): Promise<void> {
    this.group = await this.groupsService.getOne(this.groupId);
  }

  public edit(): void {
    this.router.navigate([`/group/update/${this.groupId}`]);
  }

  public back(): void {
    this.router.navigate(['/groups']);
  }

  public delete(): void {
    if (!confirm("Tem a certeza que quer apagar este grupo? este processo é irreversível")) {
      return;
    }

    this.groupsService.deleteOne(this.groupId);

    alert("grupo eliminado");

    this.router.navigate(['/groups']);
  }

  ngOnInit(): void {
  }

}
