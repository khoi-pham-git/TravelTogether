import React from "react";
import "./topbar.css";
import { NotificationsNone, Language, Settings } from "@material-ui/icons";

export default function Topbar() {
  return (
    <div className="topbar">
      <div className="topbarWrapper">
        <div className="topLeft">
          <span className="logo">TourGuide</span>
        </div>
        <div className="topRight">
          <div className="topbarIconContainer">
            <NotificationsNone />
            <span className="topIconBadge">2</span>
          </div>
          <div className="topbarIconContainer">
            <Language />
            <span className="topIconBadge">2</span>
          </div>
          <div className="topbarIconContainer">
            <Settings />
          </div>
          <img src="https://scontent.fsgn2-1.fna.fbcdn.net/v/t1.6435-9/44114772_548800558906084_7132654572174049280_n.jpg?_nc_cat=111&ccb=1-5&_nc_sid=8bfeb9&_nc_ohc=VO2ByCBijnAAX896P3m&_nc_ht=scontent.fsgn2-1.fna&oh=00_AT_BftNr6lo3x200xUxMNwnfo-htVrRm7mCmYTpe08K7OQ&oe=623035D4" alt="" className="topAvatar" />
        </div>
      </div>
    </div>
  );
}
