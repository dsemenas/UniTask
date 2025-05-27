import React from "react";

export default function Task({
  description,
  assignedTo,
  status,
  taskId,
  deleteTask,
  updateTaskStatus,
}) {
  return (
    <div class="card mt-2" style={{ width: "18rem" }}>
      <div class="card-body">
        <h6 class="card-subtitle mb-2 text-body-secondary border-bottom">
          Priskirta: {assignedTo}
        </h6>
        <span className="fw-bold">Aprašymas:</span>
        <p class="card-text border-bottom py-2">{description}</p>
        <span className="fw-bold">Keisti statusą:</span>
        <select
          value={status}
          onChange={(e) => {
            updateTaskStatus(taskId, Number(e.target.value));
          }}
          className="form-select mb-3 mt-2"
        >
          <option value={0}>Padaryti</option>
          <option value={1}>Vykdoma</option>
          <option value={2}>Atlikta</option>
        </select>
        <p className="border-bottom"></p>
        <button
          type="button"
          class="btn btn-secondary"
          onClick={() => deleteTask(taskId)}
        >
          Ištrinti
        </button>
      </div>
    </div>
  );
}
