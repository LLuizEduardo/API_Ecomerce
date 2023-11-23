/*
We're constantly improving the code you see. 
Please share your feedback here: https://form.asana.com/?k=uvp-HPgd3_hyoXRBw1IcNg&d=1152665201300829
*/

import PropTypes from "prop-types";
import React from "react";

export const UserOff = ({ color = "black", className }) => {
  return (
    <svg
      className={`user-off ${className}`}
      fill="none"
      height="80"
      viewBox="0 0 80 80"
      width="80"
      xmlns="http://www.w3.org/2000/svg"
    >
      <path
        className="path"
        d="M60 67.5V60.8333C60 57.2971 58.806 53.9057 56.6805 51.4052C54.5551 48.9048 51.6725 47.5 48.6667 47.5H28.8333C25.8275 47.5 22.9449 48.9048 20.8195 51.4052C18.694 53.9057 17.5 57.2971 17.5 60.8333V67.5"
        stroke={color}
        strokeLinecap="round"
        strokeLinejoin="round"
        strokeWidth="1.5"
      />
      <path
        className="path"
        d="M41.25 35C47.4632 35 52.5 29.9632 52.5 23.75C52.5 17.5368 47.4632 12.5 41.25 12.5C35.0368 12.5 30 17.5368 30 23.75C30 29.9632 35.0368 35 41.25 35Z"
        stroke={color}
        strokeLinecap="round"
        strokeLinejoin="round"
        strokeWidth="1.5"
      />
    </svg>
  );
};

UserOff.propTypes = {
  color: PropTypes.string,
};
