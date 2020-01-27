import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpModule } from '@angular/http';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material';

import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { AdminPanelPatientsComponent } from './components/admin-panel-patients/admin-panel-patients.component';
import { AdminPanelPatientModalComponent } from './components/admin-panel-patient-modal/admin-panel-patient-modal.component';
import { AdminPanelDoctorsComponent } from './components/admin-panel-doctors/admin-panel-doctors.component';
import { AdminPanelDoctorModalComponent } from './components/admin-panel-doctor-modal/admin-panel-doctor-modal.component';
import { AdminPanelGraphModalComponent } from './components/admin-panel-graph-modal/admin-panel-graph-modal.component';
import { AddPatientComponent } from './components/add-patient/add-patient.component';
import { AddDoctorComponent } from './components/add-doctor/add-doctor.component'; 

import { AuthService } from './services/auth.service';
import { LoadScriptsService } from './services/load-scripts.service';
import { AdministrationService } from './services/administration.service';

import { AuthGuard } from './auth.guard'
import { AuthInterceptor } from './services/auth.interceptor';
import { AuthResponseInterceptor } from './services/auth.response.interceptor';






const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },

  { path: 'admin_panel', component : AdminPanelComponent, canActivate: [AuthGuard], children: [

      { path: '', redirectTo: 'patients', pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'patients', component : AdminPanelPatientsComponent, canActivate: [AuthGuard] },
      { path: 'doctors', component: AdminPanelDoctorsComponent, canActivate: [AuthGuard] }

  ]
  }
];


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    HeaderComponent,
    FooterComponent,
    AdminPanelPatientsComponent,
    AdminPanelPatientModalComponent,
    AdminPanelComponent,
    AdminPanelDoctorsComponent,
    AdminPanelDoctorModalComponent,
    AdminPanelGraphModalComponent,
    AddPatientComponent,
    AddDoctorComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    HttpModule,
    BrowserAnimationsModule,
    ReactiveFormsModule, 
    MatDialogModule,
    RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' })
  ],
  providers: [
  	AuthGuard, AuthService, LoadScriptsService, AdministrationService,
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
  bootstrap: [AppComponent],
  entryComponents: [ AdminPanelPatientModalComponent, AdminPanelDoctorModalComponent, AdminPanelGraphModalComponent, AddPatientComponent, AddDoctorComponent ]
})

export class AppModule { }
