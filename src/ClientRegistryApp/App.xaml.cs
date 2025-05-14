using ClientRegistryApp.Models;

namespace ClientRegistryApp
{
    public partial class App : Application
    {
        public static List<Client> Clients { get; set; } = new();

        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}