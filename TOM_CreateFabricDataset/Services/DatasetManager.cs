using System.Text;
using Microsoft.AnalysisServices.Tabular;

namespace TOM_CreateFabricDataset.Services {

  class DatasetManager {

    public static Server server = new Server();

    public static void ConnectToPowerBIAsServicePrincipal() {
      string workspaceConnection = AppSettings.WorkspaceConnection;
      string tenantId = AppSettings.TenantId;
      string appId = AppSettings.ConfidentialApplicationId;
      string appSecret = AppSettings.ConfidentialApplicationSecret;
      string connectStringServicePrincipal = $"DataSource={workspaceConnection};User ID=app:{appId}@{tenantId};Password={appSecret};";
      server.Connect(connectStringServicePrincipal);
    }

    public static void ConnectToPowerBIAsUser() {
      string workspaceConnection = AppSettings.WorkspaceConnection;
      string userId = AppSettings.UserId;
      string password = AppSettings.UserPassword;
      string connectStringUser = $"DataSource={workspaceConnection};User ID={userId};Password={password};";
      server.Connect(connectStringUser);
    }

    static DatasetManager() {
      ConnectToPowerBIAsUser();
    }

    public static void CreateDirectLakeSalesModel(string DatabaseName) {

      Database database = DatasetManager.CreateDatabase(DatabaseName);

      Model model = database.Model;

      string sqlEndPoint = AppSettings.SqlEndpoint;
      string lakehouseName = AppSettings.TargetLakehouseName;

      string databaseQueryExpression = 
        Properties.Resources.DatabaseQuery_m
           .Replace("{SQL_ENDPOINT}", sqlEndPoint)
           .Replace("{LAKEHOUSE_NAME}", lakehouseName);

      model.Expressions.Add(new NamedExpression {
        Name = "DatabaseQuery",
        Kind = ExpressionKind.M,
        Expression = databaseQueryExpression
      });

      model.SaveChanges();
      model.RequestRefresh(RefreshType.Full);

      NamedExpression sqlEndpoint = model.Expressions[0];

      Table tableCustomers = CreateDirectLakeCustomersTable(sqlEndpoint);
      Table tableProducts = CreateDirectLakeProductsTable(sqlEndpoint);
      Table tableSales = CreateDirectLakeSalesTable(sqlEndpoint);
      Table tableCalendar = CreateDirectLakeCalendarTable(sqlEndpoint);

      model.Tables.Add(tableCustomers);
      model.Tables.Add(tableProducts);
      model.Tables.Add(tableSales);
      model.Tables.Add(tableCalendar);

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

    private static Table CreateDirectLakeCustomersTable(NamedExpression sqlEndpoint) {

      Table customersTable = new Table() {
        Name = "Customers",
        SourceLineageTag = "[dbo].[customers]",
        Annotations = {
          new Annotation{Name="IsTableInBiModel", Value="True"}
        },
        Partitions = {
          new Partition() {
            Name = "customers",
            Mode = ModeType.DirectLake,
            Source = new EntityPartitionSource() {
              EntityName = "customers",
              ExpressionSource = sqlEndpoint,
              SchemaName = "dbo"
            }
          }
        }
        ,
        Columns = {
          new DataColumn() { Name = "CustomerId", DataType = DataType.Int64, SourceColumn = "CustomerId", IsKey = true, IsHidden=true },
          new DataColumn() { Name = "Country", DataType = DataType.String, SourceColumn = "Country" },
          new DataColumn() { Name = "City", DataType = DataType.String, SourceColumn = "City" },
          new DataColumn() { Name = "Customer", DataType = DataType.String, SourceColumn = "Customer", },
          new DataColumn() { Name = "DOB", DataType = DataType.DateTime, SourceColumn = "DOB" },
          new DataColumn() { Name = "Age", DataType = DataType.Int64, SourceColumn = "Age", SummarizeBy=AggregateFunction.Average },
        }
      };

      customersTable.Hierarchies.Add(
        new Hierarchy() {
          Name = "Customer Geography",
          Levels = {
              new Level() { Ordinal=0, Name="Country", Column=customersTable.Columns["Country"]  },
              new Level() { Ordinal=1, Name="City", Column=customersTable.Columns["City"] }
            }
        });

      return customersTable;
    }

    private static Table CreateDirectLakeProductsTable(NamedExpression sqlEndpoint) {

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

      productsTable.Hierarchies.Add(
       new Hierarchy() {
         Name = "Product Categories",
         Levels = {
              new Level() { Ordinal=0, Name="Category", Column=productsTable.Columns["Category"]  },
              new Level() { Ordinal=1, Name="Product", Column=productsTable.Columns["Product"] }
           }
       });

      return productsTable;
    }

    private static Table CreateDirectLakeSalesTable(NamedExpression sqlEndpoint) {

      return new Table() {
        Name = "Sales",
        Description = "Sales table",
        Partitions = {
            new Partition() {
                Name = "sales",
                Mode = ModeType.DirectLake,
                Source = new EntityPartitionSource() {
                    EntityName = "sales",
                    ExpressionSource = sqlEndpoint,
                    SchemaName = "dbo"
                }
            }
      },
        Columns = {
        new DataColumn() { Name = "Date", DataType = DataType.DateTime, SourceColumn = "Date", IsHidden=true },
        new DataColumn() { Name = "DateKey", DataType = DataType.Int64, SourceColumn = "DateKey", IsHidden=true, IsKey=true },
        new DataColumn() { Name = "CustomerId", DataType = DataType.Int64, SourceColumn = "CustomerId", IsHidden=true},
        new DataColumn() { Name = "ProductId", DataType = DataType.Int64, SourceColumn = "ProductId", IsHidden=true  },
        new DataColumn() { Name = "Sales", DataType = DataType.Double, SourceColumn = "Sales", IsHidden=true},
        new DataColumn() { Name = "Quantity", DataType = DataType.Int64, SourceColumn = "Quantity", IsHidden=true }
      },
        Measures = {
        new Measure { Name = "Sales Revenue", Expression = "Sum(Sales[Sales])", FormatString=@"\$#,0;(\$#,0);\$#,0" },
        new Measure { Name = "Units Sold", Expression = "Sum(Sales[Quantity])", FormatString="#,0" },
        new Measure { Name = "Customer Count", Expression = "CountRows(Customers)", FormatString="#,0"  }
      }
      };
    }

    private static Table CreateDirectLakeCalendarTable(NamedExpression sqlEndpoint) {

      Table calendarTable = new Table() {
        Name = "Calendar",
        Description = "Calendar table",
        Partitions = {
            new Partition() {
                Name = "calendar",
                Mode = ModeType.DirectLake,
                Source = new EntityPartitionSource() {
                    EntityName = "calendar",
                    ExpressionSource = sqlEndpoint,
                    SchemaName = "dbo"
                }
            }
      },
        Columns = {
          new DataColumn() { Name = "Date", DataType = DataType.DateTime, SourceColumn = "Date", IsHidden=false },
          new DataColumn() { Name = "DateKey", DataType = DataType.Int64, SourceColumn = "DateKey", IsHidden=true, IsKey=true },
          new DataColumn() { Name = "Year", DataType = DataType.Int64, SourceColumn = "Year", IsHidden=false, SummarizeBy=AggregateFunction.None },
          new DataColumn() { Name = "Quarter", DataType = DataType.String, SourceColumn = "Quarter", IsHidden=false  },
          new DataColumn() { Name = "Month", DataType = DataType.String, SourceColumn = "Month", IsHidden=false  },
          new DataColumn() { Name = "MonthInYear", DataType = DataType.String, SourceColumn = "MonthInYear", IsHidden=false  },
          new DataColumn() { Name = "MonthInYearSort", DataType = DataType.Double, SourceColumn = "MonthInYearSort", IsHidden=true  },
          new DataColumn() { Name = "DayOfWeek", DataType = DataType.String, SourceColumn = "DayOfWeek", IsHidden=false  },
          new DataColumn() { Name = "DayOfWeekSort", DataType = DataType.Double, SourceColumn = "DayOfWeekSort", IsHidden=true  }
      }
      };

      calendarTable.Columns["MonthInYear"].SortByColumn = calendarTable.Columns["MonthInYearSort"];
      calendarTable.Columns["DayOfWeek"].SortByColumn = calendarTable.Columns["DayOfWeekSort"];

      calendarTable.Hierarchies.Add(
        new Hierarchy() {
          Name = "Calendar Drilldown",
          Levels = {
              new Level() { Ordinal=0, Name="Year", Column=calendarTable.Columns["Year"]  },
              new Level() { Ordinal=1, Name="Quarter", Column=calendarTable.Columns["Quarter"] },
              new Level() { Ordinal=2, Name="Month", Column=calendarTable.Columns["MonthInYear"] },
              new Level() { Ordinal=3, Name="Day", Column=calendarTable.Columns["Date"] }
         }
        });

      return calendarTable;
    }

    public static void ExportDatabasesToBim() {
      foreach (Database database in server.Databases) {

        // create file path in Documents folder
        string folderPathMyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string filePath = @"c:/temp/" + database.Name + ".model.bim";

        // convert model into JSON format for export
        string fileContent = JsonSerializer.SerializeDatabase(database, new SerializeOptions {
          IgnoreTimestamps = true,
          IgnoreInferredProperties = false,
          IgnoreInferredObjects = false,
          IncludeRestrictedInformation = true,
        });

        // create a file, write JSON content into it and then save
        StreamWriter writer = new StreamWriter(File.Open(filePath, FileMode.Create), Encoding.UTF8);
        writer.Write(fileContent);
        writer.Flush();
        writer.Dispose();

        // open model.bim file in Notepad
        //ProcessStartInfo startInfo = new ProcessStartInfo();
        //startInfo.FileName = "Notepad.exe";
        //startInfo.Arguments = filePath;
        //Process.Start(startInfo);


      }
    }
    
    public static Database CreateDatabase(string DatabaseName) {

      string newDatabaseName = server.Databases.GetNewName(DatabaseName);

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

      server.Databases.Add(database);
      database.Update(Microsoft.AnalysisServices.UpdateOptions.ExpandFull);

      return database;
    }

    public static Database CopyDatabase(string sourceDatabaseName, string DatabaseName) {

      Database sourceDatabase = server.Databases.GetByName(sourceDatabaseName);

      string newDatabaseName = server.Databases.GetNewName(DatabaseName);
      Database targetDatabase = CreateDatabase(newDatabaseName);
      sourceDatabase.Model.CopyTo(targetDatabase.Model);
      targetDatabase.Model.SaveChanges();

      targetDatabase.Model.RequestRefresh(RefreshType.Full);
      targetDatabase.Model.SaveChanges();

      return targetDatabase;
    }

  }
}