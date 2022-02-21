import React from 'react'
import FeaturedInfo from '../../compinents/featuredInfo/FeaturedInfo'
import "./home.css"
import WidgetSm from "../../compinents/widgetSm/WidgetSm";
import WidgetLg from "../../compinents/widgetLg/WidgetLg";

export default function  Home() {
  return (
    <div className="home">
        <FeaturedInfo/>
        <div className="homeWidgets">
        <WidgetSm/>
        <WidgetLg/>
      </div>
    </div>
  )
}
