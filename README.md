# DIRIGIBLE APP
Dirigible is a social flight-data platform. It’s a single destination to search for and retrieve flight information, including departure and arrival times, current location, weather at both the origin and destination, and details about delays.

Currently, users can search by route, origin and departure airport, flight number (IATA), and incident status. Dirigible provides details about the aircraft, including its registration and photos, eliminating the need to visit multiple websites to access this information.

In addition to searching for flights, users can comment and reply to one another, sharing real-time thoughts on their journeys. As users interact with the platform through a karma system, they can build status within the Dirigible community, earning cool badges and titles along the way.

## PROJECT SUMMARY
This applicaion is built in .NET 9, using an MVC pattern, and is comprised of a RESTful ASP.NET web API and a MAUI/Blazor hybrid app within a single solution. 

Users can perform all CRUD operations on a 3NF Microsoft SQL Server database that safely stores user data and activity across the app. Authorization and database interactions are managed with Entity Framework Core.

JWT-based authentication ensures secure API access, with role-based authorization restricting access to sensitive endpoints.

Each backend layer is comprehensively tested with NUnit and Moq, and the frontend UI was styled primarily using Bulma. 

The app consumes six external APIs to fetch flight, weather, photo, airline, map and geolocation data. Each external API and the database has automated health checks, with real-time status displayed on the site.

The search endpoint of our backend is Rate-Limited and uses In-Memory Caching to reduce database calls and improve response times. Search queries support pagination, multiple filtering options, and the frontend UI includes autocomplete fields for departure and arrival airports.

Login and registration forms are validated on the frontend to ensure emails and passwords are in the correct format, and comments are validated with a rude-word filter to keep everything wholesome. Various other custom validation is used accross the app to improve the user experience.

## Upcoming Features
* Live Chatroom
* More admin abilities such as banning
* Homepage Carousel
* Backend Logging
* Livechat
* Passenger Verfication
