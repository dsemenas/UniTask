mport React, { useState, useEffect } from "react";

export default function MembersList({ token, groupID }) {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!token || !groupID) return;

    fetch(`http://localhost:5159/api/Group/get-members/${groupID}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
      .then((res) => {
        if (!res.ok) throw new Error("Network response was not ok");
        return res.json();
      })
      .then((jsonData) => {
        setData(jsonData || []);
        setLoading(false);
      })
      .catch(() => {
        setData([]);
        setLoading(false);
      });
  }, [token, groupID]);

  if (loading) return <div>Kraunasi...</div>;

  return (
    <div>
      <h4>Esami Nariai</h4>
      <ul className="list-group list-group-flush">
        {data.length > 0 ? (
          data.map((m, i) => <li key={i}>{m.username}</li>)
        ) : (
          <li>Narių nėra</li>
        )}
      </ul>
    </div>
  );
}