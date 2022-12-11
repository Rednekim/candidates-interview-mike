import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent }  from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { CoreModule }   from './core/core.module';
import { SharedModule }   from './shared/shared.module';
import { CustomerEntryComponent } from './customers/customer-entry/customer-entry.component';

@NgModule({
  imports: [
    BrowserModule, 
    AppRoutingModule,
    CoreModule,   
    SharedModule 
  ],
  declarations: [ AppComponent, AppRoutingModule.components, CustomerEntryComponent ],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }