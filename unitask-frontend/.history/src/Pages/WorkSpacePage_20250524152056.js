import React, { useContext, useState } from "react";
import { AuthContext } from "../context/AuthContext";
import CommentSection from "../components/CommentSection";
import Logout from "../components/Logout";

export default function WorkSpacePage() {
  const { logout, user } = useContext(AuthContext);

  const [groupName, setGroupName] = useState("");

  return (
    <div>
      <Logout />
      <div className="mt-5 px-5">
        <h1 className="text-center">Darbo aplinka</h1>
        <div className="mt-5">
          <h3>Esamos grupės</h3>
          <ul className="list-group list-group-flush mt-3 fs-5 w-25">
            <li className="list-group-item">Grupė_nr.4</li>
            <li className="list-group-item">Grupė_nr.4</li>{" "}
            <li className="list-group-item">Grupė_nr.4</li>{" "}
            <li className="list-group-item">Grupė_nr.4</li>
          </ul>
        </div>
        <div className="mt-5">
          <h3>Grupės pridėjimas</h3>
          <form className="mt-3 w-25">
            <div class="mb-3">
              <input
                type="text"
                class="form-control"
                id="groupName"
                value={groupName}
                onChange={e => set}
                placeholder="Įrašykite grupės pavadinimą..."
              />
            </div>
            <button type="submit" class="btn btn-success">
              Pridėti
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}
