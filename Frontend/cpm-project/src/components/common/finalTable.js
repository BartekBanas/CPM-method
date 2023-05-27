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
    const totalCost = receivedData.totalCost;
    const totalProfil = receivedData.totalProfit;
    const totalRevenue = receivedData.totalRevenue;
    const profitTable = receivedData.profitTable;
    const transportationTable = receivedData.transportationTable;


    const newColumns = columns.map(column => {
        const newCol = { ...column };
        newCol.editable = false;
        return newCol;
    });

    const newDataSource = dataSource.map((row, ix) => {
        const resultRow = profitTable[ix];
        const resultRowT = transportationTable[ix];
        let newRow = { ...row };
        let newRowT = { ...row };
        resultRowT.forEach((value, ix) => {
            newRowT['dynamic' + (ix + 1)] += value;
        });
        resultRow.forEach((value, ix) => {
            newRow['dynamic' + (ix + 1)] = ` ${resultRowT[ix]} [${value}]`;
        });
        return newRow;
    });

    return (
        <Space direction='vertical' size='middle' style={{}} align="center">
            <DynamicTableContent
                dataSource={newDataSource}
                columns={newColumns}
            />
            <Space>
                <Card title="Legenda" style={{ width: 400, margin: 'auto' }}>
                    <p>() - Jednostkowe koszty zakupu dla dostawców lub ceny sprzedaży dla odbiorców</p>
                    <p>[] - Zysk jednostkowy</p>
                    <p>Liczby bez nawiasów to ilość towaru transportowanego</p>
                </Card>
                <Card title="Tabela Transportu" style={{ width: 400, margin: 'auto' }}>
                    <p>Koszt całkowity = {totalCost}</p>
                    <p>Zysk = {totalProfil}</p>
                    <p>Przychód = {totalRevenue}</p>
                </Card>
            </Space>
        </Space >
    );
};

export default FinalTable