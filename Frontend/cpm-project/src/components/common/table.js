import { Button, Empty, Form, Input, Popconfirm, Table, theme, Drawer } from 'antd';
import React, { useContext, useEffect, useRef, useState } from 'react';
import { isEmpty } from 'lodash';
import axios from 'axios';
import CPMDiagram from './CPMgraf';
import test from './t';


const EditableContext = React.createContext(null);
const EditableRow = ({ index, ...props }) => {
    const [form] = Form.useForm();
    return (
        <Form form={form} component={false}>
            <EditableContext.Provider value={form}>
                <tr {...props} />
            </EditableContext.Provider>
        </Form>
    );
};

const EditableCell = ({
    title,
    editable,
    children,
    dataIndex,
    record,
    handleSave,
    ...restProps
}) => {
    const [editing, setEditing] = useState(false);
    const inputRef = useRef(null);
    const form = useContext(EditableContext);
    useEffect(() => {
        if (editing) {
            inputRef.current.focus();
        }
    }, [editing]);
    const toggleEdit = () => {
        setEditing((editing) => !editing);
        form.setFieldsValue({
            [dataIndex]: record[dataIndex],
        });
    };
    const save = async () => {
        try {
            const values = await form.validateFields();
            toggleEdit();
            handleSave({
                ...record,
                ...values,
            });
        } catch (errInfo) {
            console.log('Zapis nieudany', errInfo);
        }
    };
    let childNode = children;
    if (editable) {
        childNode = editing ? (
            <Form.Item
                style={{
                    margin: 0,
                }}
                name={dataIndex}
                rules={[
                    {
                        required: true,
                        message: `${title} jest wymagane!`,
                    },
                ]}
            >
                <Input ref={inputRef} onPressEnter={save} onBlur={save} />
            </Form.Item>
        ) : (
            <div
                className="editable-cell-value-wrap"
                style={{
                    paddingRight: 24,
                }}
                onClick={toggleEdit}
            >
                {children}
            </div>
        );
    }
    return <td {...restProps}>{childNode}</td>;
};
const TableWithInfo = ({ eventForm }) => {

    const [dataSource, setDataSource] = useState([]);

    const [count, setCount] = useState(0);

    const [open, setOpen] = useState(false);

    const [showGraph, setShowGraph] = useState(false);

    const showDrawer = () => {
        setOpen(true);
    };

    const onClose = () => {
        setOpen(false);
    };

    useEffect(() => {
        if (isEmpty(eventForm)) {
            return;
        }
        setDataSource((dataSource) => {
            return [...dataSource, { ...eventForm, key: count }];
        });
        setCount(count + 1);
    }, [eventForm]);

    const handleDelete = (key) => {
        setDataSource((dataSource) => {
            return [...dataSource.filter((item) => item.key !== key)];
        });
    };
    const defaultColumns = [
        {
            title: 'Nazwa',
            dataIndex: 'name',
            editable: true,
        },
        {
            title: 'Czas',
            dataIndex: 'time',
            editable: true,
        },
        {
            title: 'Następstwo Zdarzeń',
            colSpan: 2,
            dataIndex: 'futureEvents',
            editable: true,
        },
        {
            title: 'Następstwo Zdarzeń 2',
            colSpan: 0,
            dataIndex: 'futureEvents2',
            editable: true,
        },
        {
            title: 'Opcje',
            dataIndex: 'options',
            render: (_, record) =>
                dataSource.length >= 1 ? (
                    <Popconfirm
                        title="Na pewno chcesz usunąć?"
                        onConfirm={() => handleDelete(record.key)}
                        okText="Tak"
                        cancelText="Nie"
                    >
                        <a>Usuń</a>
                    </Popconfirm>
                ) : null,
        },
    ];

    const handleSave = (row) => {
        const newData = [...dataSource];
        const index = newData.findIndex((item) => row.key === item.key);
        const item = newData[index];
        newData.splice(index, 1, {
            ...item,
            ...row,
        });
        setDataSource(newData);
    };
    const components = {
        body: {
            row: EditableRow,
            cell: EditableCell,
        },
    };
    const columns = defaultColumns.map((col) => {
        if (!col.editable) {
            return col;
        }
        return {
            ...col,
            onCell: (record) => ({
                record,
                editable: col.editable,
                dataIndex: col.dataIndex,
                title: col.title,
                handleSave,
            }),
        };
    });

    const [receivedData, setReceivedData] = useState({});

    useEffect(() => {
        console.log('Zmiana danych:', receivedData);
    }, [receivedData]);

    const sendData = (event) => {
        event.preventDefault();

        if (isEmpty(dataSource)) {
            return;
        }

        const activities = dataSource.map((activity) => {
            return {
                taskName: activity.name,
                duration: Number(activity.time),
                sequence: [
                    Number(activity.futureEvents),
                    Number(activity.futureEvents2)
                ]
            };
        });

        console.log(activities)
        axios.post('https://localhost:44363/api/CPM', { activities: activities })
            .then(response => {
                setShowGraph(true);
                console.log(response)
                setReceivedData(response.data)
                console.log(receivedData)
            })
            .catch(error => console.log(error))
            .finally(e => {
                setShowGraph(true);
            });
        showDrawer();
    }

    return (
        <h3>
            <div>
                <Button type="primary" onClick={sendData} style={{ marginBottom: 16 }}>
                    Zatwierdź
                </Button>
                <Drawer
                    title="Graf"
                    size="large"
                    overflow="hidden"
                    placement="bottom"
                    closable={true}
                    onClose={onClose}
                    open={open}
                >
                    {showGraph && <CPMDiagram receivedData={receivedData} />}
                </Drawer>
                <Table
                    components={components}
                    rowClassName={() => 'editable-row'}
                    bordered
                    dataSource={dataSource}
                    columns={columns}
                    size="small"
                />
            </div>
        </h3 >
    );
};
export default TableWithInfo;