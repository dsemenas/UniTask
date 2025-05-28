import React, { useContext, useEffect, useState } from "react";
import { AuthContext } from "../context/AuthContext";
import Logout from "../components/Logout";
import { Link } from "react-router-dom";

export default function WorkSpacePage() {
  const { logout, user, token } = useContext(AuthContext);

  const [groupName, setGroupName] = useState("");
  const [isLoading, setIsLoading] = useState("");
  const [errors, setErrors] = useState([]);
  const [successMessage, setSuccessMessage] = useState(null);
  const [data, setData] = useState([]);

  useEffect(() => {
    fetchData();
  }, []);

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
      const response = await fetch(
        "https://localhost:7084/api/Group/create-group",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
          body: JSON.stringify({ Name: groupName, OwnerName: user.username }),
        }
      );

      const data = await response.json();

      if (data.success) {
        // Group created successfully
        setSuccessMessage(
          "Grupė sėkmingai sukurta! Galite ją matyti tarp kitų grupių sąrašo."
        );
        setGroupName("");
        setTimeout(() => {
          setSuccessMessage(null);
        }, 3000);
        fetchData();
        // Optionally do something with data.data here
      } else {
        // Show error messages array or fallback error
        console.log(data.errors);
        setErrors([...data.errors] || ["Įvyko nežinoma klaida"]);
      }
    } catch (error) {
      setErrors(["Tinklo klaida, bandykite dar kartą..."]);
    } finally {
      setIsLoading(false);
    }
  };

  const fetchData = async () => {
    setIsLoading(true);
    try {
      const response = await fetch(
        `https://localhost:7084/api/Group/user/${user.id}`,
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
        }
      );
      if (!response.ok) throw new Error("Network response was not ok");

      const result = await response.json();
      setData(result.data);
    } catch (err) {
      console.log(err);
    } finally {
      setIsLoading(false);
    }
  };

  const deleteGroup = async (groupId) => {
    if (!window.confirm("Ar tikrai norite ištrinti šią grupę?")) return;

    try {
      const response = await fetch(
        `https://localhost:7084/api/Group?groupId=${groupId}`,
        {
          method: "DELETE",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
        }
      );

      if (response.ok) {
        setSuccessMessage("Grupė sėkmingai ištrinta.");
        setTimeout(() => setSuccessMessage(null), 3000);
        fetchData(); // atnaujina sąrašą
      } else {
        const errorData = await response.json();
        setErrors(errorData.errors || ["Nepavyko ištrinti grupės."]);
      }
    } catch (err) {
      console.error(err);
      setErrors(["Tinklo klaida, bandykite dar kartą..."]);
    }
  };

  return (
    <div>
      {successMessage && (
        <div className="alert alert-success text-center fw-bold" role="alert">
          {successMessage}
        </div>
      )}
      <Logout />
      <div className="mt-5 px-5">
        <h1 className="text-center">Darbo aplinka</h1>
        <div className="mt-5">
          <h3>Esamos grupės</h3>
          <ul className="list-group list-group-flush mt-3 fs-5 w-25">
            {data.length != 0 ? (
              data.map((d, i) => (
                <li className="list-group-item" key={i}>
                  <Link
                    to={`/group/${d.id}?name=${encodeURIComponent(d.name)}`}
                  >
                    {d.name}
                  </Link>
                  <button
                    class="btn btn-secondary"
                    onClick={() => deleteGroup(d.id)}
                  >
                    Ištrinti
                  </button>
                </li>
              ))
            ) : (
              <span>
                Šiuo metu nesate priskirtas jokiai grupei... Gal norėtumėte
                sukurti naują?
              </span>
            )}
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
              {isLoading ? "Kraunasi..." : "Pridėti"}
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
