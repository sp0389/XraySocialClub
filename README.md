# Xray Social Club
Xray Social Club is a member, payment & purchase tracking application I made for a social club to reduce the reliance on Excel spreadsheets and manual paper records. 
It is designed to be intutive and easy to use.

## Features
 - Create board announcements / comment on existing announcements
 - Register new members
 - Update and assign roles
 - Create club purchase records

## Technology Stack Used
- ASP.NET Core MVC
- Entity Framework Core
- Identity Framework
- Microsoft SQL Server
- LINQ
- Bootstrap
  
## Requirements
- .NET 9 SDK
- SQL Server
- Visual Studio / VS Code / Rider
- Git
- Cloudinary Account (for uploading images to announcements).

## Getting Started
1. Clone the repository
```
https://github.com/sp0389/XraySocialClub.git
```
2. Change into the project directory
```
cd XraySocialClub
```
3. Run database migrations:
```
dotnet ef database update
```
4. Start the application:
```
dotnet run
```
## Usage
- Access the web app via https://localhost:7237 (or configured port).
- Login as Administrator to manage users and roles.
- Create new member / payment records.

## Deployment
```
dotnet publish -c Release
```
Deploy to Azure, IIS, or containerize with Docker.
## Contributing

1. Fork the repository.
2. Create a feature branch.
3. Commit changes.
4. Open a Pull Request.

## Screenshots
<img width="1517" height="1249" alt="Image" src="https://github.com/user-attachments/assets/c67d3632-e0da-45f4-b9a1-7e61a518d9a4" />
