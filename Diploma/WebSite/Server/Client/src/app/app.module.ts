import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpModule } from '@angular/http';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { DoctorsComponentComponent} from './components/doctors-component/doctors-component.component';
import { DoctorComponent} from './components/doctor/doctor.component';
import { NewsComponent } from './components/news/news.component';

import { AuthService } from './services/auth.service';
import { LoadScriptsService } from './services/load-scripts.service';
import { RegisterUserService } from './services/register-user.service';

import { AuthGuard } from './auth.guard'
import { AuthInterceptor } from './services/auth.interceptor';
import { AuthResponseInterceptor } from './services/auth.response.interceptor';
import { RegisterComponent } from './components/register/register.component';
import { AgmCoreModule } from '@agm/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'






const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'doctors', component: DoctorsComponentComponent},
  { path: 'doctor', component: DoctorComponent},
  { path: 'news', component: NewsComponent}
];


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    HeaderComponent,
    FooterComponent,   
    RegisterComponent,
    DoctorsComponentComponent,
    DoctorComponent,
    NewsComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    HttpModule,
    BrowserAnimationsModule,
    ReactiveFormsModule, 
    RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' }),
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyDzEbjKqwFACpoiVUAIrQvy6IGLTlRgtKo'
    }),
    
   
  ],
  providers: [
  	AuthGuard, AuthService, RegisterUserService, LoadScriptsService,
  	{
  		provide: HTTP_INTERCEPTORS,
  		useClass: AuthInterceptor,
  		multi : true
  	},
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthResponseInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }
