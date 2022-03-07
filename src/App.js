import Topbar from "./compinents/topbar/Topbar";
import Sidebar from "./compinents/sidebar/Sidebar";
import './App.css';
import Home from "./pages/home/home";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import UserList from "./pages/userList/UserList";
import User from "./pages/user/User";
import NewUser from "./pages/newUser/NewUser";
import NewTour from "./pages/newTour/NewTour";
import Tour from "./pages/Tourguide/Tour";
import TourList from "./pages/TourguideList/TourList";
import ListBooking from "./pages/Booking/ListBooking";
import TraveList from "./pages/TourList/TraveList";
import { Redirect } from "react-router-dom";
import SignInSide from "./pages/login/login";
import HomeTemplate from "./pages/HomeTemplate/HomeTemplate";
import { routesHome } from "./route";
import { BrowserRouter } from "react-router-dom";


function App() {
  const showMenuHome = (routes) => {
    if (routes && routes.length > 0) {
      return routes.map((item, index) => {
        return (
          <HomeTemplate
            key={index}
            exact={item.exact}
            path={item.path}
            Component={item.component}
          />
        );
      });
    }
  };
  return (
    // <Router >
    //   <Switch>

    //     <Topbar />
    //     <div className="container">
    //       <Sidebar />
    //       <Route exact path="/">
    //         <Home />
    //       </Route>

    //       <Route exact path="/users">
    //         <UserList />
    //       </Route>
    //       <Route path="/user/:userId">
    //         <User />
    //       </Route>
    //       <Route path="/newUser">
    //         <NewUser />
    //       </Route>
    //       <Route path="/newTour">
    //         <NewTour />
    //       </Route>
    //       <Route path="/Tour/:TourId">
    //         <Tour />
    //       </Route>
    //       <Route path="/Tour">
    //         <TourList />
    //       </Route>
    //       <Route path="/Booking">
    //         <ListBooking />
    //       </Route>
    //       <Route path="/TravelTour">
    //         <TraveList />
    //       </Route>
    //     </div>
    //   </Switch>

    // </Router>
    <BrowserRouter>
      <Switch>
        {showMenuHome(routesHome)}
        <Route exact path="/login">
          <SignInSide />
        </Route>
      </Switch>
    </BrowserRouter>
  );
}

export default App;
