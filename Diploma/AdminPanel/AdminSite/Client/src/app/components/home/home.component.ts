import { Component, OnInit } from '@angular/core';

import { LoadScriptsService } from '../../services/load-scripts.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})


export class HomeComponent  {

  constructor( public loadScriptService : LoadScriptsService ){}

  ngOnInit(){  
        this.loadScriptService.loadScript("../../../assets/js/jquery-3.3.1.min.js");  
        this.loadScriptService.loadScript("../../../assets/js/materialize.min.js");
        this.loadScriptService.loadScript("../../../assets/js/script.js");
    }

}
