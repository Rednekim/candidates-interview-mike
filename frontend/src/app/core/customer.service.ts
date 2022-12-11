import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";

import { Observable } from "rxjs";

import { environment } from "../../environments/environment";
import {
  ICustomer,
  ICustomerBase,
  IListResultClass,
} from "../shared/interfaces";

@Injectable({
  providedIn: "root",
})
export class CustomerService {
  baseUrl = environment.apiUrl;
  baseCustomersUrl = this.baseUrl + "customers";

  constructor(private http: HttpClient) {}

  getCustomers(
    pageSize?: number,
    pageNumber?: number
  ): Observable<IListResultClass<ICustomer>> {
    let httpParams = new HttpParams();
    if (pageSize) {
      httpParams = httpParams.append("pageSize", pageSize.toString());
    }
    if (pageNumber) {
      httpParams = httpParams.append("pageNumber", pageNumber.toString());
    }
    return this.http.get<IListResultClass<ICustomer>>(
      `${this.baseCustomersUrl}`,
      { params: httpParams }
    );
  }
  createCustomer(customer: ICustomerBase): Observable<number> {
    return this.http.post<number>(this.baseCustomersUrl, customer);
  }
}
