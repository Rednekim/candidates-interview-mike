import { Component, OnInit } from "@angular/core";
import { CustomerService } from "src/app/core/customer.service";
import { ICustomer } from "src/app/shared/interfaces";
@Component({
  selector: "customers",
  templateUrl: "./customer-list.component.html",
  styleUrls: ["./customer-list.component.scss"],
})
export class CustomerListComponent implements OnInit {
  customers!: ICustomer[];
  totalRecords: number = 0;
  totalPages: number = 1;

  loading: boolean = false;
  error: boolean = false;

  pageSize: number = 10;
  pageNumber: number = 1;

  constructor(private _customerService: CustomerService) {}

  ngOnInit(): void {
    this.loadCustomers(this.pageSize, this.pageNumber);
  }

  private loadCustomers(pageSize?: number, pageNumber?: number) {
    this.loading = true;
    this._customerService.getCustomers(pageSize, pageNumber).subscribe({
      next: (data) => {
        this.customers = data.data;
        this.totalRecords = data.total;
        this.pageNumber = data.pageNumber ?? 1;
        this.totalPages = !(pageSize && pageNumber) ? 1 : Math.ceil(data.total / data.pageSize);
      },
      error: (err) => {
        this.error = true;
        console.error(err);
      },
      complete: () => (this.loading = false),
    });
  }

  changePage(newPage: number) {
    this.loadCustomers(this.pageSize, newPage);
  }
  counter = (n: number) => new Array(n);
}
