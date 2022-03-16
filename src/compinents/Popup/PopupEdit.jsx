import { Publish } from '@material-ui/icons';
import { Dialog, DialogContent, DialogTitle } from '@mui/material';
import React from 'react'
import axios from "axios";
import { useEffect, useState } from "react";
import { useParams } from "react-router";
import { useFormik } from "formik";
import * as yup from "yup";
import { useDispatch } from "react-redux";
import { updateUser } from "../../Module/action";
import { FormHelperText, TextField } from "@material-ui/core";




const schema = yup.object().shape({
    name: yup.string().required().trim(),
    phone: yup.string().required().trim(),
    email: yup.string().required().trim().email(),
    address: yup.string().required().trim(),

});
export default function PopupEdit(props) {
    const {  onpenPopup, setOpenPopup } = props;
    let { userId } = useParams();
    const [openPopup, setOpen]= useState(false);
    const [user, setUser] = React.useState({});
    const handleOpen = () => setOpenPopup(true);
    
    console.log(userId);
    useEffect(() => {
        axios.get(`https://traveltogetherr.somee.com/api/v1.0/customers/customers/id?id=${userId}`)
            .then((res) => {
                // console.log(res);
                setUser(res.data.data[0])
            })
            .catch((err) => { console.log(err); })

    }, [])
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

            // console.log(res);
            
            dispatch(updateUser(values,userId))
        },
    }, []);
    return (
        <Dialog open={onpenPopup}>
            <DialogContent>
                <div className="userUpdate">
                    <span className="userUpdateTitle">Edit</span>
                    <form className="userUpdateForm" onSubmit={formik.handleSubmit} >
                        <div className="userUpdateLeft">
                            <div className="userUpdateItem">
                                <label>Username</label>
                                <TextField
                                    id="outlined-basic"
                                    label="name"
                                    name="name"
                                    value={formik.values.name}
                                    variant="outlined"
                                    placeholder={user.name}
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
                            <div className="userUpdateItem">
                                <label>Email</label>
                                <TextField
                                id="outlined-basic"
                                label="email"
                                name="email"
                                value={formik.values.email || ""}
                                variant="outlined"
                                placeholder={user.email}
                                onChange={(e) => {
                                    formik.handleChange(e)
                                }}
                                onBlur={formik.handleBlur}
                            />{formik.touched.email && formik.errors.email && (
                                <FormHelperText
                                    error
                                    id="standard-weight-helper-text-username-login"
                                >
                                    {formik.errors.email}
                                </FormHelperText>
                            )}
                            </div>
                            <div className="userUpdateItem">
                                <label>Phone</label>
                                 <TextField
                                id="outlined-basic"
                                label="phone"
                                name="phone"
                                value={formik.values.phone || ""}
                                variant="outlined"
                                placeholder={user.phone}
                                
                                onChange={(e) => {
                                    formik.handleChange(e)
                                }}
                                onBlur={formik.handleBlur}
                            />{formik.touched.phone && formik.errors.phone && (
                                <FormHelperText
                                    error
                                    id="standard-weight-helper-text-username-login"
                                >
                                    {formik.errors.phone}
                                </FormHelperText>
                            )}
                            </div>
                            <div className="userUpdateItem">
                                <label>Address</label>
                                <TextField
                                id="outlined-basic"
                                label="address"
                                name="address"
                                value={formik.values.address || ""}
                                variant="outlined"
                                placeholder={user.address}
 
                                onChange={(e) => {
                                    formik.handleChange(e)
                                }}
                                onBlur={formik.handleBlur}
                            />{formik.touched.address && formik.errors.address && (
                                <FormHelperText
                                    error
                                    id="standard-weight-helper-text-username-login"
                                >
                                    {formik.errors.address}
                                </FormHelperText>
                            )}
                            </div>
                        </div>
                        <div className="userUpdateRight">
                            <div className="userUpdateUpload">
                                <img
                                    className="userUpdateImg"
                                    src={user.image}
                                    alt=""
                                />
                                <label htmlFor="file">
                                    <Publish className="userUpdateIcon" />
                                </label>
                                <input type="file" id="file" style={{ display: "none" }} />
                            </div>
                            <button className="userUpdateButton">Update</button>
                        </div>
                    </form>
                </div>

            </DialogContent>
        </Dialog>

    )
}
