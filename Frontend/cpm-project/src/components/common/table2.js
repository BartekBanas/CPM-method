import React, { useState } from 'react';
import { Table, Button, Input } from 'antd';

const DynamicTable = (eventFormMP) => {
    const [columns, setColumns] = useState([]); // stan kolumn
    const [data, setData] = useState([]); // stan danych
    console.log(eventFormMP)

    // Dodawanie nowej kolumny
    const addColumn = () => {
        const newColumn = {
            title: `Odbiorca ${columns.length + 1} `,
            dataIndex: `column${columns.length + 1}`,
            key: `column${columns.length + 1}`,
            render: (_, record) => (
                <Input
                    value={record[`column${eventFormMP}`]}
                    onChange={(e) => handleValueChange(e.target.value, record.key, `column${columns.length + 1}`)}
                />
            ),
        };
        setColumns((prevColumns) => [...prevColumns, newColumn]);
        setData((prevData) => prevData.map((row) => ({ ...row, eventFormMP: '' })));
    };

    // Dodawanie nowego wiersza
    const addRow = () => {
        const newRow = {
            key: `row${data.length + 1}`,
            ...columns.reduce((acc, column) => ({ ...acc, [column.dataIndex]: '' }), {}),
        };
        setData((prevData) => [...prevData, newRow]);
    };

    // Aktualizacja wartości w tabeli
    const handleValueChange = (value, rowIndex, columnKey) => {
        setData((prevData) =>
            prevData.map((row) => {
                if (row.key === rowIndex) {
                    return { ...row, [columnKey]: value };
                }
                return row;
            })
        );
    };

    return (
        <div>
            <Button onClick={addColumn}>Dodaj kolumnę</Button>
            <Button onClick={addRow}>Dodaj wiersz</Button>
            <Table dataSource={data} columns={columns} bordered />
        </div>
    );
};

export default DynamicTable;
