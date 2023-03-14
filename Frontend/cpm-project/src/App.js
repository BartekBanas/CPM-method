import logo from './logo.svg';
import './App.css';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <span className="App-names">
          Bartłomiej Burda, Bartłomiej Banaś, Dawid Chmielowiec, Damian Błażowski
        </span>
        <ul>
          <li className="button-1">
            test guzik
          </li>
          <li className="button-1">
            test guzik
          </li>
          <li className="button-1">
            test guzik
          </li>
        </ul>
      </header>
    </div>
  );
}

export default App;
