import React, { useState,useRef, useEffect } from 'react';
import ChartistGraph from 'react-chartist';
import { Button,Modal } from 'antd'
import './graph.scss'


const Graph = ({graphData,isVisiableGraph,setIsVisiableGraph}) => {
  useEffect(() => {


    return () => {
       graphData = []
    }
  }, [])
  const svgRef = useRef(null)
  const [isVisiableResult,setIsVisiableResult] = useState(false)
  const [isVisiableResultToArtur, setIsVisiableResultToArtur] = useState(false)
  let count = 1;
  let arr = []
    var datamin = {
        // labels: [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,55,22,77,22,77],
        series: [
          graphData.points
        ]
      };
   
      var options = {
        high: Math.max(...graphData.points),
        low:  Math.min(...graphData.points),
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
    let peaks = graphData.peaks.map(item => Number(item))
    const picksDetect = () => {
   
    let dots = document.querySelectorAll('.ct-point')
    console.log('peaks', peaks)
    console.log('dots',dots)
    peaks.pop()
    peaks.map(item => {
      dots[item].style.stroke = 'black';
      dots[item].style.strokeWidth = '5px'
    })
    }
    const sem = () => {
      for(let i = 1; i < peaks.length-1; i++) {
        for(let j=i+1; j < peaks.length; j++) {
          arr.push(`${i}:${j}`)
        }
      }
    }
    sem()
 
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
         <div className="mainGraph__footer">
           <Button className="mainGraph__footer-text" onClick={() => document.querySelector('.sema').addEventListener('wheel', (e) => onWheel(e))}>Трансформування</Button>
           <Button onClick={() => picksDetect()}>Rpicks</Button>
           <Button onClick={() => {
             setIsVisiableResult(!isVisiableResult)
             }}>Result</Button>
            <Button onClick={() => {
             setIsVisiableResultToArtur(!isVisiableResultToArtur)
             }}>ResultToArtur</Button>
        </div>
        <Modal
      visible={isVisiableResultToArtur}
      onOk={() => setIsVisiableResultToArtur(!isVisiableResultToArtur)}
      onCancel={() => setIsVisiableResultToArtur(!isVisiableResultToArtur)}
      width={1920}
    >
      <ul className="resultToArtur">
        {graphData.corelationResult.map((item,index) => {
          if(Math.sign(item) == -1 && item < 0.6) {
            return <li><span>{arr[index]}</span>&nbsp;{item}</li>
          }
        })}
        </ul>
    </Modal>
        <Modal
      visible={isVisiableResult}
      onOk={() => setIsVisiableResult(!isVisiableResult)}
      onCancel={() => setIsVisiableResult(!isVisiableResult)}
      width={1920}
    >
      <ul className="result">
        {graphData.corelationResult.map((item,index) => {
          return <li><span>{arr[index]}</span>&nbsp;{item}</li>
        })}
        </ul>
    </Modal>
    </Modal>
    </>     
     );
}
 
export default Graph;