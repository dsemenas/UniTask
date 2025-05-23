import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useContext } from "react";
import { AuthContext } from "../context/AuthContext";

export default function LoginPage() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [errors, setErrors] = useState([]);
  const [isLoading, setIsLoading] = useState(false);

  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    setErrors([]);

    if (username.length == 0 || password.length == 0) {
      setErrors(["Įveskite vartotojo vardą ir slaptažodį."]);
      return;
    }

    setIsLoading(true);

    try {
      const response = await fetch("http://localhost:5159/api/Auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password }),
      });

      const data = await response.json();

      if (data.success) {
        // Sėkmingas login
        localStorage.setItem("token", data.data);
        navigate("/workspace");
      } else {
        setErrors([data.errors] || ["Įvyko nežinoma klaida"]);
      }
    } catch (error) {
      setErrors(["Tinklo klaida, bandykite dar kartą"]);
    } finally {
      setIsLoading(false);
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
              value={username}
              onChange={(e) => setUsername(e.target.value)}
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
            {isLoading ? "Kraunasi..." : "Prisijungti"}
          </button>
          {!errors.length == 0 ? (
            <p className="fw-bold text-danger" style={{ fontSize: "14px" }}>
              {errors.map((err, i) => (
                <span key={i}>{err}</span>
              ))}
            </p>
          ) : null}
          <span className="d-block fs-6 fw-light mt-1 ">
            Neturi dar paskyros? Gali ją susikurti spusteldamas{" "}
            <Link to="/register">čia</Link>!
          </span>
        </form>
      </div>
    </div>
  );
}
