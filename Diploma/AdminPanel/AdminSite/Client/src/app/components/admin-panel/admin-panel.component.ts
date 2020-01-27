import { Component, OnInit } from '@angular/core';

import { LoadScriptsService } from '../../services/load-scripts.service';


@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {

  Name = localStorage.getItem("Name");

  constructor( public loadScriptService : LoadScriptsService ) { }

  ngOnInit() {
  	   this.loadScriptService.loadScript("../../../assets/js/jquery-3.3.1.min.js"); 
        this.loadScriptService.loadScript("../../../assets/js/materialize.min.js");
        this.loadScriptService.loadScript("../../../assets/js/script-help.js");
        this.loadScriptService.loadScript("../../../assets/js/admin.js");
  }

}
