import React from 'react'
import { UploadOutlined } from '@ant-design/icons';
import { Button, Upload } from 'antd'
const TestPage = () => {
    const props = {
        name: 'file',
        action: 'https://www.mocky.io/v2/5cc8019d300000980a055e76',
        headers: {
          authorization: 'authorization-text',
        },
        onChange(info) {
          console.log(info)
        },
      };
    return (
        <div style={{height: '100%'}}>
            <div style={{marginTop: 100}}>
            <Upload {...props}>
          <Button>
            <UploadOutlined /> Click to Upload
          </Button>
        </Upload>
            </div>
        </div>
    )
}

export default TestPage