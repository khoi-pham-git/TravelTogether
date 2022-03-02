import axios from "axios";
import * as  Action from "../../../action/action";
export const ActionGetListUser = (payload) => {
    return {
      type: Action.GET_LIST_USER,
      payload
    };
  };

  export const callAPIGetListUser  = () => {
      return (dispatch) => {
        axios.get("http://traveltogetherr.somee.com/api/v1.0/customers?ele=10&page=1")
        .then((res) => { 
            dispatch(ActionGetListUser(res.data.data));
            // console.log(res);
          })
        .catch((err) => { console.log(err); })
      }
  }