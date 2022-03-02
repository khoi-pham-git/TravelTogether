import "./tourList.css";
import { DataGrid } from "@material-ui/data-grid";
import { DeleteOutline } from "@material-ui/icons";
import { TourRows } from "../../dummyData";
import { Link } from "react-router-dom";
import { useEffect, useState } from "react";

import axios from "axios";

export default function TourList() {
  // const [data, setData] = useState(TourRows);
  const [tourguide,setTourguide] =useState([]);
  useEffect(()=>{
    axios.get("http://traveltogetherr.somee.com/api/v1.0/tourguide?ele=5&page=1").then((res)=>{
      setTourguide(res.data.data)
    }).catch(()=>{
      console.log("Error")
    })
  },[

  ])
  const handleDelete = (id) => {
    setTourguide(tourguide.filter((item) => item.id !== id));
  };

  const columns = [
    { field: "id", headerName: "ID", width: 90 },
    {
      field: "Name",
      headerName: "Name",
      width: 200,
      renderCell: (params) => {
        return (
          <div className="TourListItem">
            <img className="TourListImg" src={params.row.image} alt="" />
            {params.row.name}
          </div>
        );
      },
    },
    { field: "Address", headerName: "Address", width: 150 ,renderCell: (params)=>{
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
      renderCell:(params)=>{
        return(
        <div className="TourListItem">
          {params.row.phone}
        </div>
        );
      }
    },
    {
      field: "Phone Number",
      headerName: "Phone Number",
      width: 175,
      renderCell:(params)=>{
        return(
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
      renderCell:(params)=>{
        return(
        <div className="TourListItem">
          {params.row.email}
        </div>
        );
      }
    },
    {
      field: "Rank",
      headerName: "Rank",
      width: 175,
      renderCell:(params)=>{
        return(
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
          <>
            <Link to={"/tour/" + params.row.id}>
              <button className="TourListEdit">Edit</button>
            </Link>
            <DeleteOutline
              className="TourListDelete"
              onClick={() => handleDelete(params.row.id)}
            />
          </>
        );
      },
    },
  ];

  return (
    <div className="TourList">
      <DataGrid
        rows={tourguide}
        disableSelectionOnClick
        columns={columns}
        pageSize={8}
        checkboxSelection
      />
    </div>
  );
}
