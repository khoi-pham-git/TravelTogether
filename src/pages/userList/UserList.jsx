import "./userList.css";
import { DataGrid } from "@material-ui/data-grid";
import { DeleteOutline } from "@material-ui/icons";
import { Link } from "react-router-dom";
import { useState,useEffect } from "react";
import axios from "axios";

axios.defaults.baseURL = 'https://jsonplaceholder.typicode.com';
export default function UserList() {
  // const [data, setData] = useState(userRows);
  const [customer, setCustomer] = useState([]); 
  
  useEffect(() => {
    axios.get("http://traveltogether.somee.com/api/v1.0/customers?ele=10&page=1")
      .then((res) => { 
        
        setCustomer(res.data.data)
        console.log(customer); })
      .catch((err) => { console.log(err); })
  }, [customer])


 
  const handleDelete = (id) => {
    setCustomer(customer.filter((item) => item.id !== id));
  };

  const columns = [
    { field: "id", headerName: "ID", width: 90 },
    {
      field: "user",
      headerName: "User",
      width: 200,
      renderCell: (params) => {
        return (
          <div className="userListUser">
            <img className="userListImg" src={params.row.image} alt="" />
            {params.row.name}
          </div>
        );
      },
    },
    { field: "email", headerName: "Email", width: 200 },
    {
      field: "status",
      headerName: "Phone",
      width: 120,
      renderCell: (params) => {
        return (
          <div className="userListUser">
            {params.row.phone}
          </div>
        );
      },
    },
    {
      field: "transaction",
      headerName: "Address",
      width: 160,
      renderCell: (params) => {
        return (
          <div className="userListUser">
            {params.row.address}
          </div>
        );
      },
    },
    {
      field: "action",
      headerName: "Action",
      width: 150,
      renderCell: (params) => {
        return (
          <>
            <Link to={"/user/" + params.row.id}>
              <button className="userListEdit">View Detail</button>
            </Link>
            <DeleteOutline
              className="userListDelete"
              onClick={() => handleDelete(params.row.id)}
            />
          </>
        );
      },
    },
  ];

  return (
    <div className="userList">
      <DataGrid
        rows={customer}
        disableSelectionOnClick
        columns={columns}
        pageSize={8}
        checkboxSelection
      />
    </div>
  );
}
