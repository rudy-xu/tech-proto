import React, { useEffect, useState } from "react";
import { IonPage, IonContent, IonRouterOutlet, IonSplitPane, IonButton } from "@ionic/react";
import { IonReactRouter } from "@ionic/react-router";
import { Redirect, Route } from 'react-router-dom';

import Menu from "../components/Menu";
import IoT from "./IoTDashboard";
import Smart from "./SmartLogistic";
import Thermal from "./ThermalVideo";
import ErrorPage from "../components/ErrorPage";

import Dialog from "../components/Dialog";

import "./Home.css";

interface dialogParas{
    visible: boolean;
    closeModal: Function;
}

const Home: React.FC = () => {
    console.log("Home");
    // const [dialogVisible, setDialogVisible] = useState<boolean>(false);
    const acc_token = localStorage.getItem("token");
    console.log("homeToken: "+acc_token);

    if(acc_token == null || acc_token == "")
    {
        // rcp.history.push("/Login");
        // rcp.history.go(0);
        window.location.replace("/Login");
    }

    return (
        <IonPage>
            <IonContent>
                <IonReactRouter>
                    <IonSplitPane contentId="main" class="isp">
                        <Menu />
                        <IonRouterOutlet id="main">
                            <Route path="/Home/IoTDashboard" component={IoT} exact/>
                            <Route path="/Home/SmartLogistic" component={Smart} exact />
                            <Route path="/Home/ThermalVideo" component={Thermal} exact />
                            <Route path="/Home" render={() => <Redirect to="/Home/IoTDashboard" />} exact />
                            <Route component={ErrorPage} />
                        </IonRouterOutlet>
                    </IonSplitPane>
                </IonReactRouter>
            </IonContent>
        </IonPage>
    );

    // function handleClickDialog(): void {
    //     console.log("handleClickDialog");
    //     setDialogVisible(true);
    //     console.log(dialogVisible)
    // }

    // function closeModal(): void{
    //     console.log("I am is callback.");
    //     setDialogVisible(false);
    // }

    // return (
    //     <IonPage>
    //         <IonContent class="app">
    //             <IonButton color="tertiary" onClick={handleClickDialog}>Click Here</IonButton>
    //             {/* {dialogVisible ? <Dialog /> : null} */}
    //             <Dialog visible={dialogVisible} closeModal={closeModal}></Dialog>
    //         </IonContent>
    //     </IonPage>
    // );
};

export default Home;