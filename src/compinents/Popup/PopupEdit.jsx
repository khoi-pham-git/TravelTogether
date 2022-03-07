import { Publish } from '@material-ui/icons';
import { Dialog, DialogContent, DialogTitle } from '@mui/material';
import React from 'react'
import axios from "axios";
import { useEffect, useState } from "react";
import { useParams } from "react-router";
import { useFormik } from "formik";
import * as yup from "yup";
import { useDispatch } from "react-redux";
import { callAPIGetListUser, updateUser } from "../../Module/action";


const schema = yup.object().shape({
    name: yup.string().required().trim(),
    phone: yup.string().required().trim(),
    email: yup.string().required().trim().email(),
    address: yup.string().required().trim(),

});
export default function PopupEdit(props) {


    const { title, children, onpenPopup, setOpenPopup } = props;
    let { userId } = useParams();
    const [user, setUser] = React.useState({});
    const handleOpen = () => setOpenPopup(true);
    const handleClose = () => setOpenPopup(false);
    console.log(userId);
    useEffect(() => {
        axios.get(`http://traveltogetherr.somee.com/api/v1.0/customers/customers/id?id=${userId}`)
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
            //   handleClose();
            dispatch(updateUser())
        },
    }, []);
    return (
        <Dialog open={onpenPopup}>
            <DialogContent>
                <div className="userUpdate">
                    <span className="userUpdateTitle">Edit</span>
                    <form className="userUpdateForm">
                        <div className="userUpdateLeft">
                            <div className="userUpdateItem">
                                <label>Username</label>
                                <input
                                    type="text"
                                    placeholder={user.name}
                                    className="userUpdateInput"

                                />
                            </div>
                            <div className="userUpdateItem">
                                <label>Email</label>
                                <input
                                    type="text"
                                    placeholder={user.email}
                                    className="userUpdateInput"
                                />
                            </div>
                            <div className="userUpdateItem">
                                <label>Phone</label>
                                <input
                                    type="text"
                                    placeholder={user.phone}
                                    className="userUpdateInput"
                                />
                            </div>
                            <div className="userUpdateItem">
                                <label>Address</label>
                                <input
                                    type="text"
                                    placeholder={user.address}
                                    className="userUpdateInput"
                                />
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
