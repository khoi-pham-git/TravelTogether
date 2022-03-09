import "./tourList.css";
import { DataGrid } from "@material-ui/data-grid";
import { DeleteOutline } from "@material-ui/icons";
import { Link } from "react-router-dom";
import { useEffect, useState } from "react";
import AddIcon from '@mui/icons-material/Add';
import * as React from 'react';
import axios from "axios";
import NewTour from "../newTour/NewTour";
import NewBooking from '../NewBooking/NewBooking'
import Button from '@material-ui/core/Button';


export default function TourList() {
  // const [data, setData] = useState(TourRows);
  const [open, setOpen] = React.useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);
  const [tourguide, setTourguide] = useState([]);
  useEffect(() => {
    axios.get("https://traveltogetherr.somee.com/api/v1.0/tourguide?page=1").then((res) => {
      console.log(res);
      setTourguide(res.data.data)
    }).catch(() => {
      console.log("Error")
    })
  }, [

  ])
  const handleDelete = (id) => {
    setTourguide(tourguide.filter((item) => item.id !== id));
  };

  const columns = [
    { field: "id", headerName: "ID", width: 100 },
    {
      field: "Name",
      headerName: "Name",
      width: 150,
      renderCell: (params) => {
        return (
          <div className="TourListItem">
            <img className="TourListImg" src={params.row.image} alt="" />
            {params.row.name}
          </div>
        );
      },
    },
    {
      field: "Address", headerName: "Address", width: 150, renderCell: (params) => {
        return (
          <div className="TourListItem">
            {params.row.address}
          </div>
        );
      },
    },

    {
      field: "Phone Number",
      headerName: "Phone Number",
      width: 175,
      renderCell: (params) => {
        return (
          <div className="TourListItem">
            {params.row.phone}
          </div>
        );
      }
    },
    {
      field: "Email",
      headerName: "Email",
      width: 175,
      renderCell: (params) => {
        return (
          <div className="TourListItem">
            {params.row.email}
          </div>
        );
      }
    },
    {
      field: "Rank",
      headerName: "Rank",
      width: 100,
      renderCell: (params) => {
        return (
          <div className="TourListItem">
            {params.row.rank}
          </div>
        );
      }
    },
    {
      field: "action",
      headerName: "Action",
      width: 150,
      renderCell: (params) => {
        return (
          <div>
            <Link to={"/tour/" + params.row.id}>
              <button className="TourListEdit">Edit</button>
            </Link>
            <DeleteOutline
              className="TourListDelete"
              onClick={() => handleDelete(params.row.id)}
            />
          </div>
        );
      },
    },
  ];

  return (

    <div className="TourList">
      <Button
        variant="contained"
        color="primary"
        size="small"

        startIcon={<AddIcon />}
        onClick={handleOpen}
      >
        Create
      </Button>
      {tourguide && <DataGrid
        rows={tourguide}
        disableSelectionOnClick
        columns={columns}
        pageSize={8}
        checkboxSelection
      />}
      <NewTour open={open} handleClose={handleClose} />
    </div>
  );
}
