import { Component, OnInit } from '@angular/core';
import { LoadScriptsService } from '../../services/load-scripts.service';
import { P } from '@angular/core/src/render3';
@Component({
  selector: 'app-doctor',
  templateUrl: './doctor.component.html',
  styleUrls: ['./doctor.component.css']
})
export class DoctorComponent implements OnInit {
  
  constructor(
    public loadScriptService : LoadScriptsService,
  ) {
    
   }
  
  ngOnInit() {
    this.loadScriptService.loadScript("../../../assets/js/rating.js");
    this.loadRating();
  }
 
  public loadRating(){
    let rating_item = document.querySelectorAll('.rating-item');
    console.log(rating_item)
    let local = +localStorage.getItem('Maksim Ivanov rating');
    console.log(local)
    for(let i = 0; i < local;i++){
      Array.from(rating_item).forEach(item=>{
        if(item.getAttribute('data-rating') <= String(local)){
          item.classList.add('active')
        }
      })
    }
  }
}
