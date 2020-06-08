import React, { useState,useRef, useEffect } from 'react';
import ChartistGraph from 'react-chartist';
import { Button,Modal } from 'antd'
import './graph.scss'
const Graph = ({graphData,isVisiableGraph,setIsVisiableGraph}) => {
  useEffect(() => {
    console.log('mounted')
    console.log(document)
    setTimeout(() => {
      document.querySelector('.sema').addEventListener('wheel', onWheel)
    },1000)
    
  },[])
  const svgRef = useRef(null)
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
        showArea: false,
        showPoint: false,
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
      
    const onWheel = (e) => {
    
      let delta = e.deltaY
      let x = e.offsetX==undefined?e.layerX:e.offsetX;
      let y = e.offsetY;
  
    console.log(typeof delta)
  
    if (delta > 0) {
      setScrollCount(scrollCount + 0.5)
      console.log('suka')
    }
    else {
      if(scrollCount > 1) {
        setScrollCount(scrollCount - 0.5)
      }
      
    }
    console.log(scrollCount)
    svgRef.current.chart.style.transform = 'scale('+scrollCount+','+scrollCount+')';
    svgRef.current.chart.style.transformOrigin = x + 'px '+ y+'px';
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
        <div style={{overflow: 'hidden'}} className="sema">
        <ChartistGraph data={data} options={options} type={type} ref={svgRef} />
        </div>
         <div className="mainGraph__footer"><Button className="mainGraph__footer-text" >Вейвлет преобразование</Button></div>
    </Modal>
    </>     
     );
}
 
export default Graph;