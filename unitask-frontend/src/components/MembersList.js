import React, { useState, useEffect } from "react";

export default function MembersList({
  token,
  groupId,
  setShouldFetchMembers,
  shouldFetchMembers,
}) {
  const [data, setData] = useState(null);

  useEffect(() => {
    if (shouldFetchMembers) {
      fetchData();
      setShouldFetchMembers(false);
    }
    fetchData();
  }, [shouldFetchMembers, setShouldFetchMembers]);

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
      console.log(result);
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
        {data != null && data.length != 0 ? (
          data.map((m, i) => (
            <li key={i} className="list-group-item">
              {m.username}
            </li>
          ))
        ) : (
          <span>Kazkas blogai</span>
        )}
      </ul>
    </div>
  );
}
