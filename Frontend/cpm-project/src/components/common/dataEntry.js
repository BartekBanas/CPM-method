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
                            <Form.Item
                                name="zd1"
                                rules={[{ required: true }]}
                                style={{ display: 'inline-block', width: 'calc(50% - 3px)' }}
                            >
                                <Input />
                            </Form.Item>
                            <div style={{ display: 'inline-block' }}>
                                -
                            </div>
                            <Form.Item
                                name="zd2"
                                rules={[{ required: true }]}
                                style={{ display: 'inline-block', width: 'calc(50% - 3px)' }}
                            >
                                <Input />
                            </Form.Item>
                        </Form.Item>
                        <Form.Item>
                            <Button type="primary" htmlType="submit" style={{ display: 'inline-block', margin: '0 5%' }}>
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