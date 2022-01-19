import {
  IonContent,
  IonItem,
  IonList,
  IonListHeader,
  IonMenu,
  IonMenuToggle,
  IonImg,
  IonText,
} from '@ionic/react';

import React from 'react';
import { useLocation } from 'react-router-dom';
// import { desktop, desktopOutline, videocam, videocamOutline, home, homeOutline} from 'ionicons/icons';

import './Menu.css';

interface AppPage {
  url: string;
  imgSrc: string;
  // iosIcon: string;
  // mdIcon: string;
  title: string;
}

const appPages: AppPage[] = [
  {
    title: 'Smart Logistics',
    url: '/Home/SmartLogistic',
    imgSrc: '/assets/icon/favicon.png'
  },
  {
    title: ' Camera Video',
    url: '/Home/ThermalVideo',
    imgSrc: '/assets/imgs/video.png',
  },
  {
    title: 'IoT Dashboard',
    url: '/Home/IoTDashboard',
    imgSrc: '/assets/imgs/dashboard.png',
  }
];

// const labels = ['Family', 'Friends', 'Notes', 'Work', 'Travel', 'Reminders'];

const Menu: React.FC = () => {
  const location = useLocation();

  return (
    <IonMenu contentId="main" type="overlay">
      <IonContent class="ionContent">
        <IonList id="inbox-list" class="ionList">
          <IonListHeader>
            <IonImg src="./assets/imgs/logo.png" class="logImg"/>
          </IonListHeader>
          {/* <IonNote>xxxxx.xxx@sioux.asia</IonNote> */}
          {appPages.map((appPage, index) => {
            return (
              <IonMenuToggle key={index} autoHide={false}>
                <IonItem class="ionItem" className={location.pathname === appPage.url ? 'selected' : ''} routerLink={appPage.url} routerDirection="none" lines="none" detail={false}>
                  {/* <IonChip class="chipMenu"/> */}
                  {/* <IonIcon slot="start" ios={appPage.imgSrc} md={appPage.mdIcon} class="iconMenu"/> */}
                  <IonImg slot="start" src={appPage.imgSrc} class="iconMenu"/>
                  <IonText color="light" class="menuText">{appPage.title}</IonText>
                </IonItem>
              </IonMenuToggle>
            );
          })}
        </IonList>

        {/* <IonList id="labels-list">
          <IonListHeader>Labels</IonListHeader>
          {labels.map((label, index) => (
            <IonItem lines="none" key={index}>
              <IonIcon slot="start" icon={bookmarkOutline} />
              <IonLabel>{label}</IonLabel>
            </IonItem>
          ))}
        </IonList> */}
      </IonContent>
    </IonMenu>
  );
};

export default Menu;
