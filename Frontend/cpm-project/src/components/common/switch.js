import React, { useState } from 'react';
import { Button, Radio, Space, Divider } from 'antd';

function Switcher() {

    const [method, setMethod] = useState('CPM'); // default is 'CPM'


    return (
        <h1>
            <Space>
                <Radio.Group value={method} onChange={(e) => setMethod(e.target.value)}>
                    <Radio.Button type="primary" value={'CPM'}>Metoda CPM</Radio.Button>
                    <Radio.Button type="primary" value={'Posrednika'}>Metoda Pośrednika</Radio.Button>
                </Radio.Group>
            </Space>
        </h1>
    );
}

export default Switcher;