import { Button, Form, Input, Select, Space, Card } from 'antd';
import React from 'react';

const { Option } = Select;

function DataEntryForNewTask() {
    const [form] = Form.useForm();

    const onReset = () => {
        form.resetFields();
    };

    return (

        <h2>
            <Form
                form={form}
            >
                <Space direction="vertical" size={16}>
                    <Card title="Nowe zdarzenie" style={{ width: 400 }}>
                        <Form.Item name="nazwa" label="Nazwa" rules={[{ required: true }]}>
                            <Input />
                        </Form.Item>
                        <Form.Item name="czas" label="Czas" rules={[{ required: true }]}>
                            <Input />
                        </Form.Item>
                        <Form.Item name="nastepstwozdarzen" label="Nastepstwo Zdarzeń" rules={[{ required: true }]}>
                            <Input />
                        </Form.Item>
                        <Form.Item>
                            <Button type="primary" htmlType="submit">
                                Zatwierdź
                            </Button>
                            <Button htmlType="button" onClick={onReset}>
                                Resetuj
                            </Button>
                        </Form.Item>
                    </Card>
                </Space>
            </Form>
        </h2>
    );
};

export default DataEntryForNewTask;