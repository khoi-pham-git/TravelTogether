import ListBooking from "./pages/Booking/ListBooking";
import Home from "./pages/home/home";
import NewTour from "./pages/newTour/NewTour";
import NewUser from "./pages/newUser/NewUser";
import TourList from "./pages/TourguideList/TourList";
import TraveList from "./pages/TourList/TraveList";
import User from "./pages/user/User";
import UserList from "./pages/userList/UserList";
const routesHome = [
    {
        path: "/",
        exact: true,
        component: Home,
    },
    {
        path: "/users",
        exact: false,
        component: UserList,
    },
    {
        path: "/user/:userId",
        exact: false,
        component: User,
    },
    {
        path: "/newUser",
        exact: false,
        component: NewUser,
    },
    {
        path: "/newTour",
        exact: false,
        component: NewTour,
    },
    {
        path: "/Tour",
        exact: false,
        component: TourList,
    },
    {
        path: "/Booking",
        exact: false,
        component: ListBooking,
    },
    {
        path: "/TravelTour",
        exact: false,
        component: TraveList,
    }

];
export { routesHome };