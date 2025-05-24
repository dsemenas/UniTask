import React, { useContext } from "react";
import { AuthContext } from "../context/AuthContext";

export default function Logout() {
  const { logout, user } = useContext(AuthContext);
  return (
    <div className="p-3 position-relative">
      <div className="position-absolute top-0 end-0 me-2 mt-2">
        <button className="btn btn-primary" onClick={logout}>
          Atsijungti
        </button>
      </div>
      <div>
        <span className="me-2">
          Esate prisijungęs kaip:
          <span className="fw-bold"> {user.username}</span>
        </span>
      </div>
    </div>
  );
}
