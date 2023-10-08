# Using TOM to Create a DirectLake Dataset

This repository contains C# console application named
**TOM_CreateFabricDataset** which demonstrates how to create a
DirectLake data model for Fabric and Power BI using the Tabular Object
Model (TOM). This repository also contains a Fabric notebook named
**CreateLakehouseTables.ipynb** with Python code which must be used to
create tables in a Fabric Lakehouse that will be used as the underlying
datasource for the DirectLake data model.

Here are the high-level steps to completing this demonstration:

- Create workspace associated with Fabric capacity

- Create a new Lakehouse in the new workspace

- Create Lakehouse tables using a pre-provided Fabric notebook

- Run the custom C# application to create DirectLake data model using
  TOM

## Create workspace associated with Fabric capacity

Create a new workspace with a name such as **DirectLakeDemo**. Make sure
the workspace is associated with a Premium capacity or a trial capacity
with Fabric capabilities.

Get URL to Workspace Connection

<img src="./images/media/image1.png"
style="width:3.25in;height:2.41129in" />

The new

powerbi://api.powerbi.com/v1.0/myorg/DirectLakeDemo

## Create a new Lakehouse in the new workspace

Inside the new workspace, create a new Lakehouse named
**SalesDataLakehouse**.

<img src="./images/media/image2.png"
style="width:2.43333in;height:2.83265in" />

Ssss

<img src="./images/media/image3.png"
style="width:4.025in;height:2.14905in" />

Sss

<img src="./images/media/image4.png"
style="width:4.05in;height:1.45512in" />

Ssss

<img src="./images/media/image5.png"
style="width:3.08333in;height:1.88695in" />

Sssssss

<img src="./images/media/image6.png"
style="width:3.96911in;height:2.66667in" />

Get Lakehouse SQL Endpoint

<img src="./images/media/image7.png"
style="width:4.11702in;height:2.06685in"
alt="A screenshot of a computer Description automatically generated" />

Ssss

5lcsgl3vll3edero2m4sge7gdu-nya26urqtgsejoagwutwdoogl4**.datawarehouse.pbidedicated.windows.net**

## Create Lakehouse tables using a pre-provided Fabric notebook

Download all the sources files from this repository as a single ZIP
archive using [**this
link**](https://github.com/PowerBiDevCamp/TOM_CreateFabricDataset/archive/refs/heads/main.zip).
When you look inside the ZIP archive, you should see several files
inside. Extract the files into a local folder on your machine.

<img src="./images/media/image8.png"
style="width:4.3103in;height:2.78333in" />

Back to Fabric UI.

<img src="./images/media/image9.png"
style="width:4.13333in;height:2.8046in" />

Sssss

<img src="./images/media/image10.png"
style="width:2.325in;height:1.94116in" />

Aaaaa

<img src="./images/media/image11.png"
style="width:3.74167in;height:1.20283in" />

Upload Python notebook named **CreateLakehouseTables.ipynb**.

<img src="./images/media/image12.png"
style="width:4.19167in;height:2.23338in" />

### Associate the Fabric Notebook with the Lakehouse named SalesDataLakehouse

ddddd

<img src="./images/media/image13.png"
style="width:4.56667in;height:3.14435in" />

Associate notebook named **CreateLakehouseTables.ipynb** with Lakehouse

<img src="./images/media/image14.png"
style="width:2.83622in;height:1.75833in" />

Sss

<img src="./images/media/image15.png"
style="width:4.40833in;height:2.3243in" />

Ssss

<img src="./images/media/image16.png"
style="width:2.25in;height:2.21987in" />

### Copy CSV files from this repository into the file system of your Fabric Lakehouse

Execute code in notebook to copy CSV files from GitHib repository into
Lakehouse file system
``` python
import requests

csv_base_url = "https://github.com/PowerBiDevCamp/Python-In-Fabric-Notebooks/raw/main/ProductSalesData/"

csv_files = { "Customers.csv", "Products.csv", "Invoices.csv", "InvoiceDetails.csv" }

folder_path = "Files/landing_zone_sales/"

for csv_file in csv_files:
    csv_file_path = csv_base_url + csv_file
    with requests.get(csv_file_path) as response:
        csv_content = response.content.decode('utf-8-sig')
        mssparkutils.fs.put(folder_path + csv_file, csv_content, True)
        print(csv_file + " copied to Lakehouse file in OneLake")
```

xx

<img src="./images/media/image17.png"
style="width:4.5in;height:1.38654in" />

Sssss

<img src="./images/media/image18.png"
style="width:4.51667in;height:1.65611in" />

Sssss

<img src="./images/media/image19.png"
style="width:1.71667in;height:1.23488in" />

Sss

<img src="./images/media/image20.png"
style="width:2.22493in;height:1.56667in" />

Sss

<img src="./images/media/image21.png"
style="width:3.41667in;height:1.68389in" />

xxx

### Execute code in notebook to load CSV files into Spark DataFrames for the bronze layer

Examine the code

from pyspark.sql.types import StructType, StructField, StringType,
LongType, FloatType

\# creating a Spark DataFrame using schema defined using StructType and
StructField

schema_products = StructType(\[

StructField("ProductId", LongType() ),

StructField("Product", StringType() ),

StructField("Category", StringType() )

\])

df_products = (

spark.read.format("csv")

.option("header","true")

.schema(schema_products)

.load("Files/landing_zone_sales/Products.csv")

)

df_products.printSchema()

df_products.show()

ssssss

<img src="./images/media/image22.png"
style="width:1.875in;height:1.88291in" />

Xx

from pyspark.sql.types import StructType, StructField, StringType,
LongType, FloatType, DateType

\# creating a Spark DataFrame using schema defined with StructType and
StructField

schema_customers = StructType(\[

StructField("CustomerId", LongType() ),

StructField("FirstName", StringType() ),

StructField("LastName", StringType() ),

StructField("Country", StringType() ),

StructField("City", StringType() ),

StructField("DOB", DateType() ),

\])

df_customers = (

spark.read.format("csv")

.option("header","true")

.schema(schema_customers)

.option("dateFormat", "M/d/yyyy")

.option("inferSchema", "true")

.load("Files/landing_zone_sales/Customers.csv")

)

df_customers.printSchema()

df_customers.show()

xxx

<img src="./images/media/image23.png"
style="width:3.31667in;height:3.37826in" />

Xxx

from pyspark.sql.types import StructType, StructField, StringType,
LongType, FloatType, DateType

\# creating a Spark DataFrame using schema defined using StructType and
StructField

schema_invoices = StructType(\[

StructField("InvoiceId", LongType() ),

StructField("Date", DateType() ),

StructField("TotalSalesAmount", FloatType() ),

StructField("CustomerId", LongType() )

\])

df_invoices = (

spark.read.format("csv")

.option("header","true")

.schema(schema_invoices)

.option("dateFormat", "MM/dd/yyyy")

.option("inferSchema", "true")

.load("Files/landing_zone_sales/Invoices.csv")

)

df_invoices.printSchema()

df_invoices.show()

xx

<img src="./images/media/image24.png"
style="width:2.35in;height:2.90681in" />

Xx

from pyspark.sql.types import StructType, StructField, StringType,
LongType, FloatType, DateType

\# creating a Spark DataFrame using schema defined using StructType and
StructField

schema_invoice_details = StructType(\[

StructField("Id", LongType() ),

StructField("Quantity", LongType() ),

StructField("SalesAmount", FloatType() ),

StructField("InvoiceId", LongType() ),

StructField("ProductId", LongType() )

\])

df_invoice_details = (

spark.read.format("csv")

.option("header","true")

.schema(schema_invoice_details)

.option("dateFormat", "MM/dd/yyyy")

.option("inferSchema", "true")

.load("Files/landing_zone_sales/InvoiceDetails.csv")

)

df_invoice_details.printSchema()

df_invoice_details.show()

xxx

<img src="./images/media/image25.png"
style="width:2.30062in;height:3.11667in" />

### Execute code to Save the Four DataFrames as Delta Tables in the Lakehouse

Xxxx

\# save all bronze layer tables

df_products.write.mode("overwrite").format("delta").save(f"Tables/bronze_products")

df_customers.write.mode("overwrite").format("delta").save(f"Tables/bronze_customers")

df_invoices.write.mode("overwrite").
format("delta").save(f"Tables/bronze_invoices")

df_invoice_details.write.mode("overwrite")format("delta").save(f"Tables/bronze_invoice_details")

xxxx

<img src="./images/media/image26.png"
style="width:4in;height:1.96667in" />

Sssss

<img src="./images/media/image27.png"
style="width:2in;height:2.30252in" />

### Reshape and Transform Data in Bronze Layer Tables to Create Silver Layer Tables

Ssss

\# create silver layer products table

df_silver_products =
spark.read.format("delta").load("Tables/bronze_products")

df_silver_products.write.mode("overwrite").format("delta").save(f"Tables/products")

df_silver_products.printSchema()

df_silver_products.show()

cccc

<img src="./images/media/image28.png"
style="width:2.36667in;height:2.16536in" />

Xx

\# create silver layer customers table

from pyspark.sql.functions import concat_ws, floor, datediff,
current_date, col

df_silver_customers = (

spark.read.format("delta").load("Tables/bronze_customers")

.withColumn("Customer", concat_ws(' ', col('FirstName'),
col('LastName')) )

.withColumn("Age",( floor( datediff( current_date(), col("DOB")
)/365.25) ))

.drop('FirstName', 'LastName')

)

df_silver_customers.write.mode("overwrite").format("delta").save(f"Tables/customers")

df_silver_customers.printSchema()

df_silver_customers.show()

ssss

<img src="./images/media/image29.png"
style="width:3.21814in;height:3.375in" />

Xx

\# create silver layer sales table

from pyspark.sql.functions import col, desc, concat, lit, floor,
datediff

from pyspark.sql.functions import date_format, to_date, current_date,
year, month, dayofmonth

df_bronze_invoices =
spark.read.format("delta").load("Tables/bronze_invoices")

df_bronze_invoice_details =
spark.read.format("delta").load("Tables/bronze_invoice_details")

df_silver_sales = (

df_bronze_invoice_details

.join(df_bronze_invoices, df_bronze_invoice_details\['InvoiceId'\] ==
df_bronze_invoices\['InvoiceId'\])

.withColumnRenamed('SalesAmount', 'Sales')

.withColumn("DateKey", (year(col('Date'))\*10000) +

(month(col('Date'))\*100) +

(dayofmonth(col('Date'))) )

.drop('InvoiceId', 'TotalSalesAmount', 'InvoiceId', 'Id')

.select('Date', "DateKey", "CustomerId", "ProductId", "Sales",
"Quantity")

)

df_silver_sales.write.mode("overwrite").format("delta").save(f"Tables/sales")

df_silver_sales.printSchema()

df_silver_sales.show()

xxxx

<img src="./images/media/image30.png"
style="width:3.2392in;height:3.45in" />

Xx

\# create silver layer calendar table

import pandas as pd

from datetime import datetime, timedelta, date

import os

from pyspark.sql.functions import to_date, year, month, dayofmonth,
quarter, dayofweek

first_sales_date = df_silver_sales.agg({"Date":
"min"}).collect()\[0\]\[0\]

last_sales_date = df_silver_sales.agg({"Date":
"max"}).collect()\[0\]\[0\]

start_date = date(first_sales_date.year, 1, 1)

end_date = date(last_sales_date.year, 12, 31)

os.environ\["PYARROW_IGNORE_TIMEZONE"\] = "1"

df_calendar_ps = pd.date_range(start_date, end_date,
freq='D').to_frame()

df_calendar_spark = (

spark.createDataFrame(df_calendar_ps)

.withColumnRenamed("0", "timestamp")

.withColumn("Date", to_date(col('timestamp')))

.withColumn("DateKey", (year(col('timestamp'))\*10000) +

(month(col('timestamp'))\*100) +

(dayofmonth(col('timestamp'))) )

.withColumn("Year", year(col('timestamp')) )

.withColumn("Quarter", date_format(col('timestamp'),"yyyy-QQ") )

.withColumn("Month", date_format(col('timestamp'),'yyyy-MM') )

.withColumn("Day", dayofmonth(col('timestamp')) )

.withColumn("MonthInYear", date_format(col('timestamp'),'MMMM') )

.withColumn("MonthInYearSort", month(col('timestamp')) )

.withColumn("DayOfWeek", date_format(col('timestamp'),'EEEE') )

.withColumn("DayOfWeekSort", dayofweek(col('timestamp')))

.drop('timestamp')

)

df_calendar_spark.write.mode("overwrite").format("delta").save(f"Tables/calendar")

df_calendar_spark.printSchema()

df_calendar_spark.show()

xx

<img src="./images/media/image31.png"
style="width:6.76667in;height:5.59167in" />

Xx

<img src="./images/media/image32.png"
style="width:1.975in;height:2.25163in" />

### Inspect the tables that have been created in the Lakehouse

Xxxx

<img src="./images/media/image33.png"
style="width:3.01667in;height:1.5027in" />

Xxxx

<img src="./images/media/image34.png"
style="width:3.29866in;height:2.36667in" />

Xxx

<img src="./images/media/image35.png"
style="width:4.48745in;height:2.38333in" />

Dddd

<img src="./images/media/image36.png"
style="width:4.10833in;height:2.14785in" />

Xxxx

<img src="./images/media/image37.png"
style="width:5.23333in;height:1.5601in" />

Now all Lakehouse tables have been created and you can move on top the
step where you create the DirectLake dataset using the customer
application.

## Run the custom C# application to create DirectLake data model using TOM

- Create Azure AD application

  1.  Create a native/public application with redirect URI of
      <http://localhost>

  2.  Record Application ID for use in console application.

- Download C# console application source code and open project in Visual
  Studio 2022

- Open **AppSettings.cs** and updae the following:

  1.  ApplicationID of Azure AD application

  2.  Workspace Connection

- SQL Endpoint

  1.  Lakehouse Name

  2.  UserID and Password to prevent interactive login

  3.  Save changes

- Run application

  1.  It should run without error

  2.  When done, verify you can see new data model and use it to create
      new report
