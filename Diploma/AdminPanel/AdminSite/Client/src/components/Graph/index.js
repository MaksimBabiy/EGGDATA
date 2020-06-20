import React, { useState,useRef, useEffect } from 'react';
import ChartistGraph from 'react-chartist';
import { Button,Modal } from 'antd'
import './graph.scss'

const Graph = ({graphData,isVisiableGraph,setIsVisiableGraph}) => {
 
  const svgRef = useRef(null)
 
  let count = 1;
  // useEffect(() => {
  //   console.log('render')
  //   setTimeout(() => {
     
  //   },100)
  // },[])

    var datamin = {
        // labels: [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,55,22,77,22,77],
        series: [
          graphData
        ]
      };
   
      var options = {
        high: Math.max(...graphData),
        low:  Math.min(...graphData),
        showArea: false,
        showPoint: true,
        lineSmooth: false,
        width: 20000,
        height: 800,
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
     
      console.log(count)
    if (delta > 0) {
     count +=0.5
    }
    else {
      if(count > 1) {
       count -=0.5
      }

    }
   
    svgRef.current.chart.style.transform = `scale(${count},${count})`;
    svgRef.current.chart.style.transformOrigin = x + 'px '+ y+'px';
    }
    const picksDetect = () => {
    let arr = [2091,2092,2077,2070,2071]
    let dots = document.querySelectorAll('.ct-point')
    console.log(dots)
    dots.forEach(item => {
      if ( arr.includes(Number(item.getAttribute('ct:value')))) {
        item.style.stroke = 'black'
        item.style.strokeWidth = '5px'
      }
    })
    }
    return ( 
      <>
      <Modal
      visible={isVisiableGraph}
      onOk={() => setIsVisiableGraph(!isVisiableGraph)}
      onCancel={() => setIsVisiableGraph(!isVisiableGraph)}
      width={1920}
    >
      <div className="mainGraph__header"><h4 className="mainGraph__header-title" >ЕКГ Зчитувач</h4></div>
        <div style={{overflow: 'hidden'}} className="sema" >
        <ChartistGraph data={datamin} options={options} type={type} ref={svgRef} />
        </div>
         <div className="mainGraph__footer"><Button className="mainGraph__footer-text" onClick={() => document.querySelector('.sema').addEventListener('wheel', (e) => onWheel(e))}>Трансформування</Button><Button onClick={() => picksDetect()}>Rpicks</Button></div>
    </Modal>
    </>     
     );
}
 
export default Graph;