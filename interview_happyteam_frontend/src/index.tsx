import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Pricing from "./Components/Pricing";
import Base from "./Components/Base";
import Home from "./Components/Home";
import Order from "./Components/Order";
import { AzureAPIProvider } from "./Core/Context/APIContext";

const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
  },
  {
    path: "home",
    element: (
      <Base>
        <Home />
      </Base>
    ),
  },
  {
    path: "pricing",
    element: (
      <Base>
        <Pricing />
      </Base>
    ),
  },
  {
    path: "order",
    element: (
      <Base>
        <Order />
      </Base>
    ),
  },
]);

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <AzureAPIProvider>
      <RouterProvider router={router} />
    </AzureAPIProvider>
  </React.StrictMode>
);
