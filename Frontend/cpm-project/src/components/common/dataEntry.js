import { Button, Form, Input, Space, Card } from 'antd';

function DataEntryForNewTask({ setEventForm }) {
    const [form] = Form.useForm();

    const onReset = () => {
        form.resetFields();
    };

    const handleFinished = (values) => {
        console.log(values);
        setEventForm(values);
    }

    return (

        <h2>
            <Form
                form={form}
                onFinish={handleFinished}
            >
                <Space direction="vertical" size={16}>
                    <Card title="Nowe zdarzenie" style={{ width: 400 }}>
                        <Form.Item name="name" label="Nazwa" rules={[{ required: true }]}>
                            <Input id='iName' />
                        </Form.Item>
                        <Form.Item name="time" label="Czas" rules={[{ required: true }]}>
                            <Input id='iTime' type="number" min={0} />
                        </Form.Item>
                        <Form.Item label="Nastepstwo Zdarzeń" required>
                            <Form.Item
                                name="futureEvents"
                                rules={[{ required: true }]}
                                style={{ display: 'inline-block', width: 'calc(50% - 3px)' }}
                            >
                                <Input id='iZd1' type="number" min={0} />
                            </Form.Item>
                            <div style={{ display: 'inline-block', lineHeight: '32px', textAlign: 'center' }}>
                                -
                            </div>
                            <Form.Item
                                name="futureEvents2"
                                rules={[{ required: true }]}
                                style={{ display: 'inline-block', width: 'calc(50% - 3px)' }}
                            >
                                <Input id='iZd2' type="number" min={0} />
                            </Form.Item>
                        </Form.Item>
                        <Form.Item>
                            <Button type="primary" htmlType="submit" style={{ display: 'inline-block', margin: '0 5%' }}>
                                Prześlij
                            </Button>
                            <Button htmlType="button" onClick={onReset} style={{ display: 'inline-block', margin: '0 5%' }}>
                                Reset
                            </Button>
                        </Form.Item>
                    </Card>
                </Space>
            </Form>
        </h2>
    );
};

export default DataEntryForNewTask;