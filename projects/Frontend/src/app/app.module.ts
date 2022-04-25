import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { ContactsListComponent } from './pages/contacts-list/contacts-list.component';
import { ContactsFormComponent } from './pages/contacts-form/contacts-form.component';
import { GroupsListComponent } from './pages/groups-list/groups-list.component';
import { GroupsFormComponent } from './pages/groups-form/groups-form.component';
import { MenuComponent } from './layouts/main-layout/menu/menu.component';
import { ContactsService } from './services/ContactsService';
import { ChallengeApi } from './services/ChallengeApi';
import { HttpClientModule } from '@angular/common/http';
import { ContactsViewComponent } from './pages/contacts-view/contacts-view.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    MainLayoutComponent,
    ContactsListComponent,
    ContactsFormComponent,
    GroupsListComponent,
    GroupsFormComponent,
    MenuComponent,
    ContactsViewComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
  ],
  providers: [
    ContactsService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
