import React from "react";

export default function NotFound() {
  return (
    <div className="d-flex justify-content-center align-items-center vh-100 bg-light">
      <div className="text-center">
        <h1 className="display-1 text-danger">404</h1>
        <p className="text-cente fs-3">
          Puslapis nerastas. Atrodo, kad ieškote kažko, ko čia nėra.
        </p>
        <a href="/workspace" className="btn btn-danger">
          Grįžti į pradžią
        </a>
      </div>
    </div>
  );
}
