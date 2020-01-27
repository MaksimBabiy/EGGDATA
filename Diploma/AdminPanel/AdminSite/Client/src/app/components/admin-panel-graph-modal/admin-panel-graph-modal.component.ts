import { Component, OnInit, Inject } from '@angular/core';

import { element } from 'protractor';
import { Router } from '@angular/router';
import { HttpRequest, HttpClient, HttpEventType } from '@angular/common/http';
import { Http } from '@angular/http';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

import { AdministrationService } from "../../services/administration.service";

@Component({
  selector: 'app-admin-panel-graph-modal',
  templateUrl: './admin-panel-graph-modal.component.html',
  styleUrls: ['./admin-panel-graph-modal.component.css'],
})
export class AdminPanelGraphModalComponent implements OnInit {

  constructor( public router: Router, public http: Http,  
               private httpClient: HttpClient,
               private matDialogRef : MatDialogRef<AdminPanelGraphModalComponent>,
               @Inject(MAT_DIALOG_DATA) public data : any,
               public administrationService : AdministrationService ) { }

  public dis : boolean = false;
  dataa:string[] = Array();
  polyline_points:string;
  start_point:number = 0;
  end_point:number = 0;
  scroll_count:number = 1;
  counter_points:string;
  counter_time:string;
  file:File;
  patients:any;
    
  svgTransform = {
    'transform' : '1, -1',
    'transform-origin' : ''
  }

  ngOnInit() {
    
    this.updateData();
    this.dataa = this.data.graph;
    this.addPoints(this.start_point);
  }

  public close(){
    this.matDialogRef.close();
  }


  svg_click(event){
    var delta = event.deltaY || event.detail || event.wheelDelta;

    // вывести тип события, элемент и координаты клика
    var x = event.offsetX==undefined?event.layerX:event.offsetX;
    var y = event.offsetY;

    if (delta > 0) this.scroll_count+=0.5;
    else {
      if(this.scroll_count > 1)
        this.scroll_count-=0.5;
    }

    this.svgTransform.transform = 'scale('+this.scroll_count+',-'+this.scroll_count+')';
    this.svgTransform["transform-origin"] = x + 'px '+y+'px';
    console.log("svg_click");
  }

  svg_wheel(event){
    var delta = event.deltaY || event.detail || event.wheelDelta;

    if (delta > 0) {
      this.start_point+=10;
    }
    else {
      if(this.start_point > 9){
        this.start_point-=10;
      }
    }

    this.addPoints(this.start_point)

    this.updateData()
    console.log("svg_wheel");
  }

  svg_over(){  
    console.log("svg_over");
  }

  svg_leave(){   
    console.log("svg_over");
  }
  
  return_scale(){
    this.svgTransform.transform = 'scale(1, -1)';
    this.svgTransform["transform-origin"] = "center center";
    this.scroll_count = 1;
  }


  updateData(){
    this.end_point = this.start_point + 200;

    var seconds = Math.ceil(this.start_point/128);
    var minutes = Math.ceil(seconds / 60);
    var hours = Math.ceil(minutes / 60);

    if(hours >= 1){
      hours-= 1;
      minutes=Math.ceil(minutes-(hours*60));
      seconds=Math.ceil(seconds-((hours*60)*60));
    }
    if(minutes >= 1){
      minutes-= 1;
      seconds=Math.ceil(seconds-(minutes*60));
    }
    if(seconds >= 1) seconds-= 1;

    var new_hours = (hours < 10 ? '0' : '') + hours
    var new_minutes = (minutes < 10 ? '0' : '') + minutes
    var new_seconds = (seconds < 10 ? '0' : '') + seconds

    var new_date = new_hours + " : " + new_minutes  + " : " + new_seconds

    this.counter_points = "Диапазон точек: " + (this.start_point) + " - " + (this.end_point)
    this.counter_time = "Время: " + new_date
    console.log(this.start_point + " " + Math.ceil(seconds))
  }



  addPoints(start_point){
    this.polyline_points = "";

    for (var x = 0; x < 1000; x+=5) {
      this.addPoint(x, Number(this.dataa[start_point]));
      start_point+=1;
    }
  }

  addPoint(x, y){
     y+=150; //for display graph
    this.polyline_points+= x+" "+y+" ";
    console.log(this.polyline_points);
  }

  jump(value){
    //this.time+=1600;
    this.start_point+=value;
    this.addPoints(this.start_point);
    this.updateData();
  }


  convert(){
    this.administrationService.wave(this.dataa)
      .subscribe((res:string[] = Array())=> {
        this.dis = true;
        this.dataa = res;
        this.addPoints(this.start_point);
      })
  }

}
