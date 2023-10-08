# Using TOM to Create a DirectLake Dataset

This repository contains a Fabric notebook with Python code and a C#
console application which can be used to create a DirectLake data model
using the Tabular Object Model (TOM).

Steps to completing this demonstration:

1.  Create workspace associated with Fabric capacity

2.  Get URL to Workspace Connection

3.  Write down workspace name

4.  Create Lakehouse

5.  Get Lakehouse SQL Endpoint

6.  Create Lakehouse tables using Pythin code in pre-provided Python
    notebook

7.  Upload Python notebook named **CreateLakehouseTables.ipynb**

8.  Associate notebook named **CreateLakehouseTables.ipynb** with
    Lakehouse

9.  Execute code in notebook to copy CSV files from GitHib repository
    into Lakehouse file system

10. Execute code in notebook to load CSV files and convert then into
    delta tables for bronze zone

11. Execute code in notebook to load bronze tables and reshape/transform
    data in into delta tables for silver layer

12. Execute code in notebook to generate calendar table for silver layer

13. Run C# console application to create Power BI DirectLake dataset
    using Tabular Object Model (TOM)

14. Create Azure AD application

15. Create a native/public application with redirect URI of
    <http://localhost>

16. Record Application ID for use in console application.

17. Download C# console application source code and open project in
    Visual Studio 2022

18. Open **AppSettings.cs** and updae the following:

19. ApplicationID of Azure AD application

20. Workspace Connection

21. SQL Endpoint

22. Lakehouse Name

23. UserID and Password to prevent interactive login

24. Save changes

25. Run application

26. It should run without error

27. When done, verify you can see new data model and use it to create
    new report
