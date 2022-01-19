import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';


class BaiduMap extends React.Component {
 
componentDidMount () {
//   var BMap = window.BMap
  var map = new BMap.Map("allmap"); // 创建Map实例
  map.centerAndZoom(new BMap.Point(116.404, 39.915), 11); // 初始化地图,设 置中心点坐标和地图级别
  map.addControl(new BMap.MapTypeControl()); //添加地图类型控件
  map.setCurrentCity("北京"); // 设置地图显示的城市 此项是必须设置的
  map.enableScrollWheelZoom(true); //开启鼠标滚轮缩放
}
 
render () {
  return (
    <div>
    <div
      id='allmap'
      style={{
        width:'100vw',
        height:'100vh'
      }} />
</div>
    )
  }
}
ReactDOM.render(
<BaiduMap />,
document.getElementById('root')
)