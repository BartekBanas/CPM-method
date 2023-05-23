import React from "react";
import { Card } from 'antd';

const FinalTable = ({ receivedData }) => {
    return (
        <h1>
            <Card title="Legenda" style={{ width: 400 }}>
                <p>() - Jednostkowe koszty zakupu dla dostawców lub ceny sprzedaży dla odbiorców</p>
                <p>[] - Zmienne kryterialne</p>
                <p>Liczby bez nawiasów to koszty transpotu</p>
            </Card>
        </h1>
    );
};

export default FinalTable