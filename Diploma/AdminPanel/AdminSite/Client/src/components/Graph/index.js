import React, { useState,useRef, useEffect } from 'react';
import ReactDOM from 'react-dom';
import ChartistGraph from 'react-chartist';
import { Button,Modal } from 'antd'
import './graph.scss'
const Graph = ({graphData,isVisiableGraph,setIsVisiableGraph}) => {
  
  const svgRef = useRef(null)
  
 
  let svgTransform = {
    'transform' : '1, 1',
    'transform-origin' : ''
  }
  const [scrollCount, setScrollCount] = useState(1)
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
      
    const onWheel = (event) => {
      let delta = event.deltaY || event.detail || event.wheelDelta;
      let x = event.offsetX==undefined?event.layerX:event.offsetX;
      let y = event.offsetY;
    if (delta > 0) setScrollCount(scrollCount + 0.5)
    else {
      if(scrollCount > 1)
      setScrollCount(scrollCount - 0.5);
    }
    svgRef.current.chart.style.transform = 'scale('+scrollCount+','+scrollCount+')';
    svgRef.current.chart.style.transformOrigin = x + 'px '+y+'px';
    }
    return ( 
      <>
      <Modal
      visible={isVisiableGraph}
      onOk={() => setIsVisiableGraph(!isVisiableGraph)}
      onCancel={() => setIsVisiableGraph(!isVisiableGraph)}
      width={1050}
    >
      <div className="mainGraph__header"><h4 className="mainGraph__header-title" >ЭКГ Считыватель</h4></div>
        <div onWheel={(event) => onWheel(event)} style={{overflow: 'hidden'}}>
        <ChartistGraph data={data} options={options} type={type} ref={svgRef} />
        </div>
         <div className="mainGraph__footer"><Button className="mainGraph__footer-text">Вейвлет преобразование</Button></div>
    </Modal>
    </>     
     );
}
 
export default Graph;