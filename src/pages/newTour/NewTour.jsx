import "./newTour.css";
import { useFormik } from "formik";
import * as yup from "yup";
import { Modal } from "@material-ui/core";
import { Box } from "@material-ui/system";
import { useDispatch } from "react-redux";
import axios from "axios";
import { FormHelperText, TextField } from "@material-ui/core";
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
const schema = yup.object().shape({
  name: yup.string().required().trim(),
  price: yup.string().required().trim(),
  status: yup.string().required().trim(),
  quatityTrip: yup.string().required().trim(),
  tourguidID: yup.string().required().trim(),

});
export default function NewTour({ open, handleClose }) {
  const dispatch = useDispatch();
  const formik = useFormik({
    validationSchema: schema,
    validateOnMount: true,
    validateOnBlur: true,
    initialValues: {
      name: "",
      price: "",
      status: "",
      quatityTrip: "",

    }, onSubmit: async (values) => {
      console.log(values);
      const data = {
        name: formik.values.name,
        price: formik.values.price,
        status: formik.values.status,
        quatityTrip: formik.values.quatityTrip,
        tourguidID: formik.values.tourguidID
      }
      const res = await axios({
        method: "POST", url: "https://traveltogetherr.somee.com/api/v1.0/tours", data,
      })
      console.log(res);
      handleClose();
      // dispatch(callAPIGetListUser())
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
        <div className="newTour">
          <h1 className="addTourTitle">New Tour</h1>
          <form className="addTourForm">
         
            <div className="addTourItem">
              <label>Name Tour</label>
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
            <div className="addTourItem">
              <label>price </label>
              <TextField
              id="outlined-basic"
              label="price"
              name="price"
              value={formik.values.price}
              variant="outlined"
              onChange={(e) => {
                formik.handleChange(e)
              }}
              onBlur={formik.handleBlur}
            />{formik.touched.price && formik.errors.price && (
              <FormHelperText
                error
                id="standard-weight-helper-text-username-login"
              >
                {formik.errors.price}
              </FormHelperText>
            )}
            </div>
            <div className="addTourItem">
            <label>quatityTrip </label>
            <TextField
            id="outlined-basic"
            label="quatityTrip"
            name="quatityTrip"
            value={formik.values.quatityTrip}
            variant="outlined"
            onChange={(e) => {
              formik.handleChange(e)
            }}
            onBlur={formik.handleBlur}
          />{formik.touched.quatityTrip && formik.errors.quatityTrip && (
            <FormHelperText
              error
              id="standard-weight-helper-text-username-login"
            >
              {formik.errors.quatityTrip}
            </FormHelperText>
          )}
          </div>
          <div className="addTourItem">
            <label>tourguidID </label>
            <TextField
            id="outlined-basic"
            label="tourguidID"
            name="tourguidID"
            value={formik.values.tourguidID}
            variant="outlined"
            onChange={(e) => {
              formik.handleChange(e)
            }}
            onBlur={formik.handleBlur}
          />{formik.touched.tourguidID && formik.errors.tourguidID && (
            <FormHelperText
              error
              id="standard-weight-helper-text-username-login"
            >
              {formik.errors.tourguidID}
            </FormHelperText>
          )}
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



// <div className="addTourItem">
// <label>Image</label>
// <input type="file" id="file" />
// </div>