import "./newUser.css";
import * as React from "react";
import Box from "@mui/material/Box";
import Modal from "@mui/material/Modal";
import { FormHelperText, TextField } from "@material-ui/core";
import Typography from "@material-ui/core/Typography";
import { useFormik } from "formik";
import * as yup from "yup";
import { useDispatch } from "react-redux";
import axios from "axios";
import { callAPIGetListUser } from "../../Module/action";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 400,
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
};
const schema = yup.object().shape({
  name: yup.string().required().trim(),
  phone: yup.string().required().trim(),
  email: yup.string().required().trim().email(),
  address: yup.string().required().trim(),

});
export default function NewUser({ open, handleClose }) {
  const dispatch = useDispatch();
  const formik = useFormik({
    validationSchema: schema,
    validateOnMount: true,
    validateOnBlur: true,
    initialValues: {
      name: "",
      phone: "",
      email: "",
      address: "",

    },
    onSubmit: async (values) => {
      console.log(values);
      const data = {
        name: formik.values.name,
        phone: formik.values.phone,
        email: formik.values.email,
        address: formik.values.address
      }
      const res = await axios({
        method: "POST", url: "https://traveltogetherr.somee.com/api/v1.0/customers", data,
      })
      console.log(res);
      handleClose();
      dispatch(callAPIGetListUser())
    },
  }, []);
  return (
    <Modal
      open={open}
      onClose={handleClose}
      aria-labelledby="modal-modal-title"
      aria-describedby="modal-modal-description"
    >
      <Box sx={style}>
        <Typography variant="h4" component="h2" gutterBottom>
          Create User
        </Typography>
        
        <div className="newUser">
          <form className="newUserForm" onSubmit={formik.handleSubmit}  >
            <div className="newUserItem">
              <label>Username</label>
              <TextField
              id="outlined-basic"
              label="name"
              name="name"
              value={formik.values.name}
              variant="outlined"
              onChange={(e) => {
                formik.handleChange(e)
              }}
              onBlur={formik.handleBlur}
            />{formik.touched.name && formik.errors.name && (
              <FormHelperText
                error
                id="standard-weight-helper-text-username-login"
              >
                {formik.errors.name}
              </FormHelperText>
            )}
            </div>
            <div className="newUserItem">
              <label>Email</label>
              <TextField
                id="outlined-basic"
                label="email"
                name="email"
                value={formik.values.email}
                variant="outlined"
                onChange={formik.handleChange}
              />
              {formik.touched.email && formik.errors.email && (
                <FormHelperText
                  error
                  id="standard-weight-helper-text-username-login"
                >
                  {formik.errors.email}
                </FormHelperText>
              )}
            </div>
            <div className="newUserItem">
              <label>Phone</label>
              <TextField
              id="outlined-basic"
              label="phone"
              name="phone"
              value={formik.values.phone}
              onChange={formik.handleChange}
              variant="outlined"
            />
            {formik.touched.phone && formik.errors.phone && (
              <FormHelperText
                error
                id="standard-weight-helper-text-username-login"
              >
                {formik.errors.phone}
              </FormHelperText>
            )}
            </div>
            <div className="newUserItem">
              <label>Address</label>
              <TextField
                id="outlined-basic"
                label="address"
                name="address"
                value={formik.values.address}
                onChange={formik.handleChange}
                variant="outlined"
              />
              {formik.touched.address && formik.errors.address && (
                <FormHelperText
                  error
                  id="standard-weight-helper-text-username-login"
                >
                  {formik.errors.address}
                </FormHelperText>
              )}
            </div>
          
            <button class="addTourButton" >Create</button>
          </form>
        </div>
      </Box>
    </Modal>
  );
}




// <div className="newUserItem">
// <label>Gender</label>
// <div className="newUserGender">
//   <input type="radio" name="gender" id="male" value="male" />
//   <label for="male">Male</label>
//   <input type="radio" name="gender" id="female" value="female" />
//   <label for="female">Female</label>
//   <input type="radio" name="gender" id="other" value="other" />
//   <label for="other">Other</label>
// </div>
// </div>
// <div className="newUserItem">
// <label>Active</label>
// <select className="newUserSelect" name="active" id="active">
//   <option value="yes">Yes</option>
//   <option value="no">No</option>
// </select>
// </div>