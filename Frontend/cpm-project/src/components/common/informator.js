import { Space, Card } from 'antd';
import React from 'react';

function InformationCard() {
    return (
        <Space direction="vertical" size={8}>
            <Card title="Metoda CPM" style={{ width: 400 }}>
                <p>Program pozwalający obliczyć coś tam coś tam się dopisze</p>
            </Card>
        </Space>
    );
}

export default InformationCard;