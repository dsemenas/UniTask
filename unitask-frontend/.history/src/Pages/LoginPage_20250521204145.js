import React, { useState } from "react";

export default function LoginPage() {
  const [nickname, setNickname] = useState("");
  const [password, setPassword] = useState("");

  return (
    <div className="d-flex justify-content-center align-items-center vh-100">
      <div className="border border-2 border-primary rounded-4 p-2 w-25">
        <p className="text-center fs-3 mt-2">Prisijungimas</p>
        <form className="text-start p-4 mt-4">
          <div class="mb-3">
            <label for="emailInput" class="form-label">
              Prisijungimo vardas
            </label>
            <input
              type="email"
              class="form-control"
              placeholder="Įrašykite savo prisijungimo vardą..."
              id="emailInput"
            />
          </div>
          <div class="mb-3">
            <label for="exampleInputPassword1" class="form-label">
              Password
            </label>
            <input
              type="password"
              class="form-control"
              placeholder="Įrašykite savo slaptažodį..."
              id="exampleInputPassword1"
            />
          </div>
          <button type="submit" class="btn btn-primary">
            Prisijungti
          </button>
          <span className="d-block fs-1 mt-1">Neturi paskyros?</span>
        </form>
      </div>
    </div>
  );
}
