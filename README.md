# Izenda Custom Bootstrapper

 :warning: **This project is designed for demonstration purposes, please ensure that security and customization meet the standards of your company.**
 
 
## Overview
A custom bootstrapper allows you to modify requests and responses to the Izenda API (.NET Core resources). 

## A Few Uses Cases
1. Filtering down a large list of tenants based on some custom criteria.
2. Removing items from filter field data.

Currently, this repo only shows how to remove items from filter field data.

## Required References:

1. Izenda.BI.API.dll  
2. Izenda.BI.Framework.dll 
3. Nancy.dll

## Installation

1. Build the project and copy the DLL into the Izenda API directory along with the other Izenda API DLL files.
   
2. Add the following key/values to the appSettings.json (API) file to use the custom bootstrapper.
```
  "izenda.nancyfx.bootstrapper.dll": "IzendaCustomBootstrapper.dll",
  "izenda.nancyfx.bootstrapper.type": "IzendaCustomBootstrapper.CustomBootstrapper",
```

3. Restart the API instance