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
        newCol.showDeleteButton = 'none';
        newCol.editable = false;
        if (newCol.render) {
            newCol.render = (text, record) => {
                return (
                    <Space>
                        Supplier {record.key} ({text})
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
                    <p>() - Unit purchase costs for suppliers or sales prices for recipients</p>
                    <p>[] - Unit profit</p>
                    <p>Numbers without brackets are the amount of goods transported</p>
                </Card>
                <Card title="Tabela Transportu" style={{ width: 400, margin: 'auto' }}>
                    <p>Total cost = {totalCost} {currency}</p>
                    <p>Total profit = {totalProfil} {currency}</p>
                    <p>Total reveune = {totalRevenue} {currency}</p>
                </Card>
            </Space>
        </Space >
    );
};

export default FinalTable