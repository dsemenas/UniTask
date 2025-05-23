import React, { useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import CommentSection from "../components/CommentSection";

export default function WorkSpacePage() {
  const { logout } = useContext(AuthContext);
  return (
    <div className="p-3 position-relative">
      <button
        className="btn btn-primary position-absolute top-0 end-0 me-2 mt-2"
        onClick={logout}
      >
        Atsijungti
      </button>
      <CommentSection />
    </div>
  );
}
