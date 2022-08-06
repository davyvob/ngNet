import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NavComponent } from './nav/nav.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { Error401Component } from './error401/error401.component';
import { GamescreenComponent } from './gamescreen/gamescreen.component';
import { UserComponent } from './user/user.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptorService } from './service/token-interceptor.service';
import {FormsModule} from '@angular/forms';
import {NgChartsModule} from 'ng2-charts';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    LoginComponent,
    HomeComponent,
    AboutComponent,
    Error401Component,
    GamescreenComponent,
    UserComponent,
    
  
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    FontAwesomeModule,
    HttpClientModule,
    FormsModule,
    NgChartsModule
  ],
  providers: [{provide:HTTP_INTERCEPTORS, useClass:TokenInterceptorService, multi:true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
