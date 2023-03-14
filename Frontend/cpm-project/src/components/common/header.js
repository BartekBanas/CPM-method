import React from "react";
import { UserOutlined } from '@ant-design/icons';
import { Avatar, Space } from 'antd';

function AppHeader() {
    return (
        <h1>
            <Space direction="vertical" size={16}>
                <Space wrap size={16}>
                    <Avatar size={64} icon={<UserOutlined />} />
                    <Avatar size={64} icon={<UserOutlined />} />
                    <Avatar size={64} icon={<UserOutlined />} />
                    <Avatar size={64} icon={<UserOutlined />} />
                </Space>
            </Space>
        </h1>
    );
}

export default AppHeader;