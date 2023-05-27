import React from "react";
import { Card, Space } from 'antd';
import DynamicTableContent from "./DynamicTable/DynamicTableContent";

const FinalTable = ({ dataSource, columns, receivedData, currency }) => {
    const totalCost = receivedData.totalCost;
    const totalProfil = receivedData.totalProfit;
    const totalRevenue = receivedData.totalRevenue;
    const profitTable = receivedData.profitTable;
    const transportationTable = receivedData.transportationTable;


    const newColumns = columns.map(column => {
        const newCol = { ...column };
        newCol.editable = false;
        if (newCol.render) {
            newCol.render = (text, record) => {
                return (
                    <Space>
                        Dostawca {record.key} ({text})
                    </Space>
                );
            };
        }

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
            newRow['dynamic' + (ix + 1)] = `${resultRowT[ix]} [${value} ${currency}]`;
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
                    <p>Koszt całkowity = {totalCost} {currency}</p>
                    <p>Zysk = {totalProfil} {currency}</p>
                    <p>Przychód = {totalRevenue} {currency}</p>
                </Card>
            </Space>
        </Space >
    );
};

export default FinalTable