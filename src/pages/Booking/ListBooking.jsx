
import { DataGrid } from "@material-ui/data-grid";
import { DeleteOutline } from "@material-ui/icons";
import { Link } from "react-router-dom";
import { useState,useEffect } from "react";
import axios from "axios";

axios.defaults.baseURL = 'https://jsonplaceholder.typicode.com';
export default function ListBooking() {
  // const [data, setData] = useState(userRows);
  const [Booking, setBooking] = useState([]); 
  
  useEffect(() => {
    axios.get("http://traveltogetherr.somee.com/api/v1.0/tours")
      .then((res) => { 
        console.log(Booking)
        
        setBooking(res.data.data)
        console.log(Booking); })
      .catch((err) => { console.log(err); })
  }, [Booking])


 
  const handleDelete = (id) => {
    setBooking(Booking.filter((item) => item.id !== id));
  };

  const columns = [
    { field: "id", headerName: "ID", width: 90 },
    {
      field: "BookingRequest",
      headerName: "BookingRequest",
      width: 200,
      renderCell: (params) => {
        return (
          <div className="userListUser">
            <img className="userListImg" src={params.row.name} alt="" />
            {params.row.name}
          </div>
        );
      },
    },
    { field: "tourGuideId", headerName: "tourGuideId", width: 200 },
    {
      field: "status",
      headerName: "price",
      width: 120,
      renderCell: (params) => {
        return (
          <div className="userListUser">
            {params.row.price}
          </div>
        );
      },
    },
    {
      field: "transaction",
      headerName: "quantityTrip",
      width: 160,
      renderCell: (params) => {
        return (
          <div className="userListUser">
            {params.row.quatityTrip}
          </div>
        );
      },
    },
    {
      field: "user",
      headerName: "user",
      width: 150,
      renderCell: (params) => {
        return (
          <>
            
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
        rows={Booking}
        disableSelectionOnClick
        columns={columns}
        pageSize={8}
        checkboxSelection
      />
    </div>
  );
}
