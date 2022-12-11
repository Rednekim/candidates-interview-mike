import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerEntryComponent } from './customers/customer-entry/customer-entry.component';
import { CustomerListComponent } from './customers/customer-list/customer-list.component';

const routes: Routes = [
  { path: 'customers', component: CustomerListComponent},
  { path: 'customers/0', component: CustomerEntryComponent},
  { path: '**', pathMatch:'full', redirectTo: '/customers' } 
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {
    static components = [ CustomerListComponent, CustomerEntryComponent ];
}