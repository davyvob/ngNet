import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MasterService {
  apiUrlLogin = "https://localhost:5001/api/Accounts/login";
  LoginModel = {email:"" , passWord:""};
  AnswerModel = {answer:''};

  get token() {
    return sessionStorage.getItem('currentToken')
  }
  constructor(private http:HttpClient) { }

  isLoggedIn() {
    return sessionStorage.getItem('currentToken')!=null;
  }

  logIn( email:any , pass: any):Observable<any>{
    this.LoginModel.email = email;
        this.LoginModel.passWord = pass;
        return this.http.post(this.apiUrlLogin , this.LoginModel)
                        .pipe(catchError(this.handleError));
  
  }

  private handleError(error: HttpErrorResponse) {
    let errormessage='Uncaught error in handlerror line38 login-service.ts';
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      return throwError(() => new Error('Server or network issues'));
    } 

    if(error.status === 400){
      //handle bad requests from api call
      return throwError(() => new Error(error.error));
    }

    if(error.status === 401){
      //handle bad requests from api call
      return throwError(() => new Error('Unauthirezed'));
    }

    else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      errormessage +=` Backend returned code ${error.status}, body was: `, error.error;
    }
    errormessage += ' Something bad happened; please try again later.';
    // Return an observable with a user-facing error message.
    return throwError(() => new Error(errormessage));
  }

  handleUserInfo(token:string , name:string){
  
    sessionStorage.setItem("currentToken" , token)
    sessionStorage.setItem("currentUserName" , name )
  }

  getToken(){
    return sessionStorage.getItem('currentToken');
  }

  clearToken() {
    sessionStorage.removeItem('currentToken');
  }
  

  //other service
  getnextforUser():Observable<any>{
   let apiUrlLogin = "https://localhost:5001/api/codechallenge/GetCurrent";
   return this.http.get(apiUrlLogin)
                        .pipe(catchError(this.handleError));

  }

  answerCurrentForUser(answer:any ):Observable<any>{
    let apiUrlLogin = "https://localhost:5001/api/codechallenge/AnswerCurrent";
    this.AnswerModel.answer = answer;
    return this.http.post(apiUrlLogin , this.AnswerModel)
                        .pipe(catchError(this.handleError));
  }
  


}
