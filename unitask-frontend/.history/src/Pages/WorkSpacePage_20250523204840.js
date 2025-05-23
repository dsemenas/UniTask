import React from "react";
import CommentSection from "../components/CommentSection";

export default function WorkSpacePage() {
  const { logout } = useContext(AuthContext);
  return (
    <div className="p-3">
      <CommentSection />
    </div>
  );
}
