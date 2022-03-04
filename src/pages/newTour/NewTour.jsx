import "./newTour.css";
import { useFormik } from "formik";
import * as yup from "yup";
import { Modal } from "@material-ui/core";
import { Box } from "@material-ui/system";
const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 400,
  bgcolor: "white",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,

};
export default function NewTour({ open, handleClose }) {
  return (
    <Modal
      open={open}
      onClose={handleClose}
      aria-labelledby="modal-modal-title"
      aria-describedby="modal-modal-description"
    >
      <Box sx={style}>
        <div className="newTour">
          <h1 className="addTourTitle">New Tour</h1>
          <form className="addTourForm">
            <div className="addTourItem">
              <label>Image</label>
              <input type="file" id="file" />
            </div>
            <div className="addTourItem">
              <label>Name Tour</label>
              <input type="text" placeholder="Du Lịch Đà Nẵng" />
            </div>
            <div className="addTourItem">
              <label>lộ trình </label>
              <input type="text" placeholder="1.254" />
            </div>
            <div className="addTourItem">
              <label>Active</label>
              <select name="active" id="active">
                <option value="yes">Yes</option>
                <option value="no">No</option>
              </select>
            </div>
         <div className="newUserItem">
              <label>Gender</label>
              <div className="newUserGender">
                <input type="radio" name="gender" id="male" value="male" />
                <label for="male">Male</label>
                <input type="radio" name="gender" id="female" value="female" />
                <label for="female">Female</label>
                <input type="radio" name="gender" id="other" value="other" />
                <label for="other">Other</label>
              </div>
            </div>
            <div className="newUserItem">
              <label>Active</label>
              <select className="newUserSelect" name="active" id="active">
                <option value="yes">Yes</option>
                <option value="no">No</option>
              </select>
            </div>
            <button className="addTourButton">Create</button>
          </form>
        </div>
      </Box>
    </Modal>
  );
}
