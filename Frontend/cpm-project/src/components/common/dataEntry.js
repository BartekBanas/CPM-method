import { Button, Form, Input, Select, Space, Card } from 'antd';
import React from 'react';
import handleAdd from './table.js';
import TableWithInfo from './table.js';

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
                        <Form.Item name="name" label="Nazwa" rules={[{ required: true }]}>
                            <Input id='iName' />
                        </Form.Item>
                        <Form.Item name="time" label="Czas" rules={[{ required: true }]}>
                            <Input id='iTime' type="number" min={0} />
                        </Form.Item>
                        <Form.Item name="futureEvents" label="Nastepstwo Zdarzeń">
                            <Form.Item
                                name="zd1"
                                rules={[{ required: true }]}
                                style={{ display: 'inline-block', width: 'calc(50% - 3px)' }}
                            >
                                <Input id='iZd1' type="number" min={0} />
                            </Form.Item>
                            <div style={{ display: 'inline-block' }}>
                                -
                            </div>
                            <Form.Item
                                name="zd2"
                                rules={[{ required: true }]}
                                style={{ display: 'inline-block', width: 'calc(50% - 3px)' }}
                            >
                                <Input id='iZd2' type="number" min={0} />
                            </Form.Item>
                        </Form.Item>
                        <Form.Item>
                            <Button type="primary" onClick={TableWithInfo} htmlType="submit" style={{ display: 'inline-block', margin: '0 5%' }}>
                                Zatwierdź
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