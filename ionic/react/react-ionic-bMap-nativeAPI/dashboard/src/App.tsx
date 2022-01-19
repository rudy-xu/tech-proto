import React, { useEffect } from 'react';
import { IonApp, IonRouterOutlet } from '@ionic/react';
import { IonReactRouter } from '@ionic/react-router';
import { Redirect, Route } from 'react-router-dom';
import axios from 'axios';

/* Core CSS required for Ionic components to work properly */
import '@ionic/react/css/core.css';

/* Basic CSS for apps built with Ionic */
import '@ionic/react/css/normalize.css';
import '@ionic/react/css/structure.css';
import '@ionic/react/css/typography.css';

/* Optional CSS utils that can be commented out */
import '@ionic/react/css/padding.css';
import '@ionic/react/css/float-elements.css';
import '@ionic/react/css/text-alignment.css';
import '@ionic/react/css/text-transformation.css';
import '@ionic/react/css/flex-utils.css';
import '@ionic/react/css/display.css';

/* Theme variables */
import './theme/variables.css';

import Login from "./pages/Login";
import Home from "./pages/Home";
// import ErrorPage from "./components/ErrorPage";
import RouteFallback from "./components/RouteFallback";

const App: React.FC = () => {
  console.log("App");
  useEffect(()=>{
    const getLocalData = async () => {
      await axios({
        method:"GET",
        url:"assets/json/config.json"
      })
      .then((respone) => {
        let url = respone.data.basicUrl;
        let path = respone.data.path;
        localStorage.setItem("register", url+path.register);
        localStorage.setItem("authUrl", url+path.authenticate);
        localStorage.setItem("messageUrl", url+path.message);
        localStorage.setItem("statusUrl", url+path.status);
        localStorage.setItem("videoUrl", respone.data.videoUrl);
        // localStorage.setItem("token", respone.data.token);
      });
    };
    getLocalData();
  });

  return (
    <IonApp>
      <IonReactRouter>
        <IonRouterOutlet>
            <Route path="/Login" component={Login} exact={true} />
            <Route path="/Home" component={Home} />
            <Route path="/" render={() => <Redirect to="/Login" />} exact />
            <Route component={RouteFallback} />
        </IonRouterOutlet>
      </IonReactRouter>
    </IonApp>
  );
};

export default App;
