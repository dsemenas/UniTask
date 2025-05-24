import React, { useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import CommentSection from "../components/CommentSection";
import Logout from "../components/Logout";

export default function WorkSpacePage() {
  const { logout, user } = useContext(AuthContext);
  return (
    <div>
      <Logout />
      <div className="mt-5 px-5">
        <h2 className="text-center">Darbo aplinka</h2>
        <div className="mt-5">
          <h4>Esamos grupės</h4>
          <ul>
            <li>Grupė_nr.4</li>
            <li>Grupė_nr.4</li>
            <li>Grupė_nr.4</li>
            <li>Grupė_nr.4</li>
          </ul>
        </div>
      </div>
    </div>
  );
}
