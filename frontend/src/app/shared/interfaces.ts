export interface ICustomer extends ICustomerBase {
  state?: IState;
  orderCount?: number;
  orders?: IOrder[];
  orderTotal?: number;
}

export interface ICustomerBase {
  id?: string;
  firstName: string;
  lastName: string;
  email: string;
  address: string;
  city: string;
  stateId?: number;
  zip: number;
  gender: string;
}

export interface IState {
  id: number;
  abbreviation: string;
  name: string;
}

export interface IOrder {
  product: string;
  price: number;
  quantity: number;
  orderTotal?: number;
}

export interface IListResultClass<T> {
  data: T[];
  total: number;
  pageNumber: number;
  pageSize: number;
}
