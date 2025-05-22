import React from "react";

export default function Comment() {
  return (
    <div className="card p-1 border-primary">
      <div className="card-title">
        <img
          src="https://media.istockphoto.com/id/1220827245/vector/anonymous-gender-neutral-face-avatar-incognito-head-silhouette.jpg?s=612x612&w=0&k=20&c=GMdiPt_h8exnrAQnNo7dIKjwZyYqjH4lRQqV8AOx4QU="
          class="rounded-circle border me-1"
          width="30"
          height="30"
          alt="..."
        />
        <span className="fs-5">Jonce_55</span>
      </div>
      <p class="card-text fw-light">
        Some quick example text to build on the card title and make up the bulk
        of the card’s content.
      </p>
    </div>
  );
}
