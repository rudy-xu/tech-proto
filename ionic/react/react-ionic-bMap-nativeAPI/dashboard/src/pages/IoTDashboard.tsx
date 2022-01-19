import React from 'react';
import {
  IonItem,
  IonContent,
  IonHeader,
  IonPage,
  IonTitle,
  IonToolbar,
  IonRow,
  IonLabel,
  IonCardHeader,
  IonCardContent,
  IonGrid,
  IonCard
} from '@ionic/react';
// import { radioButtonOn } from 'ionicons/icons'
// import ExploreContainer from '../components/ExploreContainer';
import { useDataApi } from "../hooks/useDataApi";
import { sensor} from "../models/IotDashboardData";
import { RouteComponentProps } from 'react-router-dom';

import './IoTDashboard.css';


const sensorRemark = [
  {
    unit: "",
    warning_thres: 700, 
    error_thres: 900,
    description: "Cars Per Hour"
  },
  {
    unit: "",
    warning_thres: 500, 
    error_thres: 800,
    description: "Pedestrian per Hour"
  },
  {
    unit: "dB",
    warning_thres: 50, 
    error_thres: 60,
    description: "Noise Level"
  },
  {
    unit: "kWh",
    warning_thres: 40, 
    error_thres: 60,
    description: "Electricity"
  },
  {
    unit: "℃",
    warning_thres: 30, 
    error_thres: 50,
    description: "Pavement Temp"
  },
  {
    unit: "μg/m³",
    warning_thres: 100, 
    error_thres: 200,
    description: "PM 2.5"
  },
  {
    unit: "μg/m³",
    warning_thres: 40, 
    error_thres: 100,
    description: "PM 10"
  },
  {
    unit: "ppm",
    warning_thres: 450, 
    error_thres: 800,
    description: "CO2"
  }
];


// //About refreshing by pulling down. End the refreshing by setTimeout. 
// function doRefresh(event: CustomEvent){
//   console.log("Begin async operation");

//   setTimeout(() => {
//     console.log("Async operation has ended");
//     event.detail.complete();
//   },2000);
// }

// //About refreshing with mouse down. End the refreshing by setTimeout
// function doInfinite($event: CustomEvent<void>){
//   console.log("Begin async operation");

//   setTimeout(() => {
//     console.log("Async operation has ended");
//     ($event.target as HTMLIonInfiniteScrollElement).complete();
//   },2000);
// }


const IoTDashboard: React.FC<RouteComponentProps> = (rcp) => {

  const { msgData, tokenFlag } = useDataApi();

  if(tokenFlag)
  {
    rcp.history.push("/Login");
    rcp.history.go(0);
  }

  // function handleClick(){
  //   // props.history.push("/Login");
  //   // props.history.go(0);
  //   window.location.reload();
  // }


  return (
    <IonPage>
      <IonHeader class="iot-header">
        <IonToolbar>
          <IonTitle class="iot-title">IoT Dashboard</IonTitle>
        </IonToolbar>
        {/* <IonButton color="warning" onClick={() => {handleClick()}}>sdadads</IonButton> */}
      </IonHeader>

      <IonContent>
        
        {/* <IonRefresher slot="fixed" onIonRefresh={doRefresh}>
        <IonRefresherContent
          // pullingIcon={chevronDownCircleOutline}
          pullingText="Pull to refresh"
          refreshingSpinner="circles"
          refreshingText="Refreshing...">
        </IonRefresherContent>
      </IonRefresher> */}

        <IonLabel class="lable-title">Smart Street</IonLabel><br /><br />
        <IonLabel class="label-map">Map</IonLabel>
        <IonLabel class="label-mapSL">&gt;SL-NanTian-21</IonLabel><br /><br />
        <IonLabel class="label-SL">SL-NanTian-21</IonLabel>
        <IonLabel class="iot-time">{new Date().toISOString()}</IonLabel>
        <IonItem class="iot-splitLine-top" />

        <IonGrid class="dashboard-grid">
          <SensorData msg={msgData.data} />
          {/* {msgData.map((value, index) => (
              // console.log(value.data),
              // console.log(typeof(value.data)),
              <SensorData key={index} msg={JSON.parse(value.data)} />
            ))} */}
        </IonGrid>
        <IonItem class="iot-splitLine-bottom" />

        {/* <IonInfiniteScroll threshold="100px" onIonInfinite={doInfinite}>
        <IonInfiniteScrollContent
          loadingSpinner="bubbles"
          loadingText="Loading more data…"
        ></IonInfiniteScrollContent>
      </IonInfiniteScroll> */}

      </IonContent>
    </IonPage>
  );
};


// interface Paras {
//   value: number
// }

// function InfoData(props: Paras): JSX.Element{
//   return (
//     <IonCardHeader class="sensor-normal">{props.value}</IonCardHeader>
//     );
// }

const InfoData: React.FC<{ sensorValue: number, unit: any }> = ({ sensorValue, unit }) => {
  return (
    <IonCardHeader class="sensor-normal">
      {sensorValue}
      <IonLabel class="unit-normal">{unit}</IonLabel>
    </IonCardHeader>
  );
}

const WarningData: React.FC<{ sensorValue: number, unit: string }> = ({ sensorValue, unit }) => {
  return (
    <IonCardHeader class="sensor-warning">
      {sensorValue}
      <IonLabel class="unit-warning">{unit}</IonLabel>
    </IonCardHeader>
  );
}

const ErrorData: React.FC<{ sensorValue: number, unit: string }> = ({ sensorValue, unit }) => {
  return (
    <IonCardHeader class="sensor-error">
      {sensorValue}
      <IonLabel class="unit-error">{unit}</IonLabel>
    </IonCardHeader>
  );
}


const SensorData: React.FC<{msg: sensor[]}> = ({ msg }) => {
  // console.log(msg);
  return (
      <IonRow>
        {msg.map((v, idx) => (
          <IonCard key={idx} class="sensor-card">
          {v.value < sensorRemark[idx].warning_thres
          ? <InfoData key={idx} sensorValue={v.value} unit={sensorRemark[idx].unit} />
          : v.value < sensorRemark[idx].error_thres
          ? <WarningData sensorValue={v.value} unit={sensorRemark[idx].unit} />
          : <ErrorData sensorValue={v.value} unit={sensorRemark[idx].unit} />}
           <IonCardContent class="sensor-des">{sensorRemark[idx].description}</IonCardContent>
        </IonCard>
        ))}
      </IonRow>
  );
};

// interface Props {
//   //
// }

// var interval:any;
// class IoTDashboard extends React.Component<Props, sDataState>{
//   constructor(props: Props) {
//     super(props);
//     this.state = {
//       senMsg: [],
//       senDev: []
//     }
//   }

//   //execute this setInterval after rendering
//   componentDidMount() {
//     interval = setInterval(
//       () => this.myFresh(),
//       5000
//     );
//   }

//   //clean setInterval
//   componentWillMount(){
//       clearInterval(interval);
//   }

//   myFresh() {
//     const f = async () => {
//       const statusRes = await fetch('http://10.86.12.127:5000/api/DeviceStatus', {
//         method: 'GET',
//         // headers: {
//         //   "Content-Type":"application/json; charset=utf-8"
//         // },
//         // mode: 'no-cors'
//         // cache: 'default'
//       });
//       const msgRes = await fetch('http://10.86.12.127:5000/api/Devicemessage', {
//         method: 'Get',
//       });

//       // console.log(result);
//       // console.log(result.json());
//       const myStatus = await statusRes.json();
//       const myMsg = await msgRes.json();
//       console.log("myMsg: "+ myMsg);

//       this.setState({
//         senDev: myStatus,
//         senMsg: myMsg
//       });
//     };
//     f();
//   }

//   render() {
//     return (
//       <IonPage>
//         <IonHeader>
//           <IonToolbar>
//             <IonButtons slot="start">
//               <IonMenuButton />
//             </IonButtons>
//             <IonTitle class="iotTitle">IoT Dashboard</IonTitle>
//           </IonToolbar>
//         </IonHeader>

//         <IonContent>
//           <IonRow>
//             {/* <meta http-equiv="refresh" content="1"></meta> */}
//             <IonCol>
//               <IonItem>
//                 <IonTitle>Data information</IonTitle>
//               </IonItem>
//               <IonItem>
//                 <IonCol class="tbHead">DEVICEID</IonCol>
//                 <IonCol class="tbHead">TIME</IonCol>
//                 <IonCol class="tbHead">S_1</IonCol>
//                 <IonCol class="tbHead">S_2</IonCol>
//                 <IonCol class="tbHead">S_3</IonCol>
//                 <IonCol class="tbHead">S_4</IonCol>
//                 <IonCol class="tbHead">S_5</IonCol>
//                 <IonCol class="tbHead">S_6</IonCol>
//                 <IonCol class="tbHead">S_7</IonCol>
//                 <IonCol class="tbHead">S_8</IonCol>
//                 <IonCol class="tbHead">S_9</IonCol>
//                 <IonCol class="tbHead">S_10</IonCol>
//               </IonItem>
//               <IonList>
//                 {this.state.senMsg.map((data, index) => (
//                   <SensorData key={index} msg={data} />
//                 ))}
//               </IonList>
//             </IonCol>

//             <IonCol size="2.8">
//               <IonItem>
//                 <IonTitle>Device State</IonTitle>
//               </IonItem>
//               <IonItem>
//                 <IonCol class="tbHead">DEVICEID</IonCol>
//                 <IonCol class="tbHead">STATUS</IonCol>
//                 <IonCol class="tbHead">TIME</IonCol>
//               </IonItem>
//               <IonList>
//                 {this.state.senDev.map((st, idx) => (
//                   <IonItem key={idx} class="statusItem">
//                     <IonCol class="myCol">{st.device_uuid}</IonCol>
//                     <IonCol class="myCol">
//                       {st.status === 1 ? <div className="statusOn"> <i  /></div>: <div className="statusOff"> <i  /></div>}
//                     </IonCol>
//                     <IonCol size="6" class="myCol">{st.timeStamp}</IonCol>
//                   </IonItem>
//                 ))}
//               </IonList>
//             </IonCol>
//           </IonRow>
//         </IonContent>
//       </IonPage>
//     );
//   }
// };

export default IoTDashboard;
