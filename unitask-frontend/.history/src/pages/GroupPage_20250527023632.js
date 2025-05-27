import React from "react";
import { useParams } from "react-router-dom";
import { useEffect, useState, useContext } from "react";
import Logout from "../components/Logout";
import { AuthContext } from "../context/AuthContext";
import MemberAddForm from "../components/MemberAddForm";
import MembersList from "../components/MembersList";

export default function GroupPage() {
  const { logout, user, token } = useContext(AuthContext);
  const { groupID } = useParams();

  return (
    <div className="container-fluid">
      {user && <Logout />}
      <div className="row">
        <div className="col-3 border-end border-primary border-2 vh-100">
          <div className="border-bottom border-primary w-100">
            <div className="px-3 py-2 mt-4 mb-4">
              <MembersList token={token} groupId={groupID} />
            </div>
          </div>
          <div className="border-bottom border-primary w-100">
            <div className="px-3 py-2 mt-4 mb-4">
              <MemberAddForm token={token} groupId={groupID} />
            </div>
          </div>
          <div className="w-100">
            <div className="px-3 py-2 mt-4 mb-4">
              <h4>Užduoties sukūrimas</h4>
              <form className="mt-3">
                <div class="mb-3">
                  <div class="form-floating">
                    <textarea
                      class="form-control"
                      placeholder="Užduoties aprašymas"
                      id="floatingTextarea2"
                      style={{ height: "100px" }}
                    ></textarea>
                    <label for="floatingTextarea2">Užduoties aprašymas</label>
                  </div>
                </div>
                <button type="submit" class="btn btn-success">
                  Sukurti
                </button>
              </form>
            </div>
          </div>
        </div>
        <div className="col-9">Hello col-8</div>
      </div>
    </div>
  );
}
