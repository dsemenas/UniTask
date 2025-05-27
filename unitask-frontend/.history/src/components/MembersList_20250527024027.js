import React, { useState, useEffect } from "react";

export default function MembersList(token, groupID) {
  const [data, setData] = useState(null);

  useEffect(() => {
    fetch("http://localhost:5159/api/Group/add-members", {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
      .then((res) => res.json())
      .then((data) => setData(data));
  }, [token]);

  if (!data) return <div>Loading...</div>;
  return (
    <div>
      <h4>Esami Nariai</h4>
      <ul className="list-group list-group-flush">
        <li className="list-group-item">Narys_1</li>
        <li className="list-group-item">Narys_1</li>
        <li className="list-group-item">Narys_1</li>
        <li className="list-group-item">Narys_1</li>
        <li className="list-group-item">Narys_1</li>
        <li className="list-group-item">Narys_1</li>
      </ul>
    </div>
  );
}
