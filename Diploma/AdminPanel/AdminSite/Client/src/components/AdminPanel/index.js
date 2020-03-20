import React, { useState } from 'react'
import { connect } from 'react-redux'
import { PatientsTable } from 'components'
import { DoctorsTable } from 'containers'
import {VerticalAlignTopOutlined,UserOutlined,MenuOutlined,MailOutlined,PieChartOutlined,BuildOutlined,ExportOutlined,UsergroupAddOutlined,BlockOutlined,GroupOutlined,BookOutlined,CalendarOutlined} from '@ant-design/icons';
import './index.scss'

const AdminPanel = ({userData}) => {
   const [tableContent, setTableContent] = useState(false)
    return (
    <>
    <div className="bg"><br/><br/><br/></div>
    <div className="admin-header">
            <div className="nav-switch" id="hum-menu">
             <MenuOutlined />
            </div>
            <div className="user-panel">
                <p className="user-panel__name">{userData.email}</p>
                <div className="user-panel__photo">
                <UserOutlined />
                </div> 
            </div>
    </div>
    <div className="admin-page">
        <div className="admin-nav" id="navigation">
            <div className="admin-info">
                <div className="admin-photo">
                    <UserOutlined />
                </div>
                <div className="admin-name">
                    <p>{userData.email}</p>
                    <div className="admin-name__status"><span className="status" className="cl">online</span></div>
                </div>
            </div>
            <ul className="admin-menu">
                    <li className="link-menu" onClick={() => setTableContent(false)}>
                        <UsergroupAddOutlined/>
                        <a >Список пациентов</a>
                    </li>
                    <li className="link-menu" onClick={() => setTableContent(true)}>
                        <UsergroupAddOutlined/>
                        <a >Список докторов</a>
                    </li>
                    <li className="link-menu">
                         <BlockOutlined />
                        <a href="#">Dashboard (in developing)</a>
                    </li>
                    <li className="link-menu">
                         <GroupOutlined />
                        <a href="#">Users (in developing)</a>
                    </li>
                    <li className="link-menu">
                        <BookOutlined />
                        <a href="#">Database Joins (in developing)</a>
                    </li>
                    <li className="link-menu">
                         <CalendarOutlined />
                        <a href="#">Invoice Example (in developing)</a>
                    </li>
                    <li className="link-menu">
                        <ExportOutlined />
                        <a href="#">Backup & Export (in developing)</a>
                    </li>
                    <li className="link-menu">
                        <BuildOutlined />
                        <a href="#">UI Components (in developing)</a>
                    </li>
                    <li className="link-menu">
                        <PieChartOutlined />
                        <a href="#">Charts (in developing)</a>
                    </li>
                    <li className="link-menu">
                        <CalendarOutlined />
                        <a href="#">Calendar (in developing)</a>
                    </li>
                    <li className="link-menu">
                        <MailOutlined />
                        <a href="#">Mailbox (in developing)</a>
                    </li>
                    <li className="link-menu">
                    <VerticalAlignTopOutlined />
                        <a href="#">Multilevel (in developing)</a>
                    </li>        
            </ul>
            
        </div>
        <div className="admin-page-main">
            {tableContent ? <DoctorsTable /> : <PatientsTable /> }
        </div>
    </div>
    </>
    )
}

export default connect(({user}) => ({
    userData: user.userData
  }))(AdminPanel);
