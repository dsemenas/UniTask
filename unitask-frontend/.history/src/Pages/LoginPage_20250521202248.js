import React, { useState } from "react";

export default function LoginPage() {
  const [nickname, setNickname] = useState("");
  const [password, setPassword] = useState("");

  return (
      <div></div>
    <div className="d-flex justify-content-center align-items-center vh-100">
      <form>
        <div class="mb-3">
          <label for="exampleInputEmail1" class="form-label">
            Prisijungimo vardas
          </label>
          <input
            type="email"
            class="form-control"
            id="exampleInputEmail1"
            aria-describedby="emailHelp"
          />
        </div>
        <div class="mb-3">
          <label for="exampleInputPassword1" class="form-label">
            Password
          </label>
          <input
            type="password"
            class="form-control"
            id="exampleInputPassword1"
          />
        </div>
        <button type="submit" class="btn btn-primary">
          Prisijungti
        </button>
      </form>
    </div>
  );
}
