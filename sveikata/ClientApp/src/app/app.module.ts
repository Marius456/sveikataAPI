import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { DiseasesComponent } from './components/diseases/diseases.component';
import {ReactiveFormsModule} from '@angular/forms';
import { ServicesComponent } from './components/services/services.component';
import { CommentsComponent } from './components/comments/comments.component';
import { UsersComponent } from './components/users/users.component';
import { AngularSvgIconModule } from 'angular-svg-icon';
import { FooterComponent } from './nav-menu/footer.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    DiseasesComponent,
    ServicesComponent,
    CommentsComponent,
    UsersComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    AngularSvgIconModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'diseases', component: DiseasesComponent },
      { path: 'services', component: ServicesComponent },
      { path: 'comments', component: CommentsComponent },
      { path: 'users', component: UsersComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
