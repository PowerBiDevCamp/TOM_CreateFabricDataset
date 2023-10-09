namespace TOM_CreateFabricDataset {

  class AppSettings {

    // fill in this configuration data before running demo application
    public const string WorkspaceConnection = "";
    public const string SqlEndpoint = "";
    public const string TargetLakehouseName = "";
    public const string ApplicationId = "";
    public const string RedirectUri = "http://localhost";

    // optionally add credentials to authenticate without requiring interactive login
    public const string UserId = "";
    public const string UserPassword = "";

    // confidential application metadata for service principal authentication not currently used in this demo
    public const string TenantId = "";
    public const string ConfidentialApplicationId = "";
    public const string ConfidentialApplicationSecret = "";
    public const string TenantSpecificAuthority = "https://login.microsoftonline.com/" + TenantId;

  }
}

