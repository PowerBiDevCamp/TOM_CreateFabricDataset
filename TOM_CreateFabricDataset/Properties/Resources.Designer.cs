﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TOM_CreateFabricDataset.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TOM_CreateFabricDataset.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SWITCH (
        ///    TRUE(),
        ///    [Age] &gt;= 65, &quot;65 and over&quot;,
        ///    [Age] &gt;= 50, &quot;50 to 64&quot;,
        ///    [Age] &gt;= 40, &quot;40 to 49&quot;,
        ///    [Age] &gt;= 30, &quot;30 to 39&quot;,
        ///    [Age] &gt;= 18, &quot;18 to 29&quot;,
        ///    [Age] &lt; 18, &quot;Under 18&quot;
        ///).
        /// </summary>
        internal static string CalculatedColumn_AgeGroup_dax {
            get {
                return ResourceManager.GetString("CalculatedColumn-AgeGroup_dax", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SWITCH( 
        ///    TRUE(), 
        ///    [State] in {&quot;AK&quot;, &quot;AZ&quot;, &quot;CA&quot;, &quot;CO&quot;, &quot;HI&quot;, &quot;MT&quot;, &quot;NM&quot;, &quot;NV&quot;, &quot;OR&quot;, &quot;UT&quot;, &quot;WA&quot;},
        ///    &quot;Western Region&quot;,
        ///    [State] in {&quot;AL&quot;, &quot;AR&quot;, &quot;IA&quot;, &quot;ID&quot;, &quot;IL&quot;, &quot;IN&quot;, &quot;KS&quot;, &quot;KY&quot;, &quot;LA&quot;, &quot;MI&quot;, &quot;MN&quot;, &quot;MO&quot;, &quot;MS&quot;, &quot;ND&quot;, &quot;NE&quot;, &quot;OH&quot;, &quot;OK&quot;, &quot;SD&quot;, &quot;TN&quot;, &quot;TX&quot;, &quot;WI&quot;, &quot;WV&quot;, &quot;WY&quot;},
        ///    &quot;Central Region&quot;,
        ///    [State] in {&quot;CT&quot;, &quot;DE&quot;, &quot;FL&quot;, &quot;GA&quot;, &quot;MA&quot;, &quot;MD&quot;, &quot;ME&quot;, &quot;NC&quot;, &quot;NH&quot;, &quot;NJ&quot;, &quot;NY&quot;, &quot;PA&quot;, &quot;RI&quot;, &quot;SC&quot;, &quot;VA&quot;, &quot;VT&quot;},
        ///    &quot;Eastern Region&quot;
        ///).
        /// </summary>
        internal static string CalculatedColumn_SalesRegion_dax {
            get {
                return ResourceManager.GetString("CalculatedColumn-SalesRegion_dax", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SWITCH( 
        ///    TRUE(), 
        ///    [State] in {&quot;AK&quot;, &quot;AZ&quot;, &quot;CA&quot;, &quot;CO&quot;, &quot;HI&quot;, &quot;MT&quot;, &quot;NM&quot;, &quot;NV&quot;, &quot;OR&quot;, &quot;UT&quot;, &quot;WA&quot;},
        ///    1,
        ///    [State] in {&quot;AL&quot;, &quot;AR&quot;, &quot;IA&quot;, &quot;ID&quot;, &quot;IL&quot;, &quot;IN&quot;, &quot;KS&quot;, &quot;KY&quot;, &quot;LA&quot;, &quot;MI&quot;, &quot;MN&quot;, &quot;MO&quot;, &quot;MS&quot;, &quot;ND&quot;, &quot;NE&quot;, &quot;OH&quot;, &quot;OK&quot;, &quot;SD&quot;, &quot;TN&quot;, &quot;TX&quot;, &quot;WI&quot;, &quot;WV&quot;, &quot;WY&quot;},
        ///    2,
        ///    [State] in {&quot;CT&quot;, &quot;DE&quot;, &quot;FL&quot;, &quot;GA&quot;, &quot;MA&quot;, &quot;MD&quot;, &quot;ME&quot;, &quot;NC&quot;, &quot;NH&quot;, &quot;NJ&quot;, &quot;NY&quot;, &quot;PA&quot;, &quot;RI&quot;, &quot;SC&quot;, &quot;VA&quot;, &quot;VT&quot;},
        ///    3
        ///).
        /// </summary>
        internal static string CalculatedColumn_SalesRegionSort_dax {
            get {
                return ResourceManager.GetString("CalculatedColumn-SalesRegionSort_dax", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SWITCH( 
        ///    TRUE(), 
        ///    [State] in {&quot;AK&quot;, &quot;AZ&quot;, &quot;CA&quot;, &quot;CO&quot;, &quot;HI&quot;, &quot;MT&quot;, &quot;NM&quot;, &quot;NV&quot;, &quot;OR&quot;, &quot;UT&quot;, &quot;WA&quot;},
        ///    1,
        ///    [State] in {&quot;AL&quot;, &quot;AR&quot;, &quot;IA&quot;, &quot;ID&quot;, &quot;IL&quot;, &quot;IN&quot;, &quot;KS&quot;, &quot;KY&quot;, &quot;LA&quot;, &quot;MI&quot;, &quot;MN&quot;, &quot;MO&quot;, &quot;MS&quot;, &quot;ND&quot;, &quot;NE&quot;, &quot;OH&quot;, &quot;OK&quot;, &quot;SD&quot;, &quot;TN&quot;, &quot;TX&quot;, &quot;WI&quot;, &quot;WV&quot;, &quot;WY&quot;},
        ///    2,
        ///    [State] in {&quot;CT&quot;, &quot;DE&quot;, &quot;FL&quot;, &quot;GA&quot;, &quot;MA&quot;, &quot;MD&quot;, &quot;ME&quot;, &quot;NC&quot;, &quot;NH&quot;, &quot;NJ&quot;, &quot;NY&quot;, &quot;PA&quot;, &quot;RI&quot;, &quot;SC&quot;, &quot;VA&quot;, &quot;VT&quot;},
        ///    3
        ///).
        /// </summary>
        internal static string CalculatedColumn_SalesRegionSort_m {
            get {
                return ResourceManager.GetString("CalculatedColumn-SalesRegionSort_m", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Var CalenderStart = Date(Year(Min(Sales[InvoiceDate])) , 1, 1)  
        ///Var CalendarEnd = Date(Year(MAX(Sales[InvoiceDate])), 12, 31)
        ///Return
        ///  CALENDAR(CalenderStart, CalendarEnd)
        ///.
        /// </summary>
        internal static string CalculatedTable_Calendar_dax {
            get {
                return ResourceManager.GetString("CalculatedTable-Calendar_dax", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to let
        ///    Source = Sql.Database(&quot;devcamp.database.windows.net&quot;, &quot;WingtipSales&quot;),
        ///    dbo_Customers = Source{[Schema=&quot;dbo&quot;,Item=&quot;Customers&quot;]}[Data],
        ///    RemovedOtherColumns = Table.SelectColumns(dbo_Customers,{&quot;CustomerId&quot;, &quot;FirstName&quot;, &quot;LastName&quot;, &quot;City&quot;, &quot;State&quot;, &quot;Zipcode&quot;, &quot;Gender&quot;, &quot;BirthDate&quot;, &quot;FirstPurchaseDate&quot;, &quot;LastPurchaseDate&quot;}),
        ///    MergedColumns = Table.CombineColumns(RemovedOtherColumns,{&quot;FirstName&quot;, &quot;LastName&quot;},Combiner.CombineTextByDelimiter(&quot; &quot;, QuoteStyle.None),&quot;Customer&quot;),
        ///    ReplacedFemale [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CustomersQuery_m {
            get {
                return ResourceManager.GetString("CustomersQuery_m", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to let
        ///    database = Sql.Database(&quot;{SQL_ENDPOINT}&quot;, &quot;{LAKEHOUSE_NAME}&quot;)
        ///in
        ///    database.
        /// </summary>
        internal static string DatabaseQuery_m {
            get {
                return ResourceManager.GetString("DatabaseQuery_m", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to let
        ///    Source = Sql.Database(&quot;devcamp.database.windows.net&quot;, &quot;WingtipSales&quot;),
        ///    dbo_Products = Source{[Schema=&quot;dbo&quot;,Item=&quot;Products&quot;]}[Data],
        ///    RemovedOtherColumns = Table.SelectColumns(dbo_Products,{&quot;ProductId&quot;, &quot;Title&quot;, &quot;Description&quot;, &quot;ProductCategory&quot;, &quot;ProductImageUrl&quot;}),
        ///    RenamedColumns = Table.RenameColumns(RemovedOtherColumns,{{&quot;Title&quot;, &quot;Product&quot;}}),
        ///    SplitColumnByDelimiter = Table.SplitColumn(RenamedColumns, &quot;ProductCategory&quot;, Splitter.SplitTextByDelimiter(&quot; &gt; &quot;, QuoteStyle.Csv), {&quot;Product [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ProductQuery_m {
            get {
                return ResourceManager.GetString("ProductQuery_m", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EVALUATE
        ///  SUMMARIZECOLUMNS(
        ///    Customers[State],
        ///    &quot;Sales Revenue&quot;, SUM(Sales[SalesAmount]),
        ///    &quot;Units Sold&quot;, SUM(Sales[Quantity])
        ///).
        /// </summary>
        internal static string QueryGetSalesByState_dax {
            get {
                return ResourceManager.GetString("QueryGetSalesByState_dax", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to let
        ///    Source = Sql.Database(&quot;devcamp.database.windows.net&quot;, &quot;WingtipSales&quot;),
        ///    dbo_InvoiceDetails = Source{[Schema=&quot;dbo&quot;,Item=&quot;InvoiceDetails&quot;]}[Data],
        ///    ExpandedInvoices = Table.ExpandRecordColumn(dbo_InvoiceDetails, &quot;Invoices&quot;, {&quot;InvoiceDate&quot;, &quot;CustomerId&quot;}, {&quot;InvoiceDate&quot;, &quot;CustomerId&quot;}),
        ///    RemovedColumns = Table.RemoveColumns(ExpandedInvoices,{&quot;Products&quot;}),
        ///    ChangedType = Table.TransformColumnTypes(RemovedColumns,{{&quot;InvoiceDate&quot;, type date}, {&quot;SalesAmount&quot;, Currency.Type}})
        ///in
        ///    ChangedType.
        /// </summary>
        internal static string SalesQuery_m {
            get {
                return ResourceManager.GetString("SalesQuery_m", resourceCulture);
            }
        }
    }
}
