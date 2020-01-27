import { Injectable, Inject } from '@angular/core';
import { HttpClient } from "@angular/common/http";



@Injectable()
export class RegisterUserService {

  constructor( private http: HttpClient ) { }

  registration(username : string, email : string, password : string){

  	var url ="/api/Account/RegisterUser"; 

  	var tempUser = <User>{};
  	tempUser.Email = email;
  	tempUser.Login = username;
  	tempUser.Password = password;

  	return this.http.put<User>(url, tempUser)
  }

}
