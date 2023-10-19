import { createContext, useContext, useEffect, useState } from "react";
import { CarConfig } from "../Interfaces/CarConfigInterface";
import { CountryConfig } from "../Interfaces/CountryConfigInterface";
import axios from "axios";
import { IOrder } from "../Interfaces/Interfaces";

interface APIProps {
  carConfig?: CarConfig;
  countryConfig?: CountryConfig;
  LogError?: (name: string, message: string) => void;
  CreateOrder?: (data: IOrder) => Promise<{ result: boolean; message: string }>;
}
const APIContext = createContext<APIProps>({});

export const useAPI = () => {
  return useContext(APIContext);
};

export const AzureAPIProvider = ({ children }: any) => {
  const [carConfig, setCarConfig] = useState<CarConfig>();
  const [countryConfig, setCountryConfig] = useState<CountryConfig>();

  const getConfigs = () => {
    axios
      .get("http://localhost:5250/api/Config/ReadAll")
      .then((response) => {
        response.data.forEach((value: any) => {
          if (value.name === "CarConfig") {
            const config: { CarConfig: CarConfig } = JSON.parse(value.data);
            setCarConfig(config.CarConfig);
          } else if (value.name === "CountryConfig") {
            const config: { CountryConfig: CountryConfig } = JSON.parse(
              value.data
            );
            setCountryConfig(config.CountryConfig);
          }
        });
      })
      .catch((error) => LogError("APIContext -> getConfigs()", error));
  };

  const CreateOrder = async (data: IOrder) => {
    const resultData = await axios
      .post("http://localhost:5250/api/Order/Create", data)
      .then((response) => {
        if (response.status == 200) {
          return { result: true, message: "Successfully made an order!" };
        }
        return {
          result: false,
          message: "Oh no! We cannot make an order right now!",
        };
      })
      .catch((error) => {
        LogError("APIContext -> CreateOrder", error);
        return {
          result: false,
          message: "Oh no! We cannot make an order right now!",
        };
      });
    const result: { result: boolean; message: string } = resultData;
    return result;
  };

  const LogError = (name: string, message: string) => {
    axios.post("http://localhost:5250/api/Error/LogError", {
      name: name,
      message: message,
    });
  };

  useEffect(() => {
    getConfigs();
  }, []);

  const value = {
    carConfig: carConfig,
    countryConfig: countryConfig,
    LogError: LogError,
    CreateOrder: CreateOrder,
  };

  return <APIContext.Provider value={value}>{children}</APIContext.Provider>;
};
