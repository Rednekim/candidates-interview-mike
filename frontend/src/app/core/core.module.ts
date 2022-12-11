import { NgModule, Optional, SkipSelf } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { EnsureModuleLoadedOnceGuard } from '../shared/ensureModuleLoadedOnceGuard';
import { StateService } from './state.service';

@NgModule({
  imports: [ 
    HttpClientModule, 
  ], 
  providers: [StateService],
})
export class CoreModule extends EnsureModuleLoadedOnceGuard {    
  constructor( @Optional() @SkipSelf() parentModule: CoreModule) {
    super(parentModule);
  }  
}
