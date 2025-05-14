namespace ClientRegistryApp.Common.Helpers;

public static class LogHelper
{
    public static void TrackError(
        Exception exception,
        Dictionary<string, string> properties = null,
        string description = "")
    {
        var localProperties = new Dictionary<string, string>();

        var current = Connectivity.NetworkAccess;
        var profiles = Connectivity.ConnectionProfiles;

        switch (current)
        {
            case NetworkAccess.Internet:
                localProperties.Add("Internet", "On");
                break;
            case NetworkAccess.Local:
                localProperties.Add("Local", "On");
                break;
            case NetworkAccess.ConstrainedInternet:
                localProperties.Add("ConstrainedInternet", "On");
                break;
            case NetworkAccess.None:
                localProperties.Add("None", "On");
                break;
            case NetworkAccess.Unknown:
                localProperties.Add("Unknown", "On");
                break;
        }

        if (profiles != null)
        {
            if (profiles.Contains(ConnectionProfile.Unknown))
            {
                localProperties.Add("ConnectionProfile", "Unknown");
            }
            else if (profiles.Contains(ConnectionProfile.Bluetooth))
            {
                localProperties.Add("ConnectionProfile", "Bluetooth");
            }
            else if (profiles.Contains(ConnectionProfile.Cellular))
            {
                localProperties.Add("ConnectionProfile", "Cellular");
            }
            else if (profiles.Contains(ConnectionProfile.Ethernet))
            {
                localProperties.Add("ConnectionProfile", "Ethernet");
            }
            else if (profiles.Contains(ConnectionProfile.WiFi))
            {
                localProperties.Add("ConnectionProfile", "WiFi");
            }
        }

        if (!string.IsNullOrEmpty(description))
        {
            localProperties.Add("Adicional_Description", description);
        }

        if (properties != null)
        {
            foreach (var property in properties)
            {
                localProperties.Add(property.Key, property.Value);
            }
        }

        // Display information for those properties using Console.WriteLine
        Console.WriteLine("Exception: " + exception.ToString());
        Console.WriteLine("Extra properties: " + string.Join(", ", localProperties.Select(kvp => $"{kvp.Key}={kvp.Value}")));        
    }

    public static void TrackEvent(string eventName, Dictionary<string, string> properties = null)
    {
        Console.WriteLine($"TrackEvent: {eventName} {properties}");
    }
}
