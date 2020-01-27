import { Component, OnInit, Inject } from '@angular/core';
import { HttpRequest, HttpClient, HttpEventType } from '@angular/common/http';
import { Http, RequestOptions, Headers, Response } from '@angular/http';

import { AdministrationService } from '../../services/administration.service';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';



@Component({
  selector: 'app-admin-panel-patient-modal',
  templateUrl: './admin-panel-patient-modal.component.html',
  styleUrls: ['./admin-panel-patient-modal.component.css']
})
export class AdminPanelPatientModalComponent implements OnInit {


  public warning : string;
  public dis : boolean = false; 

  constructor( private matDialogRef : MatDialogRef<AdminPanelPatientModalComponent>,
  			       @Inject(MAT_DIALOG_DATA) public data : any,
  			       private adminService : AdministrationService,
               public http: HttpClient )   { }

  ngOnInit() { }

  public close(){
  	this.matDialogRef.close();
  }

  
  public update(){

  	var model = { 		
  		FirstName : this.data.patient.firstName,
  		LastName : this.data.patient.lastName,
  		MiddleName : this.data.patient.middleName,
  		Age : this.data.patient.age,
  		Weight : this.data.patient.weight,
  		Height : this.data.patient.height,
  		Sex : this.data.patient.sex,
  		PhoneNumber : this.data.patient.phoneNumber,
  		HomeNumber : this.data.patient.homeNumber,
  		Email : this.data.patient.email,
  		Condition : this.data.patient.condition
  	}

  	this.adminService.updatePatient(model)
  	.subscribe(res => { 	 

  		window.location.reload(); 


  	})  
  	
  }
  

  public delete(){

    var PatientId = this.data.patient.patientId;

    this.adminService.deletePatient(PatientId)
      .subscribe(res => {

        window.location.reload();

      })
  }


  //Загрузка графика
  public uploadGraph(files){

    this.dis = true;

    if(files.length == 0){
      this.warning = "";
      this.dis = false;
      return;
    }


    const formData = new FormData();
    for (let file of files){
       formData.append(file.name, file);}

    this.adminService.uploadGraphPatient(formData)
      .subscribe(res => {

        if(res.toString() == "Exception,Тільки файл з розширенням dat!"){
           this.warning = "Только файл с расширением dat!";
           this.dis = true;
         }
        else {
          this.warning = "";
          this.dis = false;
          console.log("Файл загружен"); 
        }

      }) 
   
  }

}
