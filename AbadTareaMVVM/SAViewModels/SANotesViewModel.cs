using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AbadTareaMVVM.SAViewModels;

internal class SANotesViewModel : IQueryAttributable
{
    public ObservableCollection<SANoteViewModel> AllNotes { get; }
    public ICommand NewCommand { get; }
    public ICommand SelectNoteCommand { get; }

    public SANotesViewModel()
    {
        AllNotes = new ObservableCollection<SANoteViewModel>(SAModels.SANote.LoadAll().Select(n => new SANoteViewModel(n)));
        NewCommand = new AsyncRelayCommand(NewNoteAsync);
        SelectNoteCommand = new AsyncRelayCommand<SANoteViewModel>(SelectNoteAsync);
    }

    private async Task NewNoteAsync()
    {
        await Shell.Current.GoToAsync(nameof(SAViews.SANotePage));
    }

    private async Task SelectNoteAsync(SANoteViewModel note)
    {
        if (note != null)
            await Shell.Current.GoToAsync($"{nameof(SAViews.SANotePage)}?load={note.SAIdentifier}");
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("deleted"))
        {
            string noteId = query["deleted"].ToString();
            SANoteViewModel matchedNote = AllNotes.Where((n) => n.SAIdentifier == noteId).FirstOrDefault();

            // If note exists, delete it
            if (matchedNote != null)
                AllNotes.Remove(matchedNote);
        }
        else if (query.ContainsKey("saved"))
        {
            string noteId = query["saved"].ToString();
            SANoteViewModel matchedNote = AllNotes.Where((n) => n.SAIdentifier == noteId).FirstOrDefault();

            // If note is found, update it
            if (matchedNote != null)
            {
                matchedNote.Reload();
                AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
            }
            // If note isn't found, it's new; add it.
            else
                AllNotes.Insert(0, new SANoteViewModel(SAModels.SANote.Load(noteId)));
        }
    }
}
