import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

import { AdministrationService } from '../../services/administration.service'

@Component({
  selector: 'app-add-doctor',
  templateUrl: './add-doctor.component.html',
  styleUrls: ['./add-doctor.component.css']
})
export class AddDoctorComponent implements OnInit {
 
  public model = {
      FirstName : "",
      LastName : "",
      MiddleName : "",
      Age : "",
      Weight : "",
      Height : "",
      Sex : "",
      PhoneNumber : "",
      HomeNumber : "",
      Email : "",
      Condition : ""
  }

  constructor(private matDialogRef : MatDialogRef<AddDoctorComponent>,
              public administrationService: AdministrationService) { }

  ngOnInit() {
  }

  public Add(){
    this.model = {
      FirstName : this.model.FirstName,
      LastName : this.model.LastName,
      MiddleName : this.model.MiddleName,
      Age : this.model.Age,
      Weight : this.model.Weight,
      Height : this.model.Height,
      Sex : this.model.Sex,
      PhoneNumber : this.model.PhoneNumber,
      HomeNumber : this.model.HomeNumber,
      Email : this.model.Email,
      Condition : this.model.Condition
    }

    this.administrationService.addDoctor(this.model)
    .subscribe((res)=>{
      window.location.reload()
    })
    
  }

  public close(){
    this.matDialogRef.close();
  }
}
