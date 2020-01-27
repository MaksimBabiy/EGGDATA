import { EventEmitter, Inject, Injectable, PLATFORM_ID } from "@angular/core"; 
import { isPlatformBrowser } from '@angular/common';
import { HttpClient, HttpHeaders } from "@angular/common/http";

import { Observable } from "rxjs";
import { map, catchError} from 'rxjs/operators';


@Injectable()
export class AuthService {

  authKey: string = "auth";    
  clientId: string = "User";


  constructor(private http: HttpClient, @Inject(PLATFORM_ID) private platformId: any) { }

  

  //login 
  login(username: string, password: string) : Observable<boolean>{
  
  	var url = "api/admintoken/auth/";
    
  	var data = {
  			UserName : username,
  			Password : password,
        GrantType : "password",
  			ClientId : this.clientId,

  			scope : "offline_access profile email"
  	};

    
  
  	return this.getAuthFromServer(url,data);

  }

  // logout
  logout() : boolean {
  	this.setAuth(null);
  	return true;
  }


  //refresh token
  refreshToken(): Observable<boolean>{

    var url = "api/admintoken/auth";

    var data = {
      ClientId : this.clientId,
      GrantType : "refresh_token",
      RefreshToken : this.getAuth()!.refreshToken,
      
      scope: "offline_access profile email"
    };

    return this.getAuthFromServer(url,data);

  }

  getAuthFromServer(url: string, data: any) : Observable<boolean>{

    return this.http.post<TokenResponse>(url, data)
      .pipe(map((res) => {
        let token = res && res.token;
        if(token) {
          this.setAuth(res);
       
        
          return true;
        }

        return Observable.throw('Unauthorized');
      }))
      .pipe(catchError(error => {
        return new Observable<any>(error);
      }))

  }


  setAuth(auth : TokenResponse | null) : boolean {
    if (isPlatformBrowser(this.platformId))
      if(auth){
        localStorage.setItem(this.authKey, JSON.stringify(auth));
      }
      else localStorage.removeItem(this.authKey);

      return true;
  }


  getAuth() : TokenResponse | null {
    if(isPlatformBrowser(this.platformId)){
      var i = localStorage.getItem(this.authKey);

      if (i)
       return JSON.parse(i);
    }
  }

  // user check
  isLoggedIn() : boolean {
    if(isPlatformBrowser(this.platformId))
      return localStorage.getItem(this.authKey) != null;
    return false;
  }


}
