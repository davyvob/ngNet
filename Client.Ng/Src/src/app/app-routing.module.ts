import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { Error401Component } from './error401/error401.component';
import { GamescreenComponent } from './gamescreen/gamescreen.component';
import { AuthGuard } from './guard/auth.guard';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { UserComponent } from './user/user.component';

const routes: Routes = [
  {component: HomeComponent , path:"" },
  {component: AboutComponent , path:"About" },
  {component: LoginComponent , path:"Login" },
  {component: GamescreenComponent , path:"GameScreen"   },
  {component: UserComponent , path:"User" ,  canActivate:[AuthGuard] },
  {component: Error401Component , path:"**" }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
