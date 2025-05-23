import React, { useState } from "react";
import { Link } from "react-router-dom";

export default function LoginPage() {
  const [nickname, setNickname] = useState("");
  const [password, setPassword] = useState("");
  const [error, setErrors] = useState(null);

  const handleSubmit = (e) => {
    e.preventDefault();

    if(nickname.length = 0 || password.length = 0) {
      
    }
  };

  return (
    <div className="d-flex justify-content-center align-items-center vh-100">
      <div className="border border-2 border-primary rounded-4 p-2 w-25">
        <p className="text-center fs-3 mt-2">Prisijungimas</p>
        <form className="text-start p-4 mt-4" onSubmit={handleSubmit}>
          <div class="mb-3">
            <label for="nicknameInput" class="form-label">
              Prisijungimo vardas
            </label>
            <input
              type="nickname"
              class="form-control"
              placeholder="Įrašykite savo prisijungimo vardą..."
              value={nickname}
              onChange={(e) => setNickname(e.target.value)}
              id="nicknameInput"
            />
          </div>
          <div class="mb-3">
            <label for="passwordInput" class="form-label">
              Slaptažodis
            </label>
            <input
              type="password"
              class="form-control"
              placeholder="Įrašykite savo slaptažodį..."
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              id="passwordInput"
            />
          </div>
          <button type="submit" class="btn btn-primary">
            Prisijungti
          </button>
          <span className="d-block fs-6 fw-light mt-1 ">
            Neturi dar paskyros? Gali ją susikurti spusteldamas{" "}
            <Link to="/register">čia</Link>!
          </span>
        </form>
      </div>
    </div>
  );
}
