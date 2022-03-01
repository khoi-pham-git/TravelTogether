import Topbar from "./compinents/topbar/Topbar";
import Sidebar from "./compinents/sidebar/Sidebar";
import './App.css';
import Home from "./pages/home/home";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import UserList from "./pages/userList/UserList";
import User from "./pages/user/User";
import NewUser from "./pages/newUser/NewUser";
import NewTour from "./pages/newTour/NewTour";
import Tour from "./pages/Tour/Tour";
import TourList from "./pages/TourList/TourList";
import Booking from "./pages/Booking/Booking";
import ListBooking from "./pages/Booking/ListBooking";





function App() {
  return (
    <Router >
      <Topbar />
      <div className="container">
        <Sidebar />
        <Switch>
          <Route exact path="/">
            <Home />
          </Route>
          <Route exact path="/users">
           <UserList/>
          </Route>
          <Route path="/user/:userId">
            <User />
          </Route>
          <Route path="/newUser">
            <NewUser/>
          </Route>
          <Route path="/newTour">
            <NewTour />
          </Route>
          <Route path="/Tour/:TourId">
            <Tour />
          </Route>
          <Route path="/Tour">
            <TourList />
          </Route>
          <Route path="/Booking">
            <ListBooking/>
          </Route>
        </Switch>
      </div>
    </Router>
  );
}

export default App;
