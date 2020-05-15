import React, { useState } from 'react';
import ReactDOM from 'react-dom';
import ChartistGraph from 'react-chartist';
import { Button,Modal } from 'antd'
import './graph.scss'
const Graph = ({graphData,isVisiableGraph,setIsVisiableGraph}) => {
  console.log(graphData,isVisiableGraph)
  // const [dis, setDis] = useState(false)
  // const [data, setData] = useState(graphData)
  // const [polylinePoints, setPolylinePoints] = useState('')
  // const [startPoint, setStartPoint] = useState(0)
  // const [endPoint, setEndPoint] = useState(0)
  // const [scrollCount, setScrollCount] = useState(1)
  // const [counterPoints, setCounterPoints] = useState([""])
  // const [counterTime, setCounterTime] = useState([""])
  // const svgTransform = {
  //   'transform' : '1, -1',
  //   'transform-origin' : ''
  // }
    var data = {
        // labels: [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,55,22,77,22,77],
        series: [
          graphData
        ]
      };
   
      var options = {
        high: Math.max(...graphData),
        low:  Math.min(...graphData),
        showArea: true,
        showPoint: true,
        lineSmooth: false,
        width: 1000,
        height: 600,
        // showLabel: true,
        axisY: {
            onlyInteger: true,
            showLabel: true,
            offset: 20
          }
      };
   
      var type = 'Line'
    
    return ( 
      <>
      <Modal
      visible={isVisiableGraph}
      onOk={() => setIsVisiableGraph(!isVisiableGraph)}
      onCancel={() => setIsVisiableGraph(!isVisiableGraph)}
      width={1050}
    >
      <div className="mainGraph__header"><h4 className="mainGraph__header-title">ЭКГ Считыватель</h4></div>
         <ChartistGraph data={data} options={options} type={type} />
         <div className="mainGraph__footer"><Button className="mainGraph__footer-text">Вейвлет преобразование</Button></div>
    </Modal>
    </>     
     );
}
 
export default Graph;