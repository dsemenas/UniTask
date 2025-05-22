import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";

export default function RegisterPage() {
  const [nickname, setNickname] = useState("");
  const [nicknameValidationError, setNicknameValidationError] = useState(false);
  const [password, setPassword] = useState("");
  const [passwordValidationError, setPasswordValidationError] = useState(false);
  const [successMessage, setSuccessMessage] = useState(false);
  const [isLoading, setIsLoading] = useState(false);

  const navigate = useNavigate();

  const handleNicknameValidation = (e) => {
    setNickname(e.target.value);
    if (e.target.value.length < 4) {
      setNicknameValidationError(true);
    } else {
      setNicknameValidationError(false);
    }
  };

  const handlePasswordValidation = (e) => {
    setPassword(e.target.value);
    if (
      e.target.value.length < 8 ||
      !/[a-zA-Z]/.test(e.target.value) ||
      !/[0-9]/.test(e.target.value)
    ) {
      setPasswordValidationError(true);
    } else {
      setPasswordValidationError(false);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!nicknameValidationError && !passwordValidationError) {
      setIsLoading(true);
      const userData = {
        nickname,
        password,
      };

      try {
        const response = await fetch("https://httpbin.org/post", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(userData),
        });

        if (!response.ok) {
          const errorData = await response.json();
          console.error("Registration failed:", errorData.message);
          setIsLoading(false);
          return;
        }

        setIsLoading(false);
        const data = await response.json();
        setSuccessMessage(true);
        setTimeout(() => {
          navigate("/login");
        }, 3000);
      } catch (error) {
        setIsLoading(false);
        console.error("Network error:", error);
      }
    }
  };

  return (
    <div>
      {successMessage && (
        <div className="alert alert-success text-center fw-bold" role="alert">
          Registracija sėkminga. Netrukus būsite perkeltas į prisijungimo
          puslapį!
        </div>
      )}
      <div className="d-flex justify-content-center align-items-center vh-100">
        <div className="border border-2 border-primary rounded-4 p-2 w-25">
          <p className="text-center fs-3 mt-2">Registracija</p>
          <form className="text-start p-4 mt-4" onSubmit={handleSubmit}>
            <div class="mb-3">
              <label for="nicknameInput" class="form-label">
                Prisijungimo vardas
              </label>
              <input
                type="text"
                className="form-control"
                placeholder="Įrašykite savo prisijungimo vardą..."
                value={nickname}
                onChange={(e) => handleNicknameValidation(e)}
                id="nicknameInput"
              />
              {nicknameValidationError ? (
                <span
                  className="fw-bold text-danger"
                  style={{ fontSize: "14px" }}
                >
                  Prisijungimo vardą turi sudaryti mažiausiai 4 simboliai.
                </span>
              ) : null}
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
                onChange={(e) => handlePasswordValidation(e)}
                id="passwordInput"
              />
              {passwordValidationError ? (
                <span
                  className="fw-bold text-danger"
                  style={{ fontSize: "14px" }}
                >
                  Slaptažodyje turi būti bent 8 simboliai, įskaitant raides ir
                  skaičius.
                </span>
              ) : null}
            </div>
            <button type="submit" class="btn btn-primary">
              Registruotis
            </button>
            {isLoading ? (
              <div class="spinner-border" role="status">
                <span class="visually-hidden">Loading...</span>
              </div>
            ) : null}
            <span className="d-block fs-6 fw-light mt-1 ">
              Turi paskyrą? Prisijungti prie jos gali spusteldamas{" "}
              <Link to="/login">čia</Link>!
            </span>
          </form>
        </div>
      </div>
    </div>
  );
}
