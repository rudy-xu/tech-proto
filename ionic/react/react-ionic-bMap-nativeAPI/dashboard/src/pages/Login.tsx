import React, { useState } from "react";
import { IonPage, IonContent, IonButton, IonInput, IonItem } from "@ionic/react";
import { RouteComponentProps } from "react-router";
import axios from "axios";

import "./Login.css";


const Login: React.FC<RouteComponentProps> = (props) => {

    console.log("Login");

    const [ notes, setNotes ] = useState("");
    const [ userName, setUserName ] = useState("");
    const [ pwd, setPwd ] = useState("");

    function handleClickLogin(){
        console.log("handleClickLogin");
        
        const url_authenticate = localStorage.getItem("authUrl")!;

        axios({
            method:"POST",
            url:url_authenticate,
            headers:{
                "Content-Type": "application/json"
            },
            data:{
                username:userName,
                password:pwd
            }
        })
        .then((response) => {
            if(response.data.flag)
            {
                localStorage.setItem("token",response.data.token);
                props.history.push("/Home");
            }
            else
            {
                setNotes("Invalid User name or Password.");
            }
        })
        .catch((error) => {
            alert("something wrong!");
            console.log(error);
        });
    }

    function handleClickCancel(){
        setUserName("");
        setPwd("");
        setNotes("");
    }

    function handleEnterKey(event: string): void{
                
        if(event === "Enter")
        {
            console.log("event");
            handleClickLogin();
        }
    }


    return(
        <IonPage>
            {/* <header className="header">Sign Up</header> */}
            
            <IonContent>
                <div className="loginPageCenter">
                    <p style={{color: "red"}}>{notes}</p>
                    <IonItem>
                        <IonInput value={userName} placeholder="UserName" onIonChange={e => setUserName(e.detail.value!)}></IonInput>
                    </IonItem>
                    <IonItem>
                        <IonInput type="password" value={pwd} placeholder="Password" onIonChange={e => setPwd(e.detail.value!)} onKeyPress={e => handleEnterKey(e.key)}></IonInput>
                    </IonItem>
                    <div className="centerButton">
                        <IonButton color="tertiary" onClick={() => {handleClickLogin()}} class="loginButton">Login</IonButton>
                        <IonButton color="tertiary" onClick={() => {handleClickCancel()}} class="cancelButton">Cancel</IonButton>
                    </div>
                    
                    {/* <IonLabel class="forgotPwd">Forgot password?</IonLabel> */}
                </div>
            </IonContent>
        </IonPage>
    );
};

export default Login;