const apiUrl = "https://localhost:44301/api/todo";
let currentIdToDelete = null;

// Load todos
function fetchTodos() {
  const status = document.getElementById("statusFilter").value;
  let url = `${apiUrl}/getstatus`;
  if (status) {
    url += `?status=${status}`;
  }

  fetch(url)
    .then((res) => res.json())
    .then((todos) => {
      const list = document.getElementById("todoList");
      list.innerHTML = "";
      todos.forEach((todo) => {
        const li = document.createElement("li");
        li.className =
          "list-group-item d-flex justify-content-between align-items-center";
        li.innerHTML = `
      <div>
        <h5>${todo.title} <span class="badge bg-info">${
          todo.priority
        }</span></h5>
        <p class="mb-1">${todo.description || ""}</p>
        <small>Status: ${todo.status} | Due: ${
          todo.dueDate?.split("T")[0] || "N/A"
        }</small>
      </div>
      <div>
        <button class="btn btn-sm btn-warning me-2" onclick="openEditModal('${
          todo.id
        }')">Edit</button>
        <button class="btn btn-sm btn-danger" onclick="openDeleteModal('${
          todo.id
        }')">Delete</button>
      </div>
    `;
        list.appendChild(li);
      });
    });
}

// Filter event
document.getElementById("statusFilter").addEventListener("change", fetchTodos);

// Bootstrap modals
const todoModal = new bootstrap.Modal(document.getElementById("todoModal"));
const deleteModal = new bootstrap.Modal(document.getElementById("deleteModal"));

// Open Create Modal
function openCreateModal() {
  document.getElementById("todoForm").reset();
  document.getElementById("todoId").value = "";
  todoModal.show();
}

// Open Edit Modal
function openEditModal(id) {
  fetch(`${apiUrl}/GetById/${id}`)
    .then((res) => res.json())
    .then((todo) => {
      document.getElementById("todoId").value = todo.id;
      document.getElementById("todoTitle").value = todo.title;
      document.getElementById("todoDescription").value = todo.description || "";
      document.getElementById("todoStatus").value = todo.status;
      document.getElementById("todoPriority").value = todo.priority;
      document.getElementById("todoDueDate").value =
        todo.dueDate?.split("T")[0] || "";
      todoModal.show();
    });
}

// Save (Create or Update)
document.getElementById("todoForm").addEventListener("submit", function (e) {
  e.preventDefault();

  const id = document.getElementById("todoId").value;
  const todo = {
    id: id || undefined,
    title: document.getElementById("todoTitle").value,
    description: document.getElementById("todoDescription").value,
    status: document.getElementById("todoStatus").value,
    priority: document.getElementById("todoPriority").value,
    dueDate: document.getElementById("todoDueDate").value || null,
  };

  const method = id ? "PUT" : "POST";
  const url = id ? `${apiUrl}/update/${id}` : `${apiUrl}/createtodo`;

  fetch(url, {
    method,
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(todo),
  }).then(() => {
    todoModal.hide();
    fetchTodos();
  });
});

// Open Delete Modal
function openDeleteModal(id) {
  currentIdToDelete = id;
  deleteModal.show();
}

// Confirm Delete
document.getElementById("confirmDeleteBtn").addEventListener("click", () => {
  fetch(`${apiUrl}/delete/${currentIdToDelete}`, {
    method: "DELETE",
  }).then(() => {
    deleteModal.hide();
    fetchTodos();
  });
});

// Initial fetch
fetchTodos();
