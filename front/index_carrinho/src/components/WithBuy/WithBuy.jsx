/*
We're constantly improving the code you see. 
Please share your feedback here: https://form.asana.com/?k=uvp-HPgd3_hyoXRBw1IcNg&d=1152665201300829
*/

import PropTypes from "prop-types";
import React from "react";
import { Cart117 } from "../../icons/Cart117";
import "./style.css";

export const WithBuy = ({ cart, className }) => {
  return (
    <div className={`with-buy ${cart} ${className}`}>
      {cart === "on" && (
        <div className="overlap">
          <Cart117 className="cart" color="black" />
          <div className="group">
            <div className="overlap-group">
              <div className="ellipse" />
              <div className="text-wrapper">2</div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

WithBuy.propTypes = {
  cart: PropTypes.oneOf(["off", "on"]),
};
