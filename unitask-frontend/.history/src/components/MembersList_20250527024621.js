import React, { useState, useEffect } from "react";

export default function MembersList({ token, groupID }) {
  const [data, setData] = useState(null);

  useEffect(() => {
    fetch(`http://localhost:5159/api/Group/get-members/${groupID}`, {
      headers: { Authorization: `Bearer ${token}` },
    })
      .then((res) => {
        if (res.ok) return res.json();
        return null; // silently ignore errors
      })
      .then((json) => {
        if (json) setData(json);
      });
  }, [token, groupID]);

  if (!data) return <div>Loading...</div>;
  return (
    <div>
      <h4>Esami Nariai</h4>
      <ul className="list-group list-group-flush">
        {data != null ? (
          data.map((m, i) => {
            <li key={i}>{m.data.username}</li>;
          })
        ) : (
          <span></span>
        )}
      </ul>
    </div>
  );
}
