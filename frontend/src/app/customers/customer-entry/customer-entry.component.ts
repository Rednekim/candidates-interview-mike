import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { CustomerService } from "src/app/core/customer.service";
import { StateService } from "src/app/core/state.service";
import { ICustomerBase, IState } from "src/app/shared/interfaces";

@Component({
  selector: "customer-entry",
  templateUrl: "./customer-entry.component.html",
  styleUrls: ["./customer-entry.component.scss"],
})
export class CustomerEntryComponent implements OnInit {
  form!: FormGroup;
  states$!: Observable<IState[]>;
  constructor(
    private readonly _formBuilder: FormBuilder,
    private readonly _customerService: CustomerService,
    private readonly _stateService: StateService,
    private readonly _router: Router
  ) {
    this.states$ = this._stateService.getStates();
  }

  ngOnInit(): void {
    this.form = this._formBuilder.group({
      id: this._formBuilder.control(0, [Validators.required]),
      firstName: this._formBuilder.control(null, [Validators.required]),
      lastName: this._formBuilder.control(null, [Validators.required]),
      gender: this._formBuilder.control("Male", [Validators.required]),
      email: this._formBuilder.control(null, [
        Validators.email,
        Validators.required,
      ]),
      address: this._formBuilder.control(null, [Validators.required]),
      city: this._formBuilder.control(null, [Validators.required]),
      stateId: this._formBuilder.control(1, [Validators.required]),
    });
  }
  onFormSubmit() {
    if (this.form.valid) {
      const newCustomer = this.form.getRawValue() as ICustomerBase;
      this._customerService.createCustomer(newCustomer).subscribe((data) => {
        this._router.navigate(["customers"]);
      });
    }
  }
}
