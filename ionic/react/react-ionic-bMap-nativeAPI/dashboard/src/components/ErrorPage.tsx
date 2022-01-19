import React from "react";
import { useLocation } from "react-router-dom";

// const myRouter = ["/Login", "/Home", "/Home/IoTDashboard", "/Home/SmartLogistic", "/Home/ThermalVideo"];

const ErrorPage: React.FC = () => {

    const location = useLocation();
    console.log("errorPage: "+ location.pathname)

    // if(!(myRouter.includes(location.pathname)))
    // {
        // rcp.history.push("/Login");
        // rcp.history.go(0);
        window.location.replace("/Login");
    // }

    return null;
} 

export default ErrorPage;