# OnlineAuctionSite
This is a web application for an online auction site built using ASP.NET. The application allows users to create an account, browse items up for auction, place bids on items, and manage their own auctions.

Prerequisites
To run this application locally, you will need:

Visual Studio 2019 or later
.NET 5 SDK
SQL Server
Getting Started
Clone the repository:

bash -> Copy code
git clone https://github.com/username/auction-site.git

Open the solution in Visual Studio.

Update the connection string in the appsettings.json file to point to your local SQL Server instance.

Run the database migrations to create the necessary tables:

bash -> Copy code
dotnet ef database update

Build and run the application.

Features
User authentication and authorization
Browse items up for auction
Place bids on items
Create new auctions
Manage your own auctions
Search for items by name or category
Sort items by end time or current bid

Contributing
If you would like to contribute to this project, please fork the repository and submit a pull request. Issues and feature requests are also welcome.

License
This project is licensed under the MIT License. See the" LICENSE" file for details.




