import React, { useEffect, useMemo, useState } from "react";
import axios from "axios";
import { Button, Grid, TextField } from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";
import { Cookies } from "react-cookie";

const API_URL = "https://localhost:7123/api/User";
const emptyUserForm = {
  id: "",
  userName: "",
  email: "",
  phoneNumber: "",
  password: "",
  roles: [],
};

const Users = () => {
  const [users, setUsers] = useState([]);
  const [roles, setRoles] = useState([]);
  const [userForm, setUserForm] = useState(emptyUserForm);
  const [roleForm, setRoleForm] = useState({ id: "", name: "" });
  const [activeTab, setActiveTab] = useState("users");
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  const authHeader = useMemo(() => {
    const cookies = new Cookies();
    const token = cookies.get("token");
    return {
      headers: {
        Authorization: `Bearer ${token || ""}`,
      },
    };
  }, []);

  const showMessage = (message, isError = false) => {
    setError(isError ? message : "");
    setSuccess(isError ? "" : message);
  };

  const formatError = (err) => {
    const data = err?.response?.data;
    if (Array.isArray(data)) {
      return data.join(", ");
    }
    if (typeof data === "string") {
      return data;
    }
    return "Co loi xay ra, vui long thu lai.";
  };

  const loadData = async () => {
    try {
      const [usersResponse, rolesResponse] = await Promise.all([
        axios.get(API_URL, authHeader),
        axios.get(`${API_URL}/roles`, authHeader),
      ]);

      setUsers(usersResponse.data);
      setRoles(rolesResponse.data);
    } catch (err) {
      showMessage(formatError(err), true);
    }
  };

  useEffect(() => {
    loadData();
  }, []);

  const resetUserForm = () => {
    setUserForm(emptyUserForm);
  };

  const resetRoleForm = () => {
    setRoleForm({ id: "", name: "" });
  };

  const editUser = (user) => {
    setUserForm({
      id: user.id,
      userName: user.userName || "",
      email: user.email || "",
      phoneNumber: user.phoneNumber || "",
      password: "",
      roles: user.roles || [],
    });
  };

  const deleteUser = async (id) => {
    if (!window.confirm("Xoa user nay?")) {
      return;
    }

    try {
      await axios.delete(`${API_URL}/${id}`, authHeader);
      showMessage("Da xoa user.");
      resetUserForm();
      loadData();
    } catch (err) {
      showMessage(formatError(err), true);
    }
  };

  const saveUser = async (event) => {
    event.preventDefault();

    try {
      const payload = {
        userName: userForm.userName,
        email: userForm.email,
        phoneNumber: userForm.phoneNumber,
        password: userForm.password,
        roles: userForm.roles,
      };

      if (userForm.id) {
        await axios.put(`${API_URL}/${userForm.id}`, payload, authHeader);
        showMessage("Da cap nhat user.");
      } else {
        await axios.post(API_URL, payload, authHeader);
        showMessage("Da tao user.");
      }

      resetUserForm();
      loadData();
    } catch (err) {
      showMessage(formatError(err), true);
    }
  };

  const editRole = (role) => {
    setRoleForm({ id: role.id, name: role.name || "" });
  };

  const deleteRole = async (id) => {
    if (!window.confirm("Xoa role nay?")) {
      return;
    }

    try {
      await axios.delete(`${API_URL}/roles/${id}`, authHeader);
      showMessage("Da xoa role.");
      resetRoleForm();
      loadData();
    } catch (err) {
      showMessage(formatError(err), true);
    }
  };

  const saveRole = async (event) => {
    event.preventDefault();

    try {
      const payload = { name: roleForm.name };
      if (roleForm.id) {
        await axios.put(`${API_URL}/roles/${roleForm.id}`, payload, authHeader);
        showMessage("Da cap nhat role.");
      } else {
        await axios.post(`${API_URL}/roles`, payload, authHeader);
        showMessage("Da tao role.");
      }

      resetRoleForm();
      loadData();
    } catch (err) {
      showMessage(formatError(err), true);
    }
  };

  const toggleUserRole = (roleName) => {
    setUserForm((current) => {
      const hasRole = current.roles.includes(roleName);
      return {
        ...current,
        roles: hasRole
          ? current.roles.filter((role) => role !== roleName)
          : [...current.roles, roleName],
      };
    });
  };

  const userColumns = [
    { field: "userName", flex: 1, headerName: "Username" },
    { field: "email", flex: 1, headerName: "Email" },
    { field: "phoneNumber", flex: 1, headerName: "Phone" },
    {
      field: "roles",
      flex: 1,
      headerName: "Roles",
      valueGetter: ({ row }) => (row.roles || []).join(", "),
    },
    {
      field: "action",
      headerName: "Action",
      flex: 1,
      renderCell: ({ row }) => (
        <Grid container spacing={1}>
          <Grid item>
            <Button variant="outlined" size="small" onClick={() => editUser(row)}>
              Sua
            </Button>
          </Grid>
          <Grid item>
            <Button color="error" variant="outlined" size="small" onClick={() => deleteUser(row.id)}>
              Xoa
            </Button>
          </Grid>
        </Grid>
      ),
    },
  ];

  const roleColumns = [
    { field: "name", flex: 1, headerName: "Role name" },
    {
      field: "action",
      headerName: "Action",
      flex: 1,
      renderCell: ({ row }) => (
        <Grid container spacing={1}>
          <Grid item>
            <Button variant="outlined" size="small" onClick={() => editRole(row)}>
              Sua
            </Button>
          </Grid>
          <Grid item>
            <Button color="error" variant="outlined" size="small" onClick={() => deleteRole(row.id)}>
              Xoa
            </Button>
          </Grid>
        </Grid>
      ),
    },
  ];

  return (
    <div style={{ width: "85vw", minHeight: "85vh" }}>
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h3>User Management</h3>
        <div>
          <Button variant={activeTab === "users" ? "contained" : "outlined"} onClick={() => setActiveTab("users")}>
            Users
          </Button>
          <Button
            sx={{ ml: 1 }}
            variant={activeTab === "roles" ? "contained" : "outlined"}
            onClick={() => setActiveTab("roles")}
          >
            Roles
          </Button>
        </div>
      </div>

      {error && <div className="alert alert-danger">{error}</div>}
      {success && <div className="alert alert-success">{success}</div>}

      {activeTab === "users" ? (
        <div className="row">
          <div className="col-lg-4 mb-3">
            <form className="border rounded p-3 bg-light" onSubmit={saveUser}>
              <h5>{userForm.id ? "Sua user" : "Them user"}</h5>
              <TextField
                label="Username"
                fullWidth
                margin="dense"
                value={userForm.userName}
                onChange={(event) => setUserForm({ ...userForm, userName: event.target.value })}
              />
              <TextField
                label="Email"
                type="email"
                required
                fullWidth
                margin="dense"
                value={userForm.email}
                onChange={(event) => setUserForm({ ...userForm, email: event.target.value })}
              />
              <TextField
                label="Phone"
                fullWidth
                margin="dense"
                value={userForm.phoneNumber}
                onChange={(event) => setUserForm({ ...userForm, phoneNumber: event.target.value })}
              />
              <TextField
                label={userForm.id ? "Password moi (bo trong neu khong doi)" : "Password"}
                type="password"
                required={!userForm.id}
                fullWidth
                margin="dense"
                value={userForm.password}
                onChange={(event) => setUserForm({ ...userForm, password: event.target.value })}
              />

              <div className="mt-2 mb-2">
                <label className="form-label">Roles</label>
                {roles.map((role) => (
                  <div className="form-check" key={role.id}>
                    <input
                      className="form-check-input"
                      type="checkbox"
                      checked={userForm.roles.includes(role.name)}
                      onChange={() => toggleUserRole(role.name)}
                      id={`role-${role.id}`}
                    />
                    <label className="form-check-label" htmlFor={`role-${role.id}`}>
                      {role.name}
                    </label>
                  </div>
                ))}
              </div>

              <Button type="submit" variant="contained">
                {userForm.id ? "Luu user" : "Them user"}
              </Button>
              <Button sx={{ ml: 1 }} variant="outlined" onClick={resetUserForm}>
                Reset
              </Button>
            </form>
          </div>

          <div className="col-lg-8" style={{ height: "72vh" }}>
            <DataGrid rows={users} columns={userColumns} pageSize={9} rowsPerPageOptions={[5, 9, 20]} getRowId={(row) => row.id} />
          </div>
        </div>
      ) : (
        <div className="row">
          <div className="col-lg-4 mb-3">
            <form className="border rounded p-3 bg-light" onSubmit={saveRole}>
              <h5>{roleForm.id ? "Sua role" : "Them role"}</h5>
              <TextField
                label="Role name"
                required
                fullWidth
                margin="dense"
                value={roleForm.name}
                onChange={(event) => setRoleForm({ ...roleForm, name: event.target.value })}
              />
              <Button type="submit" variant="contained">
                {roleForm.id ? "Luu role" : "Them role"}
              </Button>
              <Button sx={{ ml: 1 }} variant="outlined" onClick={resetRoleForm}>
                Reset
              </Button>
            </form>
          </div>

          <div className="col-lg-8" style={{ height: "72vh" }}>
            <DataGrid rows={roles} columns={roleColumns} pageSize={9} rowsPerPageOptions={[5, 9, 20]} getRowId={(row) => row.id} />
          </div>
        </div>
      )}
    </div>
  );
};

export default Users;
