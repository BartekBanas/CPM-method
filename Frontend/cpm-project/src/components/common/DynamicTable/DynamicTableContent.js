import { Empty, Table } from 'antd';
import EditableCell from './EditableCell';
import EditableRow from './EditableRow';

const DynamicTableContent = ({ dataSource, columns, handleCellSave }) => {
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
                locale={{ emptyText: <Empty image={Empty.PRESENTED_IMAGE_SIMPLE} description="Fill in form" /> }}
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

export default DynamicTableContent;