import logo from "./logo.svg";
import "./App.css";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import LoginPage from "./Pages/LoginPage";

function App() {
  return (
    <div className="App">
      <LoginPage />
    </div>
  );
}

export default App;
