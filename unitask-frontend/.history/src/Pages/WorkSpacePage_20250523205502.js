import React, { useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import CommentSection from "../components/CommentSection";

export default function WorkSpacePage() {
  const { logout, user } = useContext(AuthContext);
  return (
    <div className="p-3 position-relative">
      <div className="position-absolute top-0 end-0 me-2 mt-2">
        <span className="me-1">Esate prisijungęs kaip:{user}</span>
        <button className="btn btn-primary" onClick={logout}>
          Atsijungti
        </button>
      </div>
      <CommentSection />
    </div>
  );
}
