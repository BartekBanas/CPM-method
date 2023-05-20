import { useState, useEffect } from 'react';
import { Button, Empty, Table, Space } from 'antd';
import { DeleteTwoTone } from '@ant-design/icons';
import EditableCell from './EditableCell';
import EditableRow from './EditableRow';
import { isEmpty } from 'lodash';

import './DynamicTable.css';

const newColumnProps = {
  title: '',
  dataIndex: "new",
  width: 170,
  editable: true
};

const DynamicTableContent = ({ dataSource, columns, handleCellSave, handleAddRow, handleAddCol }) => {
  const components = {
    body: {
      row: EditableRow,
      cell: EditableCell
    }
  };

  // for setting onCell - passing props per cell of this column
  const mappedCols = columns.map((col) => {
    if (!col.editable) {
      return {
        ...col,
        onCell: (record) => ({
          record,
          rowScope: col.rowScope
        })
      };
    }

    return {
      ...col,
      onCell: (record) => ({
        record,
        editable: col.editable,
        dataIndex: col.dataIndex,
        title: col.title,
        handleSave: handleCellSave
      })
    };
  });

  return (
    <div>
      <Table
        locale={{ emptyText: <Empty image={Empty.PRESENTED_IMAGE_SIMPLE} description="Wypełnij formularz" /> }}
        components={components}
        rowClassName={() => "editable-row"}
        bordered
        sticky
        dataSource={dataSource}
        columns={mappedCols}
        style={{ maxWidth: '80%', margin: 'auto' }}
        pagination={false}
        scroll={{ x: true }}
      />
    </div>
  );
};


const DynamicTable = ({ eventFormMP }) => {
  const [dataSource, setDataSource] = useState([]);
  const [dataSourceS, setDataSourceS] = useState([]);
  const [dataSourceD, setDataSourceD] = useState([]);
  const [count, setCount] = useState(null);

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

  console.log(dataSource)

  const sendData = (event) => {
    event.preventDefault();

    if (isEmpty(dataSource || eventFormMP)) {
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
      return {
        TransportCost: columns.slice(1, -1).map((column) => {
          const dataIndex = column.dataIndex;
          return tranc[dataIndex];
        })
      };
    });


    const FinalData = {
      Suppliers,
      Recipients,
      TransportCost
    }

    console.log(FinalData)
    // axios.post('https://localhost:44363/api/CPM', { activities: activities })
    //     .then(response => {
    //         setShowGraph(true);
    //         console.log(response)
    //         setReceivedData(response.data)
    //         console.log(receivedData)
    //         showDrawer();
    //     })
    //     .catch(error => {
    //         if (error.response) {
    //             if (error.response.status === 400) {
    //                 setError(error.message);
    //                 setModalVisible(true);
    //                 return;
    //             } else if (error.response.status === 415) {
    //                 setError(error.message);
    //                 setModalVisible(true);
    //                 return;
    //             }
    //         } else if (error.request) {
    //             setError(error.message);
    //             setModalVisible(true);
    //             return;
    //         } else {
    //             setError(error.message);
    //             setModalVisible(true);
    //             return;
    //         }
    //     })
    //     .finally(e => {
    //         setShowGraph(true);
    //     });

  }



  return (
    <h1>
      <Button type="primary" onClick={sendData} style={{ marginBottom: 16 }}>
        Zatwierdź
      </Button>
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
