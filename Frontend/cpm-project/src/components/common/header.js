import React from "react";
import { Avatar, Space, Tooltip } from 'antd';
import logo from './avatars/logo.svg';
import stewie from './avatars/stewie.jpg'
import nojs from './avatars/nojs.jpg'


function AppHeader() {

    return (
        <h1>
            <Space direction="vertical" size={16}>
                <Space wrap size={16}>
                    <Tooltip title="Bartłomiej Burda" placement="bottom" color={'#1890ff'}>
                        <Avatar size={80} src={stewie} />
                    </Tooltip>
                    <Avatar size={120} src={<img src={logo} className="App-logo" alt="logo" />} />
                    <Tooltip title="Bartłomiej Banaś" placement="bottom" color={'#1890ff'}>
                        <Avatar size={80} src={nojs} />
                    </Tooltip>
                </Space>
            </Space>
        </h1>
    );
}

export default AppHeader;