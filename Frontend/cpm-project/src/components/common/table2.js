import { Button, Form, Input, Popconfirm, Table } from 'antd';
import React, { useContext, useEffect, useRef, useState } from 'react';
import { isEmpty } from 'lodash';


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
const TableWithInfo = ({ eventFormMP, method }) => {

    const [dataSource, setDataSource] = useState([]);

    const [count, setCount] = useState(0);

    useEffect(() => {
        if (isEmpty(eventFormMP)) {
            return;
        }
        setDataSource((dataSource) => {
            return [...dataSource, { ...eventFormMP, key: count }];
        });
        setCount(count + 1);
    }, [eventFormMP]);

    const handleDelete = (key) => {
        setDataSource((dataSource) => {
            return [...dataSource.filter((item) => item.key !== key)];
        });
    };
    const defaultColumns = [
        {
            title: 'Nazwa',
            dataIndex: 'supply',
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

    return (
        <h3>
            <div>
                <Button type="primary" style={{ marginBottom: 16 }}>
                    Zatwierdź
                </Button>
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