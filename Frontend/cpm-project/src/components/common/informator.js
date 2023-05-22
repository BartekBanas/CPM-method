import { Space, Card } from 'antd';

function InformationCard({ method }) {
    return (
        <Space direction="vertical" size={8}>
            <Card title={method === 'CPM' ? 'Metoda CPM' : 'Metoda Pośrednika'} style={{ width: 400 }}>
                <p>Program umożliwiający rozwiązywanie zadań z metody {method === 'CPM' ? 'CPM w formie grafu' : 'pośrednika'}</p>
            </Card>
        </Space>
    );
}

export default InformationCard;