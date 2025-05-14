namespace ClientRegistryApp.Common.Helpers;

public static class ColorHelper
{
    public static Color Red()
    {
        return new Color(255, 0, 0);
    }

    public static Color Danger()
    {
        var ColorResource = Application.Current?.Resources.MergedDictionaries.FirstOrDefault();

        if (ColorResource == null)
            return Color.FromArgb("#C64840");

        return ColorResource["Danger"] as Color;
    }

    public static Color Black()
    {
        return new Color();
    }

    public static Color LightGreen()
    {
        var ColorResource = Application.Current?.Resources.MergedDictionaries.FirstOrDefault();

        if (ColorResource == null)
            return Color.FromArgb("#00B28E");

        return ColorResource["LightGreen"] as Color;
    }

    public static Color LightGray()
    {
        return new Color(211, 211, 211);
    }

    public static Color White()
    {
        return new Color(255, 255, 255);
    }
}
