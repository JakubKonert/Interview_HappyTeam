export interface Location {
    Id: number;
    Name: string;
    IsAvailable: boolean;
  }
  
  interface Country {
    Id: number;
    Name: string;
    MainLocation: string;
    Locations: Location[];
    IsAvailable: boolean;
  }
  
  export interface CountryConfig {
    Countries: Country[];
  }