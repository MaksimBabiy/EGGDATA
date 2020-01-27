import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Router } from "@angular/router"; 

import { RegisterUserService } from '../../services/register-user.service';
import { LoadScriptsService } from '../../services/load-scripts.service';
import { AuthService } from '../../services/auth.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

	form : FormGroup;

  	constructor( private router : Router, 
			   private fb : FormBuilder, 
			   private registerUserService : RegisterUserService,
			   public loadScriptService : LoadScriptsService,
			   private authService : AuthService,
  		      ) 
  	{
  		this.createForm();
  	}


  	//При формировании компонента удаляется токен из LocalStorage
  	ngOnInit() { 
		this.loadScriptService.loadScript("../../../assets/js/jquery-3.3.1.min.js"); 
       	this.loadScriptService.loadScript("../../../assets/js/materialize.min.js");
        this.loadScriptService.loadScript("../../../assets/js/script-help.js");
		this.authService.logout(); 
		localStorage.removeItem("Name");
	}


	createForm() {        
	 	this.form = this.fb.group({            
	 		Username: ['', Validators.required],            
	 		Email: ['', [Validators.required, Validators.email] ],            
	 		Password: ['', Validators.required],            
	 		PasswordConfirm: ['', Validators.required],                 
	 		}, 
	 		{            
	 			validator: this.passwordConfirmValidator
        		});    
	 }


	 onSubmit(){
	 	var username = this.form.value.Username;
	 	var email = this.form.value.Email;
	 	var password = this.form.value.Password;


	 	this.registerUserService.registration(username, email, password)
	 		.subscribe(res => {
	 			console.log("User " + username + " has been created.");
	 			this.router.navigate(["login"]);
	 		},

	 		err => {
	 			console.log(err);
	 			if(err.error == "Login is already taken")
	 			this.form.setErrors({
			 		"register": "Логин уже существует"
			 	}) 
	 			else if (err.error == "Email is already taken")
	 				this.form.setErrors({
			 		"register": "Электронная почта уже используется"
			 	})
			 	else 
			 		this.form.setErrors({
			 		"register": "Пароль не соответствует требованиям"
			 	})
	 		}
	 		)
	 }


	 passwordConfirmValidator( control: FormControl ):any {  

	   	let p = control.root.get('Password');        
	   	let pc = control.root.get('PasswordConfirm');  

	   	if (p && pc) {            
	   		if (p.value !== pc.value) {                
	   			pc.setErrors(                    
	   				{ "PasswordMismatch": true }                
	   				);            
	   		}            
	   		else {                
	   			pc.setErrors(null);
               }        
        }        
       return null;    
      }



     
   getFormControl(name: string) {        
   	return this.form.get(name);    
   }
       
   isValid(name: string) {        
   	var e = this.getFormControl(name);        
   	return e && e.valid;    
   }
      
   isChanged(name: string) {        
   	var e = this.getFormControl(name);        
   	return e && (e.dirty || e.touched);    
   }
      
   hasError(name: string) {        
   	var e = this.getFormControl(name);        
   	return e && (e.dirty || e.touched) && !e.valid;    
   } 
 
 
}
