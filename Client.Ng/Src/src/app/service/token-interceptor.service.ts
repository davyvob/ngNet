import { Injectable } from '@angular/core';
import { HttpEvent ,HttpHandler ,HttpInterceptor ,HttpRequest} from '@angular/common/http';
import { Observable } from 'rxjs';
import { MasterService } from './master.service';

@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService implements HttpInterceptor  {

  constructor(private service: MasterService) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
   
   

    let jwttoken = req.clone({
      setHeaders:{
        Authorization:'bearer ' + this.service.getToken()
      }
    })
    
    return next.handle(jwttoken);
  }
}
