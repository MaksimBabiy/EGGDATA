import { Input, Component, OnInit, Inject } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators, } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../../services/auth.service';
import { LoadScriptsService } from '../../services/load-scripts.service';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.css']
})

export class LoginComponent {

	form : FormGroup;

	constructor( private router : Router, 
			   private fb : FormBuilder, 
			   private authService : AuthService,
			   public loadScriptService : LoadScriptsService
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
	}

	createForm(){
		this.form = this.fb.group({
			Username: ['', Validators.required],
			Password: ['', Validators.required]
		})
	}

	onSubmit(){
		var username = this.form.value.Username;
		var password = this.form.value.Password;

	
		this.authService.login(username, password)
			 .subscribe(res => {
				
				
				
			 	this.router.navigate(['']);
			 },

			 err => {
			 	console.log(err);
			 	this.form.setErrors({
			 		"auth": "Неверный логин или пароль"
			 	})
			 }
			 )
	}




	getFormControl (name: string) {
		return this.form.get(name);
	}

	isValid (name: string) {
		var e = this.getFormControl(name);
		return e && e.valid;
	}

	isChanged (name : string) {
		var e = this.getFormControl(name);
		return e && (e.dirty || e.touched);
	}

	hasError (name : string) {
		var e = this.getFormControl(name);
		return e && (e.dirty || e.touched) && !e.valid;
	}



}
