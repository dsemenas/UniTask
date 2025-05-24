import React, { useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import CommentSection from "../components/CommentSection";
import Logout from "../components/Logout";

export default function WorkSpacePage() {
  const { logout, user } = useContext(AuthContext);
  return (
    <div>
      <Logout />
      <div className="mt-5">
        <h2>Darbo aplink</h2>
      </div>
    </div>
  );
}
