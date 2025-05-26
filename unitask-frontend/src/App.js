import logo from "./logo.svg";
import "./App.css";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import NotFound from "./pages/NotFoundPage";
import WorkSpacePage from "./pages/WorkSpacePage";
import ProtectedRoute from "./Routes/ProtectedRoute";
import GroupPage from "./pages/GroupPage";

function App() {
  return (
    <Router>
      <Routes>
        <Route
          path="/workspace"
          element={
            <ProtectedRoute>
              <WorkSpacePage />
            </ProtectedRoute>
          }
        />
        <Route
          path="/group/:groupID"
          element={
            <ProtectedRoute>
              <GroupPage />
            </ProtectedRoute>
          }
        />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="*" element={<NotFound />} />
        <Route path="/" element={<Navigate to="/workspace" replace />} />
      </Routes>
    </Router>
  );
}

export default App;
