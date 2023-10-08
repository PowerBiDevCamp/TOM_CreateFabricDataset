using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOM_CreateFabricDataset {

  class AppSettings {

    // public Azure AD application metadata for user authentication
    public const string ApplicationId = "";
    public const string RedirectUri = "http://localhost";

    // fill these in to run without interactive login
    public const string UserId = "user1@mydoamin.onmicrosoft.com";
    public const string UserPassword = "";

    public const string WorkspaceConnection = "powerbi://api.powerbi.com/v1.0/myorg/workspace123";

    public const string SqlEndpoint = "xyz" + 
                                      ".datawarehouse.pbidedicated.windows.net";

    public const string TargetLakehouseName = "lakehouse123";

    // confidential Azure AD application metadata for service principal authentication
    public const string TenantId = "";
    public const string ConfidentialApplicationId = "";
    public const string ConfidentialApplicationSecret = "";
    public const string TenantSpecificAuthority = "https://login.microsoftonline.com/" + TenantId;

  }

}
