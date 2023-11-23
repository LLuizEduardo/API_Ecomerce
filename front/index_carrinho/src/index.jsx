import React from "react";
import ReactDOMClient from "react-dom/client";
import { IndexCarrinho } from "./screens/IndexCarrinho";

const app = document.getElementById("app");
const root = ReactDOMClient.createRoot(app);
root.render(<IndexCarrinho />);
