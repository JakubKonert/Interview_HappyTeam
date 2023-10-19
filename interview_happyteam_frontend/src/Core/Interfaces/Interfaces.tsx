export interface IError {
    carModel?: string;
    locationStart?: string;
    locationEnd?: string;
    dateStart?: string;
    dateEnd?: string;
    validDate?:string;
  }
  
  export interface IOrder {
    Car?: string;
    LocationStart?: string;
    LocationEnd?: string;
    StartDate?: Date;
    EndDate?: Date;
    Country?: String;
  }