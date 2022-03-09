import './Booking.css'
import { DataGrid } from "@material-ui/data-grid";
import { DeleteOutline } from "@material-ui/icons";
import { useState, useEffect } from "react";
import * as React from 'react';
import axios from "axios";
import AddIcon from '@mui/icons-material/Add';
import NewBooking from '../NewBooking/NewBooking'
import Button from '@material-ui/core/Button';
import { callAPIGetListUser } from '../../Module/action';
import { useDispatch, useSelector } from "react-redux";

// chỗ này dùng axios để call api 
axios.defaults.baseURL = 'https://jsonplaceholder.typicode.com';
export default function ListBooking() {
  
  const [open, setOpen] = React.useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);
  const dispatch = useDispatch();
  // const [data, setData] = useState(BookingRows);
  //rows là biến, setRows để set giá trị cho cái coodj 
  const [rows, setRows] = useState([]);

  //userEffect để quản lí vòng đời á (lifecycle)
  useEffect(() => {
    axios.get("https://traveltogetherr.somee.com/api/v1.0/trips?ele=10&page=1")
      //dòng 17 lấy cái link API rồi set nó vào một cái response
      .then((res) => {
        setRows(res.data.data)
        //rồi từ cái res(response) gọi đến data rồi set nó cho từng cột
        // nếu muốn hiểu hơn lên lòng 19 consle.log(res) vào browser f12 lên là thấy 
      })
      .catch((err) => { console.log(err); })
  }, [])



  // cái này là đỗ dữ liệu vào cái file theo Id thôi ku
  const handleDelete = (id) => {
    // setRows(rows.filter((item) => item.id !== id));
    axios.delete  (`http://traveltogetherr.somee.com/api/v1.0/trips/${id}`)
      .then((res) => {
        console.log(res);
        dispatch(callAPIGetListUser());
      })
      .catch((err) => { alert("remove faild " + id); })
  };

  // từ dòng 32 trở đi là đỗ dữ liệu cho từng cột cái nào ko hiểu ib  tao 
  const columns = [
    { field: "id", headerName: "ID", width: 190 },
    {
      field: "BookingRequestDate",
      headerName: "BookingRequestDate",
      width: 200,
      renderCell: (params) => {
        return (
          <div className="BookingListBooking">
           
            {params.row.bookingDate}
          </div>
        );
      },
    },
    { field: "price", headerName: "price", width: 200 },
    {
      field: "price",
      headerName: "price",
      width: 120,
      renderCell: (params) => {
        return (
          <div className="BookingListBooking">
            {params.row.price}
          </div>
        );
      },
    },
    {
      field: "Start",
      headerName: "Start",
      width: 160,
      renderCell: (params) => {
        return (
          <div className="BookingListBooking">
            {params.row.startDate}
          </div>
        );
      },
    },
    {
      field: "price",
      headerName: "EndDate",
      width: 160,
      renderCell: (params) => {
        return (
          <div className="BookingListBooking">
            {params.row.endDate}
          </div>
        );
      },
    },
    
    {
      field: "Action",
      headerName: "Action",
      width: 150,
      renderCell: (params) => {
        return (

          <DeleteOutline
            className="BookingListDelete"
            onClick={() => handleDelete(params.row.id)}
          />

        );
      },
    },
  ];

  return (

    <div className="BookingList">
      <Button
        variant="contained"
        color="primary"
        size="small"
        
        startIcon={<AddIcon/>}
        onClick={handleOpen}
      >
        Create
      </Button>
      {rows && <DataGrid
        rows={rows}
        disableSelectionOnClick
        columns={columns}
        pageSize={8}
        checkboxSelection
      />
      }
      <NewBooking open={open} handleClose={handleClose} />
    </div>
  );
}






