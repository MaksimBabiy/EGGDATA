import { Injectable, Injector } from "@angular/core";
import { Router } from "@angular/router"; 
import { HttpClient, HttpHandler, HttpEvent, HttpInterceptor, HttpRequest, HttpResponse, HttpErrorResponse } from "@angular/common/http"; 
import { AuthService } from "./auth.service"; 

import { Observable } from "rxjs";
import { tap, catchError } from "rxjs/operators";


@Injectable() export class AuthResponseInterceptor implements HttpInterceptor {

    currentRequest: HttpRequest<any>;   
    auth: AuthService;

    constructor( private injector: Injector,  private router: Router)  { }

    intercept( request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        this.auth = this.injector.get(AuthService);        
        var token = (this.auth.isLoggedIn()) ? this.auth.getAuth()!.token : null;

         if (token) { 
             this.currentRequest = request;
             
             return next.handle(request)                
                .pipe(tap((event: HttpEvent<any>) => {                    
            	    if (event instanceof HttpResponse) {} 
            }))              
                .pipe(catchError(error => {                    
            	    return this.handleError(error)                
            }));    
                
       }        
       else {            
       	return next.handle(request);        
       }    
  }

    handleError(err: any) {        
    	if (err instanceof HttpErrorResponse) {            
    		if (err.status === 401) {                
    			console.log("Token expired. Attempting refresh...");   
    			this.auth.refreshToken()                    
    			 .subscribe(res => {  
    			 	if (res) {  
    			 		console.log("refresh token successful");

                         var http = this.injector.get(HttpClient);                       
                         http.request(this.currentRequest)
                         .subscribe( result => { }, error => console.error(error));                    
                    }                    
                    else {  
                         console.log("refresh token failed");
                         this.auth.logout();
                         this.router.navigate(["authentication"]);
                         }                
                 }, error => console.log(error));            
    		   }        
    		}        
    		return Observable.throw(err);    
    	} 
    } 