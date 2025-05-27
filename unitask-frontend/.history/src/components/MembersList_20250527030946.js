import React, { useState, useEffect } from "react";

export default function MembersList({ token, groupId }) {
  const [data, setData] = useState(null);

  use

  const fetchData = async () => {
    try {
      const response = await fetch(
        `http://localhost:5159/api/Group/get-members/${groupId}`,
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
    }
  };
  if (data == null || data.length == 0) return <div>Kraunasi...</div>;
  return (
    <div>
      <h4>Esami Nariai</h4>
      <ul className="list-group list-group-flush">
        {data != null || data.length != 0
          ? data.data.map((m, i) => {
              <li key={i}>{m.username}</li>;
            })
          : null}
      </ul>
    </div>
  );
}
