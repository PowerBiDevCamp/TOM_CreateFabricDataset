# Creating DirectLake Datasets using Tabular Object Model (TOM)

This documents provides a code walkthrough of the C# console application porject named **TOM_CreateFabricDataset** to build your understanding of how to use the Tabular Object Model (TOM) to automate the creation of a DirectLake-mode dataset for Power BI.

This .NET project references the **Microsoft.AnalysisServices.NetCore.retail.amd64** assembly which provides public class for the Tabular Object Model in the **Microsoft.AnalysisServices.Tabular** namespace.s  

Let's start by reviewing the code used to create a new Power BI dataset named **CreateDatabase**. Note that you must use a **CompatibilityLevel** of **1604** or higher to create DirectLake-mode tables.

public static Database CreateDatabase(string DatabaseName) {

  // ensure new workspace name not already in use
  string newDatabaseName = server.Databases.GetNewName(DatabaseName);

  // create new database object (aka Dataset) with CompatibilityLevel >= 1604
  var database = new Database() {
    Name = newDatabaseName,
    ID = newDatabaseName,
    CompatibilityLevel = 1604,
    StorageEngineUsed = Microsoft.AnalysisServices.StorageEngineUsed.TabularMetadata,
    Model = new Model() {
      Name = DatabaseName + "-Model",
      Description = "A Demo Tabular data model with 1604 compatibility level."
    },
  };

  // add new dataset to target workspace
  server.Databases.Add(database);
  database.Update(Microsoft.AnalysisServices.UpdateOptions.ExpandFull);

  // return database object to caller
  return database;
}
```
Once you have created a new **Database** object for a Power BI dataset, you can then access the data model using the Model property. The first step in enabling the creation of DirectLake-mode tables is adding a **NamedExpression** object used to connect to the SQL endpoint of the Fabric lakehouse. Examine the following code which creates a new a **NamedExpression** object with a name of **DatabaseQuery**.

``` csharp
Database database = DatasetManager.CreateDatabase(DatabaseName);
      
Model model = database.Model;

// create named expression used to create DirectLake tables
model.Expressions.Add(new NamedExpression {
  Name = "DatabaseQuery",
  Kind = ExpressionKind.M,
  Expression = Properties.Resources.DatabaseQuery_m
                .Replace("{SQL_ENDPOINT}", AppSettings.SqlEndpoint)
                .Replace("{LAKEHOUSE_NAME}", AppSettings.TargetLakehouseName)
});

model.SaveChanges();
model.RequestRefresh(RefreshType.Full);

// retrieve named expression used to create DirectLake tables
NamedExpression sqlEndpoint = model.Expressions[0];
```
xxx
``` m
let
    database = Sql.Database("your_lakehouse_id.datawarehouse.pbidedicated.windows.net", "lakehouse_name")
in
    database

```
xxxx

``` csharp
 // retrieve named expression used to create DirectLake tables
 NamedExpression sqlEndpoint = model.Expressions[0];

 // pass named expression to functions creating DirectLake tables
 Table tableCustomers = CreateDirectLakeCustomersTable(sqlEndpoint);
 Table tableProducts = CreateDirectLakeProductsTable(sqlEndpoint);
 Table tableSales = CreateDirectLakeSalesTable(sqlEndpoint);
 Table tableCalendar = CreateDirectLakeCalendarTable(sqlEndpoint);
```
xxx

``` csharp
private static Table CreateDirectLakeProductsTable(NamedExpression sqlEndpoint) {
  
  // create table in DirectLake mode
  Table productsTable = new Table() {
    Name = "Products",
    Description = "Products table",
    Partitions = {
      new Partition() {
        Name = "All Products",
        Mode = ModeType.DirectLake,
        Source = new EntityPartitionSource() {
          EntityName = "products",
          ExpressionSource = sqlEndpoint,
          SchemaName = "dbo"
        }
      }
    },
    Columns = {
      new DataColumn() { Name = "ProductId", DataType = DataType.Int64, SourceColumn = "ProductId", IsHidden = true, IsKey=true },
      new DataColumn() { Name = "Product", DataType = DataType.String, SourceColumn = "Product" },
      new DataColumn() { Name = "Category", DataType = DataType.String, SourceColumn = "Category" }
    }
  };

  // add dimensional hierarchy
  productsTable.Hierarchies.Add(
    new Hierarchy() {
      Name = "Product Categories",
      Levels = {
          new Level() { Ordinal=0, Name="Category", Column=productsTable.Columns["Category"]  },
          new Level() { Ordinal=1, Name="Product", Column=productsTable.Columns["Product"] }
        }
    });

  // return table object to caller
  return productsTable;
}

```
xxx
``` csharp
# add code here
```
private static Table CreateDirectLakeProductsTable(NamedExpression sqlEndpoint)

``` csharp
new Partition() {
  Name = "All Products",
  Mode = ModeType.DirectLake,
  Source = new EntityPartitionSource() {
    EntityName = "products",
    ExpressionSource = sqlEndpoint,
    SchemaName = "dbo"
  }
}
```
xxx

``` csharp
# add code here
```
xxx

``` csharp
# add code here
```
xxx

``` csharp
# add code here
```
xxx

``` csharp
# add code here
```
xxx

``` csharp
# add code here
```
xxx

``` csharp
# add code here
```
xxx

``` csharp
# add code here
```
xxx

``` csharp
# add code here
```
