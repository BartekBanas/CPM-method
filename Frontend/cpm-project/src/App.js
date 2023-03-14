import logo from './logo.svg';
import './App.css';
import React from 'react';
import { Button, Layout, Space, Avatar } from 'antd';
import backg from './images/backg.jpg';

import AppHeader from './components/common/header';

function App() {
  return (
    <Layout className="App">
      <header className="App-header" style={{
        backgroundImage: `url(${backg})`,
        backgroundRepeat: 'no-repeat',
        backgroundSize: 'cover'
      }}>
        <img src={logo} className="App-logo" alt="logo" />
        <AppHeader />
        <Space wrap>
          <Button type="primary">Primary Button</Button>
        </Space>
      </header>
    </Layout>
  );
}

export default App;
