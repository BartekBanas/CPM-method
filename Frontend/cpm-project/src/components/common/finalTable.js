import React from "react";
import { Card, Space } from 'antd';
import DynamicTableContent from "./DynamicTable/DynamicTableContent";


const genRes = (rows = 2, columns = 2) => {
    const array = [];

    for (let i = 0; i < rows; i++) {
        const row = [];
        for (let j = 0; j < columns; j++) {
            // Generate a random value between 0 and 1
            const randomValue = Math.round(Math.random() * 5);
            row.push(randomValue);
        }
        array.push(row);
    }

    return array;
};

const FinalTable = ({ dataSource, columns, receivedData }) => {
    const resp = genRes();

    const newColumns = columns.map(column => {
        const newCol = { ...column };
        newCol.editable = false;
        return newCol;
    });

    const newDataSource = dataSource.map((row, ix) => {
        const resultRow = resp[ix];
        let newRow = { ...row };
        resultRow.forEach((value, ix) => {
            newRow['dynamic' + (ix + 1)] += ` [${value}]`;
        });
        return newRow;
    });

    return (
        <Space direction='vertical' size='middle' style={{}}>
            <DynamicTableContent
                dataSource={newDataSource}
                columns={newColumns}
            />
            <Card title="Legenda" style={{ width: 400, margin: 'auto' }}>
                <p>() - Jednostkowe koszty zakupu dla dostawców lub ceny sprzedaży dla odbiorców</p>
                <p>[] - Zmienne kryterialne</p>
                <p>Liczby bez nawiasów to koszty transpotu</p>
            </Card>
        </Space>
    );
};

export default FinalTable