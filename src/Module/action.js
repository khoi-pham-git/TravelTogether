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

  export const callAPIGetListUser  = () => {
      return (dispatch) => {
        axios.get("http://traveltogetherr.somee.com/api/v1.0/customers?page=2")
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

  