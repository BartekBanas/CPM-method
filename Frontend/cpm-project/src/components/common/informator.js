import { Space, Card } from 'antd';

function InformationCard({ method }) {
    return (
        <Space direction="vertical" size={8}>
            <Card title={method === 'CPM' ? 'CPM Method' : 'Mediator Method'} style={{ width: 400 }}>
                <p>Program that allows user to solve tasks from the  {method === 'CPM' ? 'CPM method in the form of a graph' : 'mediator method'}</p>
            </Card>
        </Space>
    );
}

export default InformationCard;