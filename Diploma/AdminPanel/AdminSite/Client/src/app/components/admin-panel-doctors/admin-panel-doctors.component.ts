import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import {AddDoctorComponent} from '../../components/add-doctor/add-doctor.component';
import { LoadScriptsService } from '../../services/load-scripts.service';

import { Doctor } from '../../models/doctor.model';
import { AdministrationService } from '../../services/administration.service';

import { AdminPanelDoctorModalComponent } from '../admin-panel-doctor-modal/admin-panel-doctor-modal.component';
import { from } from 'rxjs';



@Component({
  selector: 'app-admin-panel-doctors',
  templateUrl: './admin-panel-doctors.component.html',
  styleUrls: ['./admin-panel-doctors.component.css']
})
export class AdminPanelDoctorsComponent implements OnInit {

  doctors : Array<Doctor>;	
  timeout : any;

  constructor( public loadScriptService : LoadScriptsService, 
               private adminService : AdministrationService, 
               public dialog : MatDialog) 
  { 
    this.loadDoctors();
    this.loadPagination();
  }

  ngOnInit() {
  	    this.loadScriptService.loadScript("../../../assets/js/jquery-3.3.1.min.js"); 
        this.loadScriptService.loadScript("../../../assets/js/materialize.min.js");
        this.loadScriptService.loadScript("../../../assets/js/script-help.js");
        this.loadScriptService.loadScript("../../../assets/js/search.js");
  }

  public loadDoctors(){
  	this.adminService.getDoctors()
            .subscribe((data: Doctor[]) => {             
                this.doctors = data;
            });
  }
  public loadPagination(){
    this.timeout = setTimeout(()=>{
      this.Pagination();
    }, 400)
  }
   public Pagination(){
     let dia = this.dialog
     let pat = this.doctors

     function openModal(doctorID){
      var doctor_model : any;
      for (let i of pat)
        if(i.doctorId == doctorID)
            {
              doctor_model = i;
              break;
            }
      dia.open(AdminPanelDoctorModalComponent, { height: '1080px', width: '1920',
        data : { doctor : doctor_model }
    });
   }
     
     let noteOnPage = 5;
     let table = document.querySelector('.parent_doc');
     let pagin = document.querySelector('.pagination');
     let countOfItems = Math.ceil( pat.length / noteOnPage);
     let items = [];
     for(let i = 1; i <= countOfItems; i++){
       let a = document.createElement('a');
       a.innerHTML = String(i);
       pagin.appendChild(a)
       a.style.display = 'inline-block'
       a.style.width = '2em';
       a.style.height = '2em';
       a.style.lineHeight = '2em'
       a.style.textAlign = 'center'
       a.style.color = '#000';
       a.style.borderRadius = '5px';
       a.style.boxShadow = '0 0 0 1px #ddd inset, 0 1px 1px #fff'
       a.style.cursor = 'pointer'
       items.push(a)
     }
     let active;
     ShowPage(items[0])
     
     items.forEach( item => {
       item.addEventListener('click',function(){
         ShowPage(this)
       } );
      });
      
      

      function ShowPage(item){
       if(active){
         active.style.backgroundColor = ''
       }
       active = item;
       item.style.backgroundColor = '#2bbbad'

       let pageNum = item.innerHTML;
       let start = (pageNum - 1) * noteOnPage;
       let end = start + noteOnPage;
       let notes = pat.slice(start, end);
       table.innerHTML = " ";
       
       for(let note of notes){
         let tr = document.createElement('tr');
         tr.style.backgroundColor = '#d4e3e5';
         table.appendChild(tr)


         let td = document.createElement('td');
         td.innerHTML = note.doctorId;
         td.style.display = 'none';
         tr.appendChild(td)

        
         td = document.createElement('td')
         td.addEventListener('click', function(){openModal(note.doctorId)})
         td.style.fontSize = '12px';
         td.className = 'search_name'
         td.style.borderWidth ='1px';
         td.style.padding = '8px';
         td.style.borderStyle = 'solid';
         td.style.borderColor = '#729ea5'
         td.style.maxWidth = '100px'
         td.innerHTML = note.firstName + " " + note.lastName + " "+ note.middleName;
         tr.appendChild(td)
         
         createCeil(note.age,td,function(){openModal(note.doctorId)},tr)
         createCeil(note.email,td,function(){openModal(note.doctorId)},tr)
         createCeil(note.phoneNumber,td,function(){openModal(note.doctorId)},tr)
        
     }
     function createCeil(text,td,fun,tr){
      td = document.createElement('td')
      td.addEventListener('click', fun)
      td.innerHTML = text;
      td.style.textAlign = 'center';
      td.style.fontSize = '12px';
      td.style.borderWidth ='1px';
      td.style.padding = '8px';
      td.style.borderStyle = 'solid';
      td.style.borderColor = '#729ea5'
      tr.appendChild(td)

     }
    }
    }

    public createDoctor() {
      this.dialog.open(AddDoctorComponent, { height: '1080px'});
    }



}
