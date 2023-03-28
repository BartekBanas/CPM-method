import './App.css';
import { useState } from 'react';
import { Button, Layout, Space } from 'antd';
import backg from './images/backg.jpg';

import AppHeader from './components/common/header';
import InformationCard from './components/common/informator';
import DataEntryForNewTask from './components/common/dataEntry';
import TableWithInfo from './components/common/table';

function App() {
  const [eventForm, setEventForm] = useState({});

  return (
    <Layout className="App">
      <header className="App-header" style={{
        backgroundImage: `url(${backg})`,
        backgroundRepeat: 'no-repeat',
        backgroundSize: 'cover'
      }}>
        <AppHeader />
        <InformationCard />
        <DataEntryForNewTask setEventForm={setEventForm} />
        <TableWithInfo eventForm={eventForm} />
      </header>
    </Layout>
  );
}

export default App;
