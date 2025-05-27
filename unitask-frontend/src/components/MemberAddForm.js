import React, { useState } from "react";

export default function MemberAddForm({
  token,
  groupId,
  handleMemberFormSubmit,
}) {
  const [name, setName] = useState("");

  const handlePost = (e) => {
    e.preventDefault();
    handleMemberFormSubmit();
    fetch("http://localhost:5159/api/Group/add-members", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        Username: name,
        GroupId: groupId,
      }),
    })
      .then((res) => res.json())
      .then((data) => console.log(data))
      .catch((err) => console.error("Error:", err));
  };

  return (
    <div>
      <h4>Nario pridėjimas</h4>
      <form className="mt-3" onSubmit={handlePost}>
        <div class="mb-3">
          <input
            type="text"
            class="form-control"
            id="userName"
            value={name}
            onChange={(e) => setName(e.target.value)}
            placeholder="Įrašykite grupės pavadinimą..."
          />
        </div>
        <button type="submit" class="btn btn-success">
          Prideti
        </button>
      </form>
    </div>
  );
}
