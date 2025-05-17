import React from "react";

export default function HelloMessage() {
  const [message, setMessage] = useState("");

  useEffect(() => {
    fetch("http://localhost:5159/hello") // adjust port if needed
      .then((response) => response.text())
      .then((data) => setMessage(data))
      .catch((error) => {
        console.error("Error fetching message:", error);
        setMessage("Failed to load message");
      });
  }, []);

  return (
    <div style={{ fontFamily: "Arial", fontSize: "24px", padding: "20px" }}>
      {message ? message : "Loading..."}
    </div>
  );
}
