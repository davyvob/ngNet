import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {faUser , faLock} from '@fortawesome/free-solid-svg-icons';
import { MasterService } from '../service/master.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  
  faUser = faUser;
  faLock = faLock;

  errorMessage:any;
  hasError:any;
  isLogged:any;
  isLoading:any;

  loginInputObject = {'email':'Email-adress' , 'password':'password'};


  constructor(private router:Router , private service: MasterService) { }

  ngOnInit(): void {
  }
   
  proceedLogin(email:any , pass:any){
    sessionStorage.setItem('username' , email);
    this.router.navigate(['']);
  }

  clearInputE(){
    this.loginInputObject.email = '';
    
  }

  clearInputP(){
   
    this.loginInputObject.password = '';
  }

  login(){
    
    console.log(this.loginInputObject);
    this.hasError = false;
    this.isLoading = true;
    this.service.logIn(this.loginInputObject.email , this.loginInputObject.password)
                     .subscribe(response => {
                      console.log(response);
                                this.service.handleUserInfo(response.token ,  response.userName);
                                this.isLoading = false;
                                this.isLogged = true;
                                this.router.navigate(['User']);
                                
                      },
                      error => {
                      this.errorMessage = error;
                      this.isLoading = false;
                      this.hasError = true;
                      this.isLogged = false;
                      }
    );
  }

}
