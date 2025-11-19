# Contract Monthly Claim System

PROG6212 – Programming 2A  
ST10449570 
Date: 21 November 2025  

# Overview
This is my Part 3 submission for the PROG6212 POE.
In this part, I continued building on the web application from Part 2 and added extra features for the review and approval side of the system.


# What This Project Does

This web app lets lecturers submit their monthly claim forms, and then HR or the programme coordinator can review them. In Part 3, I added more functionality so the reviewing process is clearer and easier to follow.

# The new things I added include:

	•	An HR Dashboard that shows the number of pending, approved and rejected claims.
	•	A Pending Claims page that displays all claims waiting for approval.
	•	A Details page where the reviewer can see the full claim info and take action.
	•	An Approval Log, so every time a claim is approved/rejected, it records who did it and when.
	•	Updated models and controllers to support the new features.


# How to Run the Project

	1.	Open the project in Visual Studio 2022.
	2.	Make sure the NuGet packages restore (Entity Framework Core, MVC, etc.).
	3.	Run the project using IIS Express.
	4.	The system uses EF Core, so the app is already set up to use a database.

Note: On the lab computer, the database didn’t always run properly (LocalDB issues), but the code and structure are all correct and ready for a proper environment.

# Main Files I Worked On

	•	ReviewController.cs — handles the approval logic and dashboard.
	•	HrDashboardViewModel.cs — used for the dashboard stats.
	•	ApprovalLog.cs — stores approval actions.
	•	Pending.cshtml, HrDashboard.cshtml, Details.cshtml — new UI pages for Part 3.


# What I Learned
This section helped me understand working with multiple views, linking models to controllers, and how to show data properly on the UI. It also pushed me to work with EF Core more and understand how to structure a proper MVC project.


# References

	•	Microsoft Docs – ASP.NET Core MVC
	•	Microsoft Docs – Entity Framework Core
	•	Class notes and Part 3 POE instructions
