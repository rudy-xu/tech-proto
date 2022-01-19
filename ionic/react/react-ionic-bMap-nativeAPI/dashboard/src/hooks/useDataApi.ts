import {useState, useEffect} from 'react';
import axios from 'axios';
import { useIonViewDidEnter, useIonViewWillLeave } from '@ionic/react';
import { Msg } from '../models/IotDashboardData';
import { VideoData } from '../models/VideoData';
import { Riders } from '../models/Location';
import "../mock";

var msgInterval: any;
export function useDataApi(){
  const url_msg: string = localStorage.getItem("messageUrl")!;

  const [ msgData, setMsgData ] = useState<Msg>({
    recordID: "",
    sessionID: "",
    timeStamp: "",
    data: []
  });

  const [ tokenFlag, setTokenFlag ] = useState(false);
  // const [ statusData, setStatus ] = useState<Dev[]>([]);
  // const [isLoading, setIsLoading ] = useState(false);
  
  //Ionic lifecycle. when enter into page, fire the refresh
  useIonViewDidEnter(() => {
    refreshData();
  });
  
  useIonViewWillLeave(() => {
    console.log("leave lotDashboard");
    clearInterval(msgInterval);
  });

  //React hook. when finish rendering, msgData and statusData change, fire the refresh
  useEffect(() =>{
    msgInterval = setInterval(
      () => refreshData(),
      3000
    );
    return () => {
      clearInterval(msgInterval);
      // console.log("clearInterval");
    }
  },[msgData]);

  function refreshData(): void {
    const acc_token = localStorage.getItem("token");

    const fetchData = async () => {
      console.log("fetch Data");

      // setIsLoading(true);

      await axios({
        method: "GET",
        url: url_msg,
        // url: "http://10.86.5.205:5000/api/DeviceMessage/top/1?device=plc-2",
        headers:{
          "Content-Type": "application/json",
          "Authorization": `Basic ${acc_token}`
        }
      })
      .then((respone) => {
        if(respone.data.length != 0)
        {
          let tmpData = respone.data[0];

          tmpData.data = JSON.parse(tmpData.data);
          console.log(tmpData);
          setMsgData(tmpData);
        } 
      })
      .catch((error) => {
        console.log("error: "+error);
        if(error.request == undefined || error.request.status == 401)
        {
          localStorage.setItem("token","");
          setTokenFlag(true);
        }
      });
  
      // setIsLoading(false);
    };
    fetchData();
  }

  return {
      msgData,
      tokenFlag
      // statusData
  };
}

var videoInterval: any;
export function useVideoDataApi(){

  const url_video: string = localStorage.getItem("videoUrl")!;

  const [ videoData, setVideoData ] = useState<VideoData[]>([]);

  useIonViewDidEnter(() => {
    getVideoData();
  });

  useIonViewWillLeave(() => {
    console.log("leave video page");
    clearInterval(videoInterval);
  });

  useEffect(() => {
    videoInterval = setInterval(
      () => getVideoData(),
      5000
    );
    return () => {
      clearInterval(videoInterval);
    }
  },[videoData]);

  function getVideoData(): void {
    const fetchVideoData = async() => {
      console.log("fetch video");

      await axios({
        method: "GET",
        url: url_video,
        // url: "http://10.86.5.205:5270/objects",
        headers:{
          "Content-Type": "application/json"
          // "Authorization": ""
        }
      })
      .then((response) => {
        if(response.data.length !== 0)
        {
          console.log(response.data.data);
          setVideoData(response.data.data);
        }
      })
      .catch((error) => {
        console.log(error);
      });
    };
    fetchVideoData();
  }

  return {
    videoData
  }
}

var mapDataInterval: any;
export function useMapDataApi(){

  const [ riderWay, setRiderWay ] = useState<Riders[]>([]);

  useIonViewWillLeave(() => {
    console.log("leave map UI");
    clearInterval(mapDataInterval);
  });

  useEffect(() => {
    mapDataInterval = setInterval(
      () => getMapData(),
      3000
    );

    return () => {
      clearInterval(mapDataInterval);
    }
  },[riderWay]);

  function getMapData(): void {
    const fetchMapData = async () => {
      console.log("fetch map data");
      
      await axios({
        method: "GET",
        url: "/api/map",
        headers: {
          "Content-Type": "application/json"
        }
      })
      .then((respone) => {
        console.log(respone.data.sites);
      })
      .catch((error) => {
        console.log(error);
      });
    };

    fetchMapData();
  }

  return {
    riderWay
  };
}