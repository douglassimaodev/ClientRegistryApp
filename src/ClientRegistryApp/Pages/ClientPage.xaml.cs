using ClientRegistryApp.PageModels;

namespace ClientRegistryApp.Pages;

public partial class ClientPage : ContentPage
{
    private readonly ClientPageModel _pageModel;

    public ClientPage(ClientPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = _pageModel = pageModel;
    }

    private void AgeEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = sender as Entry;
        if (entry == null)
            return;

        // Remove any non-digit characters
        string filtered = new string(entry.Text?.Where(char.IsDigit).ToArray());

        if (entry.Text != filtered)
            entry.Text = filtered;        
    }
}