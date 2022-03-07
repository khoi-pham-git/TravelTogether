import Sidebar from "../../compinents/sidebar/Sidebar";
import Topbar from "../../compinents/topbar/Topbar";
import React from 'react';
import { Route } from "react-router-dom";
import { Redirect } from "react-router-dom";
const HomeLayout = (props) => {
    return (
        <React.Fragment>
            <Topbar />
            <div className="container">
                <Sidebar />
                {props.children}
            </div>
        </React.Fragment>
    );
};

export default function HomeTemplate({ Component, ...props }) {
    return (
      <Route
        {...props}
        render={(propsComponent) => {
            if (true) {
                return (
                    <HomeLayout>
                      <Component {...propsComponent} />
                    </HomeLayout>
                  )
            }else{
                return <Redirect to="/login"/>
            }
        }}
      />
    );
  }