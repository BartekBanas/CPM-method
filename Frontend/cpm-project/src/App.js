import './App.css';
import { useState } from 'react';
import { Layout } from 'antd';
import backg from './images/backg.jpg';
import AppHeader from './components/common/header';
import InformationCard from './components/common/informator';
import DataEntryForNewTask from './components/common/dataEntry';
import TableWithInfo from './components/common/table';
import Switcher from './components/common/switch';

function App() {
  const [eventForm, setEventForm] = useState({});
  const [method, setMethod] = useState('CPM'); // default is 'CPM'

  return (
    <Layout className="App">
      <header className="App-header" style={{
        backgroundImage: `url(${backg})`,
        backgroundRepeat: 'no-repeat',
        backgroundSize: 'cover'
      }}>
        <AppHeader />
        <Switcher setMethod={setMethod} method={method} />
        <InformationCard method={method} />
        <DataEntryForNewTask setEventForm={setEventForm} method={method} />
        <TableWithInfo eventForm={eventForm} />
      </header>
    </Layout>
  );
}

export default App;
