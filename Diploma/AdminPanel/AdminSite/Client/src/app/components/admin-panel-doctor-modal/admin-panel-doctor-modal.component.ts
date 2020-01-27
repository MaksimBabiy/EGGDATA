import { Component, OnInit, Inject } from '@angular/core';

import { AdministrationService } from '../../services/administration.service';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';



@Component({
  selector: 'app-admin-panel-doctor-modal',
  templateUrl: './admin-panel-doctor-modal.component.html',
  styleUrls: ['./admin-panel-doctor-modal.component.css']
})
export class AdminPanelDoctorModalComponent implements OnInit {

  constructor( private matDialogRef : MatDialogRef<AdminPanelDoctorModalComponent>,
  			@Inject(MAT_DIALOG_DATA) public data : any,
  			private adminService : AdministrationService ) { }

  ngOnInit() { }

  public close(){
  	this.matDialogRef.close();
  }


  public update(){

  	var model = { 		
  		FirstName : this.data.doctor.firstName,
  		LastName : this.data.doctor.lastName,
  		MiddleName : this.data.doctor.middleName,
  		Age : this.data.doctor.age,
  		Weight : this.data.doctor.weight,
  		Height : this.data.doctor.height,
  		Sex : this.data.doctor.sex,
  		PhoneNumber : this.data.doctor.phoneNumber,
  		HomeNumber : this.data.doctor.homeNumber,
  		Email : this.data.doctor.email,
  		Condition : this.data.doctor.condition
  	}

  	this.adminService.updateDoctor(model)
  		.subscribe(res => {

        window.location.reload();

  		})
  }


  public delete(){

    var DoctorId = this.data.doctor.doctorId;

    this.adminService.deleteDoctor(DoctorId)
      .subscribe(res => {

        window.location.reload();

      })
  }

}
