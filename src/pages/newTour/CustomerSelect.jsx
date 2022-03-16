import React from 'react'
import { Select } from '@material-ui/core';

export default ({onChange ,option,values,className})=>{
    const defaultValue = (option,values)=>{
        return option ? option.find(option=>option.values===values):"true"
    }
    return(
        <div className={className}>
        <Select
        value={defaultValue(option,values)}
        onChange={values=>onChange(values)}
        option={option}>
        </Select>
        </div>
    )
}