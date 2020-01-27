import { Injectable } from '@angular/core';

@Injectable()
export class LoadScriptsService {

  constructor() { }

  public loadScript(url: string) {
      var body = <HTMLDivElement> document.body;
      var script = document.createElement('script');
      script.innerHTML = '';
      script.src = url;
      script.async = false;
      script.defer = true;
      body.appendChild(script);
}


}