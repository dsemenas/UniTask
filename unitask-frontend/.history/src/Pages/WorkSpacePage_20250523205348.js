import React, { useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import CommentSection from "../components/CommentSection";

export default function WorkSpacePage() {
  const { logout } = useContext(AuthContext);
  return (
    <div className="p-3 position-relative">
      <div>
        <button
          className="btn btn-primary"
          onClick={logout}
        >
          Atsijungti
        </button>
      </div>
      <CommentSection />
    </div>
  );
}
