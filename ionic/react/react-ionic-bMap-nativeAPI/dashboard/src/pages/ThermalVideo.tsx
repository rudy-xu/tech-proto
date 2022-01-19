import React, { useState } from 'react';
import { IonContent, IonHeader, IonPage, IonTitle, IonToolbar, IonRow, IonLabel, IonGrid, IonCol, IonItem, IonCard, IonList, IonButton } from '@ionic/react';
// import { useParams } from 'react-router';
// import ExploreContainer from '../components/ExploreContainer';
import './ThermalVideo.css';
import { useVideoDataApi } from '../hooks/useDataApi';

const ThermalVideo: React.FC = () => {
  const { videoData } = useVideoDataApi();
  const [ videoSrc, setVideoSrc ] = useState("");
  const [ faceVideoSrc, setFaceVideoSrc ] = useState("");

  function handleClick (value: string, faceValue: string) {
    setVideoSrc(value);
    setFaceVideoSrc(faceValue);
    console.log(videoSrc);
  };

  return (
    <IonPage>
      <IonHeader class="ionHerader">
        <IonToolbar>
            <IonTitle class="thermalTitle">Sioux Cloud Project</IonTitle>
        </IonToolbar>
      </IonHeader>

      <IonContent>
          <IonLabel class="lableVideoAna">
            Camera Video Analysis
            {/* <IonChip class="ionChip">Demo</IonChip> */}
          </IonLabel><br /><br />

          <IonLabel class="lableVideoCam"> Video Sources</IonLabel>
          <IonLabel class="thermalTime">Upload time: {new Date().toISOString()}</IonLabel>
          <IonItem class="splitLine"/>
          <br/><br/><br/>

        <IonGrid>
          <IonRow>
            <IonCol class="video-col">
              <IonCard class="video-list">
                <IonList style={{height:"380px", overflowY:"scroll"}}>
                  {videoData.map((value,idx) => (
                    // <IonItem key={idx} class="video-item">
                      <IonButton onClick={() => { handleClick(value.url, value.url2) }} key={idx} class="video-button"> {value.name} </IonButton>
                    /* </IonItem> */
                  ))}
                </IonList>
              </IonCard>
            </IonCol>

            <IonCol class="srcVideo-col">
              <IonCard class="norVideo-card">
                <video id="norVideo" className="src-Video" poster="/assets/imgs/tmp.JPG" src={videoSrc} controls autoPlay>
                    {/* <source id="anaVideoSrc" src={videoSrc} type="video/mp4"/> */}
                </video>
              </IonCard>
              <label className="src-video-label">Original Video</label>
            </IonCol>

            <IonCol class="anaVideo-col">
              <IonCard class="anaVideo-card">
                <video id="anaVideo" className="face-video" poster="/assets/imgs/tmp.JPG" src={faceVideoSrc} controls autoPlay />
              </IonCard>
              <label className="face-video-label">Analysis Video</label>
            </IonCol>
          </IonRow>
        </IonGrid>

      </IonContent>
    </IonPage>
  );
};

export default ThermalVideo;
