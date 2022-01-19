
var map = new BMap.Map('mapEle');  
var poi = new BMap.Point(116.307852,40.057031);
map.centerAndZoom(poi, 16);
map.enableScrollWheelZoom();

var content = '<div style="margin:0;line-height:20px;padding:2px;">' +
                    '<img src="../img/baidu.jpg" alt="" style="float:right;zoom:1;overflow:hidden;width:100px;height:100px;margin-left:3px;"/>' +
                    '地址：上地十街10号<br/>电话：(010)59928888<br/>简介：百度大厦位于北京市海淀区西二旗地铁站附近，为百度公司综合研发及办公总部。' +
                  '</div>';

//创建检索信息窗口对象
var searchInfoWindow = null;
searchInfoWindow = new BMapLib.SearchInfoWindow(map, content, {
        title  : "百度大厦",      //标题
        width  : 290,             //宽度
        height : 105,              //高度
        panel  : "panel",         //检索结果面板
        enableAutoPan : true,     //自动平移
        searchTypes   :[
            BMAPLIB_TAB_SEARCH,   //周边检索
            BMAPLIB_TAB_TO_HERE,  //到这里去
            BMAPLIB_TAB_FROM_HERE //从这里出发
        ]
    });
var marker = new BMap.Marker(poi); //创建marker对象
marker.enableDragging(); //marker可拖拽
marker.addEventListener("click", function(e){
    searchInfoWindow.open(marker);
})
map.addOverlay(marker); //在地图中添加marker