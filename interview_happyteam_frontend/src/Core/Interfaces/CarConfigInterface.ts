interface Car {
    Id: number;
    Model: string;
    PricePerDay: number;
    Amount: number;
    IsAvailable: boolean;
  }
  
  export interface CarConfig {
    Cars: Car[];
  }