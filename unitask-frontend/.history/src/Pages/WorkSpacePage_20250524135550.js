import React, { useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import CommentSection from "../components/CommentSection";
import Logout from "../components/Logout";

export default function WorkSpacePage() {
  const { logout, user } = useContext(AuthContext);
  return (
    <div>
      <Logout />
      <div>
        <h2 className="text-center mt-5">Darbo aplinka</h2>
      </div>
    </div>
  );
}
