import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { ContactsFormComponent } from './pages/contacts-form/contacts-form.component';
import { ContactsListComponent } from './pages/contacts-list/contacts-list.component';
import { ContactsViewComponent } from './pages/contacts-view/contacts-view.component';
import { GroupsFormComponent } from './pages/groups-form/groups-form.component';
import { GroupsListComponent } from './pages/groups-list/groups-list.component';

const routes: Routes = [
  {path: '', component: MainLayoutComponent, children: [
    {path: 'contacts', component: ContactsListComponent},
    {path: 'contact/create', component: ContactsFormComponent},
    {path: 'contact/update/:id', component: ContactsFormComponent},
    {path: 'contact/view/:id', component: ContactsViewComponent},
    {path: 'groups', component: GroupsListComponent},
    {path: 'group/create', component: GroupsFormComponent},
    {path: 'group/update/:id', component: GroupsFormComponent},
    //Wildcard
    {path: '**', redirectTo: 'contacts'}
  ]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
