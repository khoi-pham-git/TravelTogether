import axios from "axios";
import * as  Action from "../action/action";
export const ActionGetListUser = (payload) => {
    return {
      type: Action.GET_LIST_USER,
      payload
    };
  };

  const ActionDelte = ()=>{
    return {
      type: Action.DELETE_USER,
    };
  }

  const userUpdate = () => {
    return{
      type:Action.EDIT_USER,
    };
  };

  export const callAPIGetListUser  = () => {
      return (dispatch) => {
        axios.get("https://traveltogetherr.somee.com/api/v1.0/customers/customers")
        .then((res) => { 
            dispatch(ActionGetListUser(res.data.data));
            
          })
        .catch((err) => { console.log(err); })
      }
  }
  
  export const ActionGetListTour = (payload) => {
    return {
      type: Action.GET_LIST_TOUR,
      payload
    };
  };


  export const updateUser  = (user, id) => {
    return (dispatch) => {
      axios.put(`https://traveltogetherr.somee.com/api/v1.0/customers/customers/${id}`,user)
      .then((res) => { 
          dispatch(userUpdate());
        })
      .catch((err) => { console.log(err); })
    }
}
  
  

  