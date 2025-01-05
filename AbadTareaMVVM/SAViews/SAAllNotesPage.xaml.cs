namespace AbadTareaMVVM.SAViews;

public partial class SAAllNotesPage : ContentPage
{
    public SAAllNotesPage()
    {
        InitializeComponent();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        notesCollection.SelectedItem = null;
    }
}