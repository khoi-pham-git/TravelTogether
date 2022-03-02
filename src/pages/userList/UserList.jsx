import "./userList.css";
import { DataGrid } from "@material-ui/data-grid";
import { DeleteOutline } from "@material-ui/icons";
import { Link } from "react-router-dom";
import { useEffect } from "react";
import * as React from 'react';
import axios from "axios";
import { useDispatch, useSelector } from "react-redux";
import { callAPIGetListUser } from "./Module/action";
import AddIcon from '@mui/icons-material/Add';
import ModalUser from "../Modal/Modal";
axios.defaults.baseURL = 'https://jsonplaceholder.typicode.com';
export default function UserList() {
  // const [data, setData] = useState(userRows);
  // const [customer, setCustomer] = useState([]); 
  const [open, setOpen] = React.useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);
  const dispatch = useDispatch();
  const customer = useSelector((state)=>{
    return state.user.listUser;
  });
  console.log(customer);
  useEffect(() => {
    dispatch(callAPIGetListUser())
  }, [dispatch])


 
  const handleDelete = (id) => {
    // setCustomer(customer.filter((item) => item.id !== id));
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
      <AddIcon onClick={handleOpen}/>
      {customer && <DataGrid
        rows={customer}
        disableSelectionOnClick
        columns={columns}
        pageSize={8}
        checkboxSelection
      />}
      <ModalUser open={open} handleClose={handleClose}  />
    </div>
  );
}
