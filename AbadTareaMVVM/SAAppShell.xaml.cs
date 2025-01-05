namespace AbadTareaMVVM
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SAViews.SANotePage), typeof(SAViews.SANotePage));
        }
    }
}
