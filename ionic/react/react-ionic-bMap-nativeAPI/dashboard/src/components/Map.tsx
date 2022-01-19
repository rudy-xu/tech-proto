import React, { useEffect, useState } from "react";
import { useMapDataApi } from "../hooks/useDataApi";
import { Sites } from "../models/Location";
import { Riders } from "../models/Location";
import axios from "axios";
import { IonList } from "@ionic/react";
import Dialog from "./Dialog";
 
//declare BMap object (BMap is namespace or object when loading baidu map api-----<script>)

declare var BMapGL: any;

const tmpSite = [
    {
        id: 0,
        name: "Sky",
        info: "Dream place",
        lat: 120.657302,
        lng: 31.425559
    },
    {
        id: 1,
        name: "YuanRong",
        info: "Super Market",
        lat: 120.643463,
        lng: 31.427539
    },
    {
        id: 2,
        name: "Lake",
        info: "Just Lake, No Swimming",
        lat: 120.622847,
        lng: 31.430501
    },
    {
        id: 3,
        name: "Tesla",
        info: "Service Center",
        lat: 120.638405,
        lng: 31.417879
    }
];

const startPoint = [
    {
        id: 0,
        lat: 120.651718,
        lng: 31.427787
    },
    {
        id: 0,
        lat: 120.65016,
        lng: 31.425471
    },
    {
        id: 0,
        lat: 120.652621,
        lng: 31.424123
    }
];

// const endPoints = [
//     {
//         id: 0,
//         lat: 120.652154,
//         lng: 31.423537
//     },
//     {
//         id: 0,
//         lat: 120.652019,
//         lng: 31.423337
//     },
//     {
//         id: 0,
//         lat: 120.651705,
//         lng: 31.422929
//     },
//     {
//         id: 0,
//         lat: 120.651417,
//         lng: 31.422497
//     },
//     {
//         id: 0,
//         lat: 120.650357,
//         lng: 31.420698
//     },
//     {
//         id: 0,
//         lat: 120.650245,
//         lng: 31.42056
//     },    
//     {
//         id: 0,
//         lat: 120.65003,
//         lng: 31.420128
//     },    
//     {
//         id: 0,
//         lat: 120.65003,
//         lng: 31.420128
//     },
//     {
//         id: 0,
//         lat: 120.649711,
//         lng: 31.419389
//     },
//     {
//         id: 0,
//         lat: 120.649383,
//         lng: 31.418449
//     },
//     {
//         id: 0,
//         lat: 120.650717,
//         lng: 31.417925
//     },
//     {
//         id: 0,
//         lat: 120.652365,
//         lng: 31.417293
//     },
//     {
//         id: 0,
//         lat: 120.653443,
//         lng: 31.416916
//     },
//     {
//         id: 0,
//         lat: 120.652814,
//         lng: 31.415529
//     },
//     {
//         id: 0,
//         lat: 120.648983,
//         lng: 31.416684
//     },
//     {
//         id: 0,
//         lat: 120.646984,
//         lng: 31.417008
//     },

//     {
//         id: 0,
//         lat: 120.643463,
//         lng: 31.41707
//     },
//     {
//         id: 0,
//         lat: 120.639726,
//         lng: 31.417031
//     },
//     {
//         id: 0,
//         lat: 120.638343,
//         lng: 31.417023
//     },
//     {
//         id: 0,
//         lat: 120.638405,
//         lng: 31.417879
//     }
// ];

interface Props{
 //
}

var interval:any;
class Map extends React.Component<Props,{endPoints: [], dialogVisible: boolean}>{

    map: BMapGL.Map;
    
    constructor(props: Props){
        super(props);
        this.state = {
            endPoints: [],
            dialogVisible: false
        }
    }

    componentDidMount(){
        this.initialize();
        interval = setInterval(
            () => this.getMapData(),
            3000
        );
    }

    componentDidUpdate(){
        console.log("sdads"+this.state.endPoints);

        /**
         * draw line
         */
        // var pois = new Array(6);
        var pois: any= [];
        startPoint.map((rider,idx) => {
            pois.push(new BMapGL.Point(rider.lat, rider.lng));
        });

        // var pois = [
        //     new BMapGL.Point(120.651772,31.425347),
        //     new BMapGL.Point(120.651323,31.424831),
        //     new BMapGL.Point(120.652558,31.424176),
        //     new BMapGL.Point(120.651139,31.422085),
        //     new BMapGL.Point(120.651139,31.422085),
        //     new BMapGL.Point(120.64954,31.423252)
        // ];

        if(pois.length > 0)
        {
            var polyline = new BMapGL.Polyline(pois,{
                enabaleEditing: false,
                enableClicking: true,
                strokeWeight: "8",
                strokeOpacity: "0.8",
                strokeColor: "#18a45b"
            });
          
            this.map.addOverlay(polyline);

            var i=0;
            var interval = setInterval(() => {
                if(i >= this.state.endPoints.length){
                    clearInterval(interval);
                    return;
                }

                this.drowLine(polyline, pois, this.state.endPoints[i]);

                i = i + 1;
            },1000);

        }    
    }

    initialize(): void{

        //Judge whether the map instance already exists
        if(this.map){
            return;
        }

        /**
         * Initialize map
         */
        //create the object of baidu map or baidu map instance
        let map = new BMapGL.Map("mapEle");
        this.map = map;
        // this.instance = map;

        // Create center point
        let center = new BMapGL.Point(120.651718,31.427787);
        //set center point and map level
        //level is zoom level, the level is higher and the proportion is larger
        map.centerAndZoom(center, 15);

        /**
         * Add controls on the map
         */
        // map.addControl(new BMap.MapTypeControl());   //Add the controls of map type. Such as:map, satellite, 3D. (Only when city information is set, MapTypeControl's switch function is available.)
        // map.setCurrentCity("苏州");                 //Set city information displayed on the map.
        map.addControl(new BMapGL.NavigationControl3D());
        map.addControl(new BMapGL.ZoomControl());
        map.addControl(new BMapGL.ScaleControl());
        // map.addControl(new BMapGL.OverviewMapControl());

        map.enableScrollWheelZoom(true);          //Turn on mouse wheel zoom

        let marker = new BMapGL.Marker(center);
        map.addOverlay(marker);

        /**
         * Move map center to the center of the screen
         */
        //panTo(): let the map move smoothly to the new center point
        window.setTimeout(() => {
            map.panTo(new BMapGL.Point(120.649553,31.423246));
        },1500);

        //To solve the problem that center point is in the upper left corner. Set the center point offset pixel. Usually, set to half of <div>
        // map.panBy(930, 475);


        /**
         * Add overlay on the map
         */       
        tmpSite.forEach((site) => {
            var sitePoint = new BMapGL.Point(site.lat,site.lng);
            let markerSite = new BMapGL.Marker(sitePoint);
            map.addOverlay(markerSite);

            //listen overlay
            markerSite.addEventListener("click", () => {
                // alert("click marker");
                this.handleClickDialog();
                /**
                 * information window
                 */
                var opts = {
                    width: 200,
                    height: 100,
                    title: site.name,
                    };
                    var infoWindow = new BMapGL.InfoWindow(site.info,opts);
                map.openInfoWindow(infoWindow, sitePoint);
            });
        });
    }

    drowLine(polyline: any, pois: any, point: any): void {
        pois.push(new BMapGL.Point(point.lat, point.lng));
        polyline.setPath(pois);
    }

    getMapData() {
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
              this.setState({
                  endPoints: respone.data.sites
              });
            })
            .catch((error) => {
              console.log(error);
            });
          };
      
          fetchMapData();
    }

    handleClickDialog(): void {
        console.log("handleClickDialog");
        this.setState({dialogVisible: true});
    }

    closeModal(): void{
        console.log("I am is callback.");
        this.setState({dialogVisible: false});
    }

    render(){
        return (
            <>
            <div id="mapEle" className="mapStyle"></div>
            <Dialog visible={this.state.dialogVisible} closeModal={this.closeModal}></Dialog>
            </>

        );
    }
}

// const Map: React.FC = () => {

//     // const {riderWay} =useMapDataApi();
//     const [ sites, setSites ] = useState<Sites[]>([]);
//     const [ riders, setRiders ] = useState<Riders[]>([]);

//     map: BMapGL.Map;

//     useEffect(() => {
//         showMap();
//       },[riders,sites]);

//     function showMap(): void {

//         setSites(tmpSite);
//         setRiders(startPoint);

//         /**
//          * Initialize map
//          */
//         //create the object of baidu map or baidu map instance
//         var map = new BMapGL.Map("mapEle");
    
//         // Create center point
//         var point = new BMapGL.Point(120.651718,31.427787);
//         //set center point and map level
//         //level is zoom level, the level is higher and the proportion is larger
//         map.centerAndZoom(point, 15); 
    
//         /**
//          * Add controls on the map
//          */
//         // map.addControl(new BMap.MapTypeControl());   //Add the controls of map type. Such as:map, satellite, 3D. (Only when city information is set, MapTypeControl's switch function is available.)
//         // map.setCurrentCity("苏州");                 //Set city information displayed on the map.
//         map.addControl(new BMapGL.NavigationControl3D());
//         map.addControl(new BMapGL.ZoomControl());
//         map.addControl(new BMapGL.ScaleControl());
//         // map.addControl(new BMapGL.OverviewMapControl());
    
//         map.enableScrollWheelZoom(true);          //Turn on mouse wheel zoom

//         var marker = new BMapGL.Marker(point);
//         map.addOverlay(marker);

//         /**
//          * Move map center to the center of the screen
//          */
//         //panTo(): let the map move smoothly to the new center point
//         window.setTimeout(() => {
//             map.panTo(new BMapGL.Point(120.649553,31.423246));
//         },1500);
        
//         //To solve the problem that center point is in the upper left corner. Set the center point offset pixel. Usually, set to half of <div>
//         // map.panBy(930, 475);


//         /**
//          * Add overlay on the map
//          */
//         sites.forEach((site) => {
//             var sitePoint = new BMapGL.Point(site.lat,site.lng);
//             let markerSite = new BMapGL.Marker(sitePoint);
//             map.addOverlay(markerSite);

//             //listen overlay
//             markerSite.addEventListener("click", () => {
//                 alert("click marker");
    
//                 /**
//                  * information window
//                  */
//                 var opts = {
//                     width: 200,
//                     height: 100,
//                     title: site.name,
//                   };
//                   var infoWindow = new BMapGL.InfoWindow(site.info,opts);
//                 map.openInfoWindow(infoWindow, sitePoint);
//             });
//         });

//         /**
//          * draw line
//          */
//         // var pois = new Array(6);
//         var pois: any= [];
//         riders.map((rider,idx) => {
//             pois.push(new BMapGL.Point(rider.lat, rider.lng));
//         });

//         // var pois = [
//         //     new BMapGL.Point(120.651772,31.425347),
//         //     new BMapGL.Point(120.651323,31.424831),
//         //     new BMapGL.Point(120.652558,31.424176),
//         //     new BMapGL.Point(120.651139,31.422085),
//         //     new BMapGL.Point(120.651139,31.422085),
//         //     new BMapGL.Point(120.64954,31.423252)
//         // ];

//         if(pois.length > 0)
//         {
//             var polyline = new BMapGL.Polyline(pois,{
//                 enabaleEditing: false,
//                 enableClicking: true,
//                 strokeWeight: "8",
//                 strokeOpacity: "0.8",
//                 strokeColor: "#18a45b"
//             });
          
//             map.addOverlay(polyline);

//             var i=0;
//             var interval = setInterval(() => {
//                 if(i >= endPoints.length){
//                     clearInterval(interval);
//                     return;
//                 }

//                 drowLine(polyline, pois, endPoints[i]);

//                 i = i + 1;
//             },1000);

//         }    
//     }

//     function drowLine(polyline: any, pois: any, point: any): void {
//         pois.push(new BMapGL.Point(point.lat, point.lng));
//         polyline.setPath(pois);
//     }

//     return (
//         <div id="mapEle" className="mapStyle"></div>
//     );
// }

export default Map;