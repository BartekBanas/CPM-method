import { useState, useEffect } from 'react';
import { Button, Space, Drawer, notification } from 'antd';
import { DeleteTwoTone } from '@ant-design/icons';
import { isEmpty } from 'lodash';
import axios from 'axios';

import './DynamicTable.css';
import DynamicTableContent from './DynamicTableContent';
import FinalTable from '../finalTable';

const newColumnProps = {
  title: '',
  dataIndex: "new",
  width: 170,
  editable: true
};

const DynamicTable = ({ eventFormMP }) => {
  const [dataSource, setDataSource] = useState([]);
  const [dataSourceS, setDataSourceS] = useState([]);
  const [dataSourceD, setDataSourceD] = useState([]);
  const [count, setCount] = useState(null);
  const [open, setOpen] = useState(false);

  const [receivedData, setReceivedData] = useState({});

  const showDrawer = () => {
    setOpen(true);
  };

  const onClose = () => {
    setOpen(false);
  };

  useEffect(() => {
    if (isEmpty(eventFormMP)) {
      return;
    }
    if (!isEmpty(eventFormMP.demand)) {
      setDataSourceD((dataSourceD) => {
        return [...dataSourceD, { ...eventFormMP, key: count }];
      });
      handleAddCol();
    }
    else {
      setDataSourceS((dataSourceS) => {
        return [...dataSourceS, { ...eventFormMP, key: count }];
      });
      handleAddRow();
    }
    setCount(count + 1);
  }, [eventFormMP]);

  const [columns, setColumns] = useState([
    {
      title: "",
      dataIndex: "supply",
      rowScope: "row",
      align: "center",
      width: 80,
      fixed: 'left',
      render: (text, record) => {
        return (
          <Space>
            Dostawca {record.key} ({text})
            <DeleteTwoTone onClick={() => handleDeleteRow(record.key)} />
          </Space>
        )
      }
    },
    {
      title: "",
      dataIndex: "padding"
    }
  ]);

  const handleAddRow = () => {
    setDataSource((dataSource) => {
      const last = dataSource.slice(-1);
      const newData = {
        key: ((last[0]?.key || 0) + 1),
        supply: eventFormMP.supply
      };
      return [...dataSource, newData]
    });
  };

  const handleAddCol = () => {
    setColumns((columns) => {
      const newColumns = [...columns];
      const keepLast = newColumns.pop();
      const last = newColumns.slice(-1);
      const id = ((last[0]?.key || 0) + 1);
      const demand = eventFormMP.demand;
      const dataIndex = `dynamic${id}`;

      return [
        ...newColumns,
        {
          ...newColumnProps,
          title: () => {
            return (
              <Space>
                Odbiorca {id} ({demand})
                <DeleteTwoTone onClick={() => handleDeleteCol(dataIndex)} />
              </Space>
            );
          },
          dataIndex,
          key: id
        },
        keepLast
      ];
    });
  };

  const handleCellSave = (row) => {
    setDataSource((dataSource) => {
      const newData = [...dataSource];
      const index = newData.findIndex((item) => row.key === item.key);
      const item = newData[index];

      newData.splice(index, 1, {
        ...item,
        ...row
      });

      return newData;
    });
  };

  const handleDeleteRow = (key) => {
    setDataSource((dataSource) => {
      return [...dataSource].filter((item) => item.key !== key);
    });
  };

  const handleDeleteCol = (dataIndex) => {
    setColumns((columns) => {
      return [...columns].filter((item) => item.dataIndex !== dataIndex);
    });
    setDataSource((dataSource) => {
      return [...dataSource].map((row) => { delete row[dataIndex]; return row; })
    });
  };

  const sendData = (event) => {
    event.preventDefault();

    if (isEmpty(dataSource || eventFormMP)) {
      notification.warning({ message: 'Warning', description: 'Wypełnij forma' });
      return;
    }

    const Suppliers = dataSourceS.map((supp) => {
      return {
        Supply: supp.supply,
        Cost: supp.cost,
      };
    });

    const Recipients = dataSourceD.map((reci) => {
      return {
        Demand: reci.demand,
        Cost: reci.cost,
      };
    });

    const TransportCost = dataSource.map((tranc) => {
      return (
        columns.slice(1, -1).map((column) => {
          const dataIndex = column.dataIndex;
          return tranc[dataIndex];
        })
      );
    });

    axios.post('https://localhost:44363/api/TP', { Suppliers, Recipients, TransportCost })
      .then(response => {
        setReceivedData(response.data)
        console.log(response.data)
        showDrawer();
      })
      .catch(error => { });
  }

  return (
    <h1>
      <Button type="primary" onClick={sendData} style={{ marginBottom: 16 }}>
        Zatwierdź
      </Button>
      <Drawer
        title="Wyniki"
        size="large"
        overflow="hidden"
        placement="bottom"
        closable={true}
        onClose={onClose}
        open={open}
      >
        <FinalTable dataSource={dataSource} columns={columns} receivedData={receivedData} />
      </Drawer>
      <DynamicTableContent
        dataSource={dataSource}
        columns={columns}
        handleCellSave={handleCellSave}
        handleAddRow={handleAddRow}
        handleAddCol={handleAddCol}
      />
    </h1>
  );
};
export { DynamicTable as default };
