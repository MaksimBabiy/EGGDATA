import React from 'react';
import './whiteblock.scss'

const WhiteBlock = ({ children }) => {
    return (
        <div className="block">{children}</div>
    );
}

export default WhiteBlock;