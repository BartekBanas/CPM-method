import { Space, Card } from 'antd';
import React from 'react';

function InformationCard() {
    return (
        <Space direction="vertical" size={8}>
            <Card title="Metoda CPM" style={{ width: 400 }}>
                <p>Program umożliwiający rozwiązywanie zadań z metody CPM w formie grafu</p>
            </Card>
        </Space>
    );
}

export default InformationCard;