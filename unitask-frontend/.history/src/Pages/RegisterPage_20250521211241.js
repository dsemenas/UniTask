import React, { useState } from "react";
import { Link } from "react-router-dom";

export default function RegisterPage() {
  const [nickname, setNickname] = useState("");
  const [password, setPassword] = useState("");

  return (
    <div className="d-flex justify-content-center align-items-center vh-100">
      <div className="border border-2 border-primary rounded-4 p-2 w-25">
        <p className="text-center fs-3 mt-2">Registracija</p>
        <form className="text-start p-4 mt-4">
          <div class="mb-3">
            <label for="nicknameInput" class="form-label">
              Prisijungimo vardas
            </label>
            <input
              type="email"
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
              value={nickname}
              onChange={(e) => setNickname(e.target.value)}
              id="passwordInput"
            />
          </div>
          <button type="submit" class="btn btn-primary">
            Prisijungti
          </button>
          <span className="d-block fs-6 fw-light mt-1 ">
            Turi paskyra? Prisijungti prie jos gali spusteldamas{" "}
            <Link to="/login">čia</Link>!
          </span>
        </form>
      </div>
    </div>
  );
}
