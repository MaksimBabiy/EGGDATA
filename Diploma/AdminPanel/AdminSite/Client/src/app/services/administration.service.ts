import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';



@Injectable()
export class AdministrationService {

  constructor( private http: HttpClient ) { }

  //for patients

  getPatients(){
  	var url = "api/AdminPatients/Get";

  	return this.http.get(url);
  }

  updatePatient(modelPatient : any){
    var url = "api/AdminPatients/UpdatePatient";

    return this.http.patch(url, modelPatient);
  }

  deletePatient(patientId : string){
  	var url = "api/AdminPatients/DeletePatient/"+patientId;

  	return this.http.delete(url);
  }

  uploadGraphPatient(formData : any){
    var url = "api/AdminPatients/UploadFile";

    return this.http.post(url,formData);
  }

  openGraphPatient(patientId : string){
     var url = "api/AdminReader/Get/"+patientId;

     return this.http.get(url);
  }

  addPatient(modelPatient : any){
    var url = "api/AdminPatients/AddPatient";

    return this.http.post(url, modelPatient);
  }

  wave(data : any){
    var url = "api/AdminReader/Wave";

    return this.http.post(url, data);
  }


  
  //for doctors

  getDoctors(){
    var url = "api/AdminDoctors/Get";

    return this.http.get(url);
  }

  updateDoctor(modelDoctor : any){
    var url = "api/AdminDoctors/UpdateDoctor"

    return this.http.patch(url, modelDoctor);
  }

  deleteDoctor(doctorId : string){
    var url = "api/AdminDoctors/DeleteDoctor/"+doctorId;

    return this.http.delete(url);
  }
  addDoctor(doctor : any){
    var url = "api/AdminDoctors/AddDoctor";
    
    return this.http.post(url, doctor);
  }



}
