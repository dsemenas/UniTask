import React from "react";

export default function Comment({ username }) {
  return (
    <div className="card py-1 px-2 border-primary border-2 w-100">
      <div className="d-flex card-title align-items-center">
        <img
          src="https://media.istockphoto.com/id/1220827245/vector/anonymous-gender-neutral-face-avatar-incognito-head-silhouette.jpg?s=612x612&w=0&k=20&c=GMdiPt_h8exnrAQnNo7dIKjwZyYqjH4lRQqV8AOx4QU="
          class="rounded-circle border me-1"
          width="30"
          height="30"
          alt="..."
        />
        <span className="fs-6 fw-bold">{username}</span>
      </div>
      <hr className="mb-2 mt-0" />
      <p class="card-text fw-light fs-6">
        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod
        tempor incididunt ut labore et dolore magna aliqua. Tempor incididunt ut
        labore et dolore magna aliqua.
      </p>
    </div>
  );
}
