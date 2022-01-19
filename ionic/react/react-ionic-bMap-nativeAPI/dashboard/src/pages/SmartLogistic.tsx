import React from 'react';
import { IonContent, IonHeader, IonPage, IonTitle, IonToolbar } from '@ionic/react';
import Map from "../components/Map";

import './SmartLogistic.css';

const SmartLogistic: React.FC = () => {
 
  return (
    <IonPage>
      <IonHeader>
        <IonToolbar>
          <IonTitle class="smartTitle">
            Smart Logistic Platform
            {/* <IonChip class="ionChip">Demo</IonChip> */}
          </IonTitle>
        </IonToolbar>
      </IonHeader>

      <IonContent class="content">
          <Map />
      </IonContent>
    </IonPage>
  );
};

export default SmartLogistic;
