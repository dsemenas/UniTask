import React from "react";
import { useState, useEffect } from "react";

export default function HelloMessage() {
  const [message, setMessage] = useState("");

  useEffect(() => {
    fetch("http://localhost:5159/Hello") // adjust port if needed
      .then((response) => response.text())
      .then((data) => setMessage(data))
      .catch((error) => {
        console.error("Error fetching message:", error);
        setMessage("Failed to load message");
      });
  }, []);

  return (
    <div style={{ fontFamily: "Arial", fontSize: "24px", padding: "20px" }}>
      <button class="btn btn-danger">Hello dsafim bootstrap button</button>
    </div>
  );
}
