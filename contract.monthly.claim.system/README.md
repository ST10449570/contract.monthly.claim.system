# Contract Monthly Claim System

PROG6212 – Programming 2A  
ST10449570 
Date: 24 October 2025  

## Overview
This ASP.NET Core MVC web application allows lecturers to submit monthly claims, upload supporting files, and enables coordinators or managers to review, approve, or reject claims. It provides a modern, responsive interface and uses SQL Server LocalDB for persistent storage.

## Technologies Used
- C# and ASP.NET Core MVC  
- Entity Framework Core (EF Core)  
- SQL Server LocalDB  
- Razor Pages, HTML, and CSS  
- Visual Studio 2022  

## How to Run
1. Open the solution in Visual Studio 2022.  
2. In the Package Manager Console, run:
   ```bash
   dotnet restore
   dotnet build
   dotnet ef database update
   dotnet run

## Features
Lecturer claim submission (hours worked, hourly rate, notes)
Secure document upload (.pdf, .docx, .xlsx)
Review and approval by coordinators/managers
SQL Server database integration
Simple and accessible user interface following modern usability principles
 
## References
Microsoft. (2024). ASP.NET Core MVC documentation.
Microsoft. (2024). Entity Framework Core Overview.
Nielsen Norman Group. (2023). 10 Usability Heuristics for User Interface Design.

# Final submission 24 october 2025