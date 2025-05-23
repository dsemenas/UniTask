import logo from "./logo.svg";
import "./App.css";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import NotFound from "./pages/NotFoundPage";
import WorkSpacePage from "./pages/WorkSpacePage";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/workspace" element={<ProtectedRoute>
            <WorkSpacePage
          </ProtectedRoute>>} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="*" element={<NotFound />} />
      </Routes>
    </Router>
  );
}

export default App;
