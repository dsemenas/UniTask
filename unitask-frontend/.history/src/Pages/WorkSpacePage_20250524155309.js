import React, { useContext, useState } from "react";
import { AuthContext } from "../context/AuthContext";
import Logout from "../components/Logout";

export default function WorkSpacePage() {
  const { logout, user, token } = useContext(AuthContext);

  const [groupName, setGroupName] = useState("");
  const [isLoading, setIsLoading] = useState("");
  const [errors, setErrors] = useState([]);
  const [successMessage, setSuccessMessage] = useState("");

  const handleAddGroup = async (e) => {
    e.preventDefault();

    setErrors([]); // Clear previous errors

    if (groupName.trim().length < 4) {
      setErrors([
        "Nepavyko sukurti gupės... Grupės pavadinimą turi sudaryti bent 4 simboliai.",
      ]);
      return;
    }

    setIsLoading(true);

    try {
      const response = await fetch("https://your-api-url.com/groups", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify({ groupName }),
      });

      const data = await response.json();

      if (data.success) {
        // Group created successfully
        setSuccessMessage("Grupė sėkmingai sukurta!");
        // Optionally do something with data.data here
      } else {
        // Show error messages array or fallback error
        setErrors(data.errors || ["Įvyko nežinoma klaida"]);
      }
    } catch (error) {
      setErrors(["Tinklo klaida, bandykite dar kartą..."]);
    } finally {
      setIsLoading(false);
    }
  };

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
          <form className="mt-3 w-25" onSubmit={(e) => handleAddGroup(e)}>
            <div class="mb-3">
              <input
                type="text"
                class="form-control"
                id="groupName"
                value={groupName}
                onChange={(e) => setGroupName(e.target.value)}
                placeholder="Įrašykite grupės pavadinimą..."
              />
            </div>
            <button type="submit" class="btn btn-success">
              {isLoading ? "Kraunasi..."}
            </button>
            {errors.length == 0 ? null : (
              <p className="fw-bold text-danger" style={{ fontSize: "14px" }}>
                {errors.map((err, i) => (
                  <span key={i}>{err}</span>
                ))}
              </p>
            )}
          </form>
        </div>
      </div>
    </div>
  );
}
