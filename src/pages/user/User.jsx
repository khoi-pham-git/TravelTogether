import {
  CalendarToday,
  LocationSearching,
  MailOutline,
  PermIdentity,
  PhoneAndroid,
  Publish,
} from "@material-ui/icons";
import { Link } from "react-router-dom";
import "./user.css";
import { useParams } from "react-router";
import  axios  from "axios";
import { useEffect, useState } from "react";
import React from "react";
import PopupEdit from "../../compinents/Popup/PopupEdit";


export default function User() {
  const [openPopup, setOpenPopup]= useState(false);
  let {userId}=useParams();
  const [user, setUser] = React.useState({});
  console.log(userId);
  useEffect(() => {
    axios.get(`http://traveltogetherr.somee.com/api/v1.0/customers/id?id=${userId}`)
      .then((res) => { 
        // console.log(res);
        setUser(res.data.data[0]) 
      })
      .catch((err) => { console.log(err); })
  
  }, [])
  return (
    <div className="user">
      <div className="userTitleContainer">
        <h1 className="userTitle">Edit User</h1>
        <Link to="/newUser">
          <button className="userAddButton">Create</button>
        </Link>
      </div>
      <div className="userContainer">
        <div className="userShow">
        <div className="userShowTop">
            <img
              src={user.image}
              alt=""
              className="userShowImg"
            />
          
          </div>
        </div>
        <div className="userUpdate">
          <div className="userShowBottom">
            <span className="userShowTitle">Account Details</span>
            <div className="userShowInfo">
              <PermIdentity className="userShowIcon" />
              <span className="userShowInfoTitle">{user.name}</span>
            </div>
            <div className="userShowInfo">
              <CalendarToday className="userShowIcon" />
              <span className="userShowInfoTitle">10.12.1999</span>
            </div>
            <span className="userShowTitle">Contact Details</span>
            <div className="userShowInfo">
              <PhoneAndroid className="userShowIcon" />
              <span className="userShowInfoTitle">{user.phone}</span>
            </div>
            <div className="userShowInfo">
              <MailOutline className="userShowIcon" />
              <span className="userShowInfoTitle">{user.email}</span>
            </div>
            <div className="userShowInfo">
              <LocationSearching className="userShowIcon" />
              <span className="userShowInfoTitle">{user.address}</span>
            </div>
        
          <button className="userAddButton" onClick={()=> setOpenPopup(true)}>Edit</button>
         
          </div>
          <PopupEdit onpenPopup={openPopup}/>
        </div>
      </div>
    </div>
  );
}
