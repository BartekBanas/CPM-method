import './App.css';
import { useState, useCallback } from 'react';
import { Layout, notification } from 'antd';
import AppHeader from './components/common/header';
import InformationCard from './components/common/informator';
import DataEntryForNewTask from './components/common/dataEntry';
import TableWithInfo from './components/common/table';
import Switcher from './components/common/switch';
import DynamicTable from './components/common/DynamicTable/DynamicTable';
import axios from 'axios';

axios.interceptors.response.use((response) => {
  return response;
}, (error) => {
  if (error?.response?.data[0]?.errorMessage) {
    notification.error({
      duration: 7,
      message: 'Error',
      description: error.response.data[0].errorMessage,
    });
  }

  return Promise.reject(error);
});

const App = () => {
  const [eventForm, setEventForm] = useState({});
  const [eventFormMP, setEventFormMP] = useState({});
  const [method, setMethod] = useState('Posrednika'); // default is 'CPM'

  return (
    <Layout className="App">
      <header className="App-header">
        <AppHeader />
      </header>
      <Switcher setMethod={setMethod} method={method} />
      <InformationCard method={method} />
      <DataEntryForNewTask setEventForm={setEventForm} setEventFormMP={setEventFormMP} method={method} />
      {method === 'CPM' && <TableWithInfo eventForm={eventForm} />}
      {method === 'Posrednika' && <DynamicTable eventFormMP={eventFormMP} />}
    </Layout>
  );
}

export default App;
