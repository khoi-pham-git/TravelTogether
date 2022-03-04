
///tourguider là em này 
import { Link } from "react-router-dom";
import "./Tour.css";
import { Publish } from "@material-ui/icons";

export default function Tour() {
  return (
    <div className="Tour">
      <div className="TourTitleContainer">
        <h1 className="TourTitle">Tour</h1>
        <Link to="/newTour">
          <button className="TourAddButton">Create</button>
        </Link>
      </div>
      <div className="TourTop">
          <div className="TourTopLeft">
          </div>
          <div className="TourTopRight">
              <div className="TourInfoTop">
                  <img src="https://scontent.fsgn2-3.fna.fbcdn.net/v/t1.6435-9/139447725_1107181816401286_7367347044669173523_n.jpg?_nc_cat=106&ccb=1-5&_nc_sid=730e14&_nc_ohc=bhs3wX0UJ6AAX-Nd1A4&_nc_ht=scontent.fsgn2-3.fna&oh=00_AT_fC8tH4F9ZThaZ_7M18QcMzxb-jubetQ1rWivssd6PTQ&oe=622F3910" alt="" className="TourInfoImg" />
                  <span className="TourName">Nguyễn Văn A</span>
              </div>
              <div className="TourInfoBottom">
                  <div className="TourInfoItem">
                      <span className="TourInfoKey">id:</span>
                      <span className="TourInfoValue">123</span>
                  </div>
                  <div className="TourInfoItem">
                      <span className="TourInfoKey">sales:</span>
                      <span className="TourInfoValue">5123</span>
                  </div>
                  <div className="TourInfoItem">
                      <span className="TourInfoKey">active:</span>
                      <span className="TourInfoValue">yes</span>
                  </div>
                  <div className="TourInfoItem">
                      <span className="TourInfoKey">in stock:</span>
                      <span className="TourInfoValue">no</span>
                  </div>
              </div>
          </div>
      </div>
      <div className="TourBottom">
          <form className="TourForm">
              <div className="TourFormLeft">
                  <label>Tour Name</label>
                  <input type="text" placeholder="Nguyễn Văn A" />
                  <label>In Stock</label>
                  <select name="inStock" id="idStock">
                      <option value="yes">Yes</option>
                      <option value="no">No</option>
                  </select>
                  <label>Active</label>
                  <select name="active" id="active">
                      <option value="yes">Yes</option>
                      <option value="no">No</option>
                  </select>
              </div>
              <div className="TourFormRight">
                  <div className="TourUpload">
                      <img src="https://scontent.fsgn2-3.fna.fbcdn.net/v/t1.6435-9/139447725_1107181816401286_7367347044669173523_n.jpg?_nc_cat=106&ccb=1-5&_nc_sid=730e14&_nc_ohc=bhs3wX0UJ6AAX-Nd1A4&_nc_ht=scontent.fsgn2-3.fna&oh=00_AT_fC8tH4F9ZThaZ_7M18QcMzxb-jubetQ1rWivssd6PTQ&oe=622F3910" alt="" className="TourUploadImg" />
                      <label for="file">
                          <Publish/>
                      </label>
                      <input type="file" id="file" style={{display:"none"}} />
                  </div>
                  <button className="TourButton">Update</button>
              </div>
          </form>
      </div>
    </div>
  );
}
