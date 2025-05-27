import React from "react";
import { useParams, useLocation } from "react-router-dom";
import { useEffect, useState, useContext } from "react";
import Logout from "../components/Logout";
import { AuthContext } from "../context/AuthContext";
import MemberAddForm from "../components/MemberAddForm";
import MembersList from "../components/MembersList";
import Task from "../components/Task";

export default function GroupPage() {
  const { logout, user, token } = useContext(AuthContext);
  const { groupID } = useParams();
  const location = useLocation();
  const queryParams = new URLSearchParams(location.search);
  const groupName = queryParams.get("name");
  const [shouldFetchMembers, setShouldFetchMembers] = useState(false);

  const [task, setTask] = useState({
    description: "",
    groupId: groupID,
    username: "",
    status: "",
  });

  const [allTasks, setAllTasks] = useState(null);

  const handleMemberFormSubmit = () => {
    setShouldFetchMembers(true);
  };

  useEffect(() => {
    getAllTasks();
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setTask((prev) => ({
      ...prev,
      [name]: name === "status" ? Number(value) : value, // convert status to number
    }));

    console.log(task);
  };

  const validateTask = (task) => {
    if (!task.description.trim()) {
      alert("Description is required.");
      return false;
    }

    if (!task.groupId) {
      alert("Group ID is missing.");
      return false;
    }

    if (!task.username.trim()) {
      alert("Username is required.");
      return false;
    }

    if (![0, 1, 2].includes(task.status)) {
      alert("Invalid status selected.");
      return false;
    }

    return true;
  };

  const submitTask = async (e) => {
    e.preventDefault();

    const isValid = validateTask(task);
    if (!isValid) {
      console.error("Task is not valid.");
      return;
    }

    try {
      const response = await fetch(
        "http://localhost:5159/api/Task/create-task",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
          body: JSON.stringify(task),
        }
      );

      if (!response.ok) {
        throw new Error(`Server error: ${response.status}`);
      }

      const data = await response.json();
      console.log("Task created:", data);
      getAllTasks();
    } catch (error) {
      console.error("Error submitting task:", error);
    }
  };

  const getAllTasks = async () => {
    try {
      const response = await fetch(
        `http://localhost:5159/api/Task/all-tasks/${groupID}`,
        {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
        }
      );

      if (!response.ok) {
        throw new Error(`Failed to fetch tasks. Status: ${response.status}`);
      }

      const data = await response.json();
      console.log("Fetched tasks:", data);
      setAllTasks(data.data);
    } catch (error) {
      console.error("Error fetching tasks:", error);
      return [];
    }
  };

  const deleteTask = async (taskId) => {
    try {
      const response = await fetch(
        `http://localhost:5159/api/Task?taskId=${taskId}`,
        {
          method: "DELETE",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
        }
      );

      if (!response.ok) {
        throw new Error(`Delete failed with status: ${response.status}`);
      }

      console.log(`Task ${taskId} deleted`);
      getAllTasks();
    } catch (error) {
      console.error("Error deleting task:", error);
    }
  };

  async function updateTaskStatus(taskId, newStatus) {
    const body = {
      TaskId: taskId,
      NewStatus: newStatus,
    };

    try {
      const response = await fetch("http://localhost:5159/api/Task", {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(body),
      });

      if (!response.ok) {
        throw new Error("Failed to update task status");
      }

      const result = await response.json();
      console.log("Status updated successfully:", result);
      getAllTasks();
    } catch (error) {
      console.error("Error updating status:", error);
      throw error;
    }
  }

  return (
    <div className="container-fluid">
      {user && <Logout />}
      <div className="row">
        <div className="col-3 border-end border-primary border-2 vh-100">
          <div className="border-bottom border-primary w-100">
            <div className="px-3 py-2 mt-4 mb-4">
              <MembersList
                token={token}
                groupId={groupID}
                shouldFetchMembers={shouldFetchMembers}
                setShouldFetchMembers={setShouldFetchMembers}
              />
            </div>
          </div>
          <div className="border-bottom border-primary w-100">
            <div className="px-3 py-2 mt-4 mb-4">
              <MemberAddForm
                token={token}
                groupId={groupID}
                handleMemberFormSubmit={handleMemberFormSubmit}
              />
            </div>
          </div>
          <div className="w-100">
            <div className="px-3 py-2 mt-4 mb-4">
              <h4>Užduoties sukūrimas</h4>
              <form className="mt-3" onSubmit={submitTask}>
                <div class="mb-3">
                  <div class="form-floating">
                    <textarea
                      class="form-control"
                      placeholder="Užduoties aprašymas"
                      id="floatingTextarea2"
                      name="description"
                      style={{ height: "100px" }}
                      value={task.description}
                      onChange={handleChange}
                    ></textarea>
                    <label for="floatingTextarea2">Užduoties aprašymas</label>
                  </div>
                </div>
                <div className="mb-3">
                  <label for="assignedTo" className="form-label">
                    Kam priskirta
                  </label>
                  <input
                    type="text"
                    className="form-control"
                    id="assignedTo"
                    placeholder="Įrašykite vartotojo vardą..."
                    name="username"
                    value={task.username}
                    onChange={handleChange}
                  />
                </div>
                <div className="mb-3">
                  <p className="mb-2">Statusas</p>
                  <select
                    class="form-select"
                    aria-label="Default select example"
                    name="status"
                    value={task.status}
                    onChange={handleChange}
                  >
                    <option value="Pasirinkite statusą" selected>
                      Pasirinkite statusą
                    </option>
                    <option value={0}>Padaryti</option>
                    <option value={1}>Vykdoma</option>
                    <option value={2}>Atlikta</option>
                  </select>
                </div>
                <button type="submit" className="btn btn-success">
                  Sukurti
                </button>
              </form>
            </div>
          </div>
        </div>
        <div className="col-9">
          <h2 className="text-center mt-4">Grupė: {groupName}</h2>
          <div className="container-fluid mt-5">
            <div className="row">
              <div className="col-4 d-flex flex-column align-items-center">
                <h4 className="text-center text-danger">Padaryti</h4>
                {allTasks &&
                  allTasks
                    .filter((task) => task.status === 0)
                    .map((task) => (
                      <Task
                        key={task.id}
                        taskId={task.id}
                        description={task.description}
                        assignedTo={task.assignedTo}
                        status={task.status}
                        deleteTask={deleteTask}
                        updateTaskStatus={updateTaskStatus}
                      />
                    ))}
              </div>
              <div className="col-4 d-flex flex-column align-items-center">
                <h4 className="text-center text-warning">Vykdoma</h4>
                {allTasks &&
                  allTasks
                    .filter((task) => task.status === 1)
                    .map((task) => (
                      <Task
                        key={task.id}
                        taskId={task.id}
                        description={task.description}
                        assignedTo={task.assignedTo}
                        status={task.status}
                        deleteTask={deleteTask}
                        updateTaskStatus={updateTaskStatus}
                      />
                    ))}
              </div>
              <div className="col-4 d-flex flex-column align-items-center">
                <h4 className="text-center text-success">Atlikta</h4>
                {allTasks &&
                  allTasks
                    .filter((task) => task.status === 2)
                    .map((task) => (
                      <Task
                        key={task.id}
                        taskId={task.id}
                        description={task.description}
                        assignedTo={task.assignedTo}
                        status={task.status}
                        deleteTask={deleteTask}
                        updateTaskStatus={updateTaskStatus}
                      />
                    ))}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
