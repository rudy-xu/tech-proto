import React from "react";
import { Redirect,useLocation } from "react-router-dom";

const RouteFallback: React.FC = () => {
    const location = useLocation();

    return (
        <Redirect 
            from = {location.pathname}
            to = "/Login"
        />
    );
}

export default RouteFallback;