import './App.css';
import { useState } from 'react';
import { Layout } from 'antd';
import backg from './images/backg.jpg';
import AppHeader from './components/common/header';
import InformationCard from './components/common/informator';
import DataEntryForNewTask from './components/common/dataEntry';
import TableWithInfo from './components/common/table';
import Switcher from './components/common/switch';
import DynamicTable from './components/common/DynamicTable/DynamicTable';
//import JD from './components/common/t';

function App() {
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
