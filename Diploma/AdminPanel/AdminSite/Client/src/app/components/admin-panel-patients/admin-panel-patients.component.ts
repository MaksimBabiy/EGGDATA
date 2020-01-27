import { Component, OnInit, ViewChild, ElementRef, ɵConsole } from '@angular/core';
import { MatDialog } from '@angular/material';


import { LoadScriptsService } from '../../services/load-scripts.service';

import { Patient } from '../../models/patient.model';

import { AdministrationService } from '../../services/administration.service';

import { AdminPanelPatientModalComponent } from '../admin-panel-patient-modal/admin-panel-patient-modal.component';
import { AdminPanelGraphModalComponent } from '../admin-panel-graph-modal/admin-panel-graph-modal.component';

import { AddPatientComponent } from '../add-patient/add-patient.component';




@Component({
  selector: 'app-admin-panel-patients',
  templateUrl: './admin-panel-patients.component.html',
  styleUrls: ['./admin-panel-patients.component.css']
})
export class AdminPanelPatientsComponent implements OnInit {

  
  
  patients : Array<Patient>;
  timeout : any;
 

  constructor( public loadScriptService : LoadScriptsService, 
               private adminService : AdministrationService, 
               public dialog : MatDialog,
               
               )       
  { 
    this.loadPatients();
    this.loadPagination();
  
  }
  
  
  ngOnInit(){
        this.loadScriptService.loadScript("../../../assets/js/jquery-3.3.1.min.js"); 
        this.loadScriptService.loadScript("../../../assets/js/materialize.min.js");
        this.loadScriptService.loadScript("../../../assets/js/script-help.js");
        this.loadScriptService.loadScript("../../../assets/js/search.js");
       
    }
   
    
    public loadPatients(){
      this.adminService.getPatients()
            .subscribe((data: Patient[]) => {             
                this.patients = data;
            });
    }
   public loadPagination(){
     this.timeout = setTimeout(()=>{
       this.Pagination();
     }, 400)
   }
    public Pagination(){
      let dia = this.dialog
      let pat = this.patients
      let admin = this.adminService
 
      function openModal(patientID){  
        let patient_model : any;
        for (let i of pat)
          if(i.patientId == patientID)
              {
                patient_model = i;
                break;
              }
              dia.open(AdminPanelPatientModalComponent, { height: '1080px',
          data : { patient : patient_model }
      });
      }
      function openGraph(patientID, pp){
        var id = patientID;
    		admin.openGraphPatient(id)
    		.subscribe((res:string[] = Array()) => {
    			console.log('semen');

          if(res.toString() == "Exception,Could not find a part of the path")
            pp.innerHTML = "Файл не загружен";
          else
    			dia.open(AdminPanelGraphModalComponent, { height: '900px', width: '1920px',  
            data : { graph : res }
          })
    		})
      }
      
      let noteOnPage = 5;
      let table = document.querySelector('.parent');
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
          td.innerHTML = note.patientId;
          td.style.display = 'none';
          tr.appendChild(td)


          td = document.createElement('td')
          td.addEventListener('click', function(){openModal(note.patientId)})
          td.style.fontSize = '12px';
          td.className = 'search_name'
          td.style.borderWidth ='1px';
          td.style.borderStyle = 'solid';
          td.style.borderColor = '#729ea5';
          td.style.maxWidth = '200px'
          td.innerHTML = note.firstName + " " + note.lastName + " "+ note.middleName;
          tr.appendChild(td)
          
          createCeil(note.age,td,function(){openModal(note.patientId)},tr,)
          createCeil(note.email,td,function(){openModal(note.patientId)},tr,)
          createCeil(note.phoneNumber,td,function(){openModal(note.patientId)},tr,)


          td = document.createElement('td')
          td.style.fontSize = '12px';
          td.style.maxWidth = '30px';
          td.style.borderWidth ='1px';
          td.style.borderStyle = 'solid';
          td.style.borderColor = '#729ea5'
          tr.appendChild(td);

          let div = document.createElement('div');
          div.style.display = 'flex';
          div.style.alignItems = 'center';
          div.style.justifyContent = 'space-between';
          td.appendChild(div);

          let a = document.createElement('a');
          a.addEventListener('click', function(){openGraph(note.patientId,lab)})
          a.innerHTML = 'Отобразить'
          a.style.cursor = 'pointer';
          a.style.backgroundColor = '#2bbbad'
          a.style.borderRadius = '2px'
          a.style.padding = '7px 7px';
          a.style.textTransform = 'uppercase';
          a.style.fontSize = '14px';
          a.style.color = 'white';
          div.appendChild(a)

          let lab = document.createElement('label');
          div.appendChild(lab);
      }
      function createCeil(text,td,fun,tr,){
        td = document.createElement('td')
        td.addEventListener('click', fun)
        td.style.fontSize = '12px';
        td.className = 'search_name'
        td.style.borderWidth ='1px';
        td.style.borderStyle = 'solid';
        td.style.borderColor = '#729ea5'
        td.innerHTML = text;
        tr.appendChild(td)
      }
     }
     }
    
    //for add patient   
    public createPatient(){
    
       this.dialog.open(AddPatientComponent, { height: '1080px'})
       
    }



}

