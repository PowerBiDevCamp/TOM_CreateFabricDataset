# Creating DirectLake Datasets using Tabular Object Model (TOM)

This documents provides a code walkthrough for the C# console application oorject named **TOM_CreateFabricDataset** to build your understanding of how to use the Tabular Object Model (TOM) to automate the creation of a DirectLake-mode dataset for Power BI.

The Visual Studio project named **TOM_CreateFabricDataset** references the **Microsoft.AnalysisServices.NetCore.retail.amd64** assembly which provides public classes for the Tabular Object Model in the **Microsoft.AnalysisServices.Tabular** namespaces.  

Let's start by reviewing the code used to create a new Power BI dataset named **CreateDatabase**. Note that you must use a **CompatibilityLevel** of **1604** or higher to create DirectLake-mode tables.

``` python
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
Once you have created a new **Database** object for a Power BI dataset, you can then access thedataset (i.e data model) using the **Model** property. The first step in enabling the creation of DirectLake-mode tables is adding a **NamedExpression** object used to connect to the SQL endpoint of the Fabric lakehouse. Examine the following code which creates a new a **NamedExpression** object with a name of **DatabaseQuery**.

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
The following code listing shows what the actual M expresssion looks like. This M code calls the **Sql.Database** function passing the connection string for the SQL endpoint of the lakehouse and the name of the lakehouse.   
``` m
let
    database = Sql.Database("your_lakehouse_id.datawarehouse.pbidedicated.windows.net", "lakehouse_name")
in
    database

```
Once this **NamedExpression** has been created, it can be passed as a parameter to each of the methods in the **DatasetManagwer** class that creates a DirectLake-mode table.

``` csharp
 // retrieve named expression used to create DirectLake tables
 NamedExpression sqlEndpoint = model.Expressions[0];

 // pass named expression to functions creating DirectLake tables
 Table tableProducts = CreateDirectLakeProductsTable(sqlEndpoint);
 Table tableCustomers = CreateDirectLakeCustomersTable(sqlEndpoint);
 Table tableSales = CreateDirectLakeSalesTable(sqlEndpoint);
 Table tableCalendar = CreateDirectLakeCalendarTable(sqlEndpoint);
```
Examine the C# code for the **CreateDirectLakeProductsTable** method which create the **Products** table.  

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
      new DataColumn() { Name = "ProductId", DataType = DataType.Int64, SourceColumn = "ProductId", IsHidden = true },
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
As you can see, the **CreateDirectLakeProductsTable** method accepts a parameter of type **NamedExpression** named **SqlEndpoint**. 
``` csharp
private static Table CreateDirectLakeProductsTable(NamedExpression sqlEndpoint)
```
Below in the implementation of the **CreateDirectLakeProductsTable** method, there is code which creates a new **Partition** object with a **Mode** property setting of **ModeType.DirectLake**. One more requirement in creating the new **Partition** object is initilized the **Source** property with a new **EntityPartitionSource** object. The **EntityPartitionSource** object has an **EntityName** with the lakehouse table name and an **ExpressionSource** property with is intialized with the **SqlEndpoint** parameter. 
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
Now you can review the entire implementation of the **CreateDirectLakeSalesModel** method which contains the top-level logic to create a complete DirectLake-mode dataset for Power BI.

``` csharp
public static void CreateDirectLakeSalesModel(string DatabaseName) {

  Database database = DatasetManager.CreateDatabase(DatabaseName);
      
  Model model = database.Model;

  // create get named expression used to create DirectLake tables
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

  // pass named expression to functions creating DirectLake tables
  Table tableCustomers = CreateDirectLakeCustomersTable(sqlEndpoint);
  Table tableProducts = CreateDirectLakeProductsTable(sqlEndpoint);
  Table tableSales = CreateDirectLakeSalesTable(sqlEndpoint);
  Table tableCalendar = CreateDirectLakeCalendarTable(sqlEndpoint);

  // add DirectLake tables to data model
  model.Tables.Add(tableCustomers);
  model.Tables.Add(tableProducts);
  model.Tables.Add(tableSales);
  model.Tables.Add(tableCalendar);

  // create table relationship
  model.Relationships.Add(new SingleColumnRelationship {
    Name = "Customers to Sales",
    ToColumn = tableCustomers.Columns["CustomerId"],
    ToCardinality = RelationshipEndCardinality.One,
    FromColumn = tableSales.Columns["CustomerId"],
    FromCardinality = RelationshipEndCardinality.Many
  });

  model.Relationships.Add(new SingleColumnRelationship {
    Name = "Products to Sales",
    ToColumn = tableProducts.Columns["ProductId"],
    ToCardinality = RelationshipEndCardinality.One,
    FromColumn = tableSales.Columns["ProductId"],
    FromCardinality = RelationshipEndCardinality.Many
  });

  model.Relationships.Add(new SingleColumnRelationship {
    Name = "Calendar to Sales",
    ToColumn = tableCalendar.Columns["DateKey"],
    ToCardinality = RelationshipEndCardinality.One,
    FromColumn = tableSales.Columns["DateKey"],
    FromCardinality = RelationshipEndCardinality.Many
  });

  model.SaveChanges();
  model.RequestRefresh(RefreshType.Full);
  model.SaveChanges();

}
```
If you want to walk through the steps to set up and run the demo, you should look at **[README.md](README.md)**.
