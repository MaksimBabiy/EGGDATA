import React from 'react';
import ReactDOM from 'react-dom';
import ChartistGraph from 'react-chartist';
import './graph.scss'
const Graph = () => {
    var data = {
        labels: [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,55,22,77,22,77],
        series: [
          [1, 2, 4, 8, 6, -2, -1, -4, -6, -2]
        ]
      };
   
      var options = {
        high: 20,
        low: -20,
        showArea: false,
        // showLabel: true,
        axisY: {
            onlyInteger: true,
            offset: 20
          }
      };
   
      var type = 'Line'
    return ( 
      <div className="mainGraph">
        <ChartistGraph data={data} options={options} type={type} />
      </div>
     );
}
 
export default Graph;