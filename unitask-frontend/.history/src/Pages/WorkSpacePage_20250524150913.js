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
        <h1 className="text-center">Darbo aplinka</h1>
        <div className="mt-5">
          <h3>Esamos grupės</h3>
          <ul className=mt-2>
            <li>Grupė_nr.4</li>
            <li>Grupė_nr.6</li>
            <li>Grupė_nr.11</li>
            <li>Grupė_nr.12</li>
          </ul>
        </div>
        <div className="mt-5">
          <h3>Grupės pridėjimas</h3>
        </div>
      </div>
    </div>
  );
}
