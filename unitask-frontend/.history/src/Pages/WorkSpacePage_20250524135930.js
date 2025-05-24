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
        <div>
          <h4>Esamos grupės</h4>
          <div className="mt-3">
            <ul>
              
            </ul>
          </div>
        </div>
        <div className="mt-5">
          <h4>Grupės sukūrimas</h4>
        </div>
      </div>
    </div>
  );
}
