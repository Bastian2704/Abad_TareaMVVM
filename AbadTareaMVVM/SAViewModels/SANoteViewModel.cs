using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace AbadTareaMVVM.SAViewModels;

internal class SANoteViewModel : ObservableObject, IQueryAttributable
{
    private SAModels.SANote _note;

    public string SAText
    {
        get => _note.SAText;
        set
        {
            if (_note.SAText != value)
            {
                _note.SAText = value;
                OnPropertyChanged();
            }
        }
    }

    public DateTime SADate => _note.SADate;

    public string SAIdentifier => _note.SAFilename;

    public ICommand SASaveCommand { get; private set; }
    public ICommand SADeleteCommand { get; private set; }

    public SANoteViewModel()
    {
        _note = new SAModels.SANote();
        SASaveCommand = new AsyncRelayCommand(Save);
        SADeleteCommand = new AsyncRelayCommand(Delete);
    }

    public SANoteViewModel(SAModels.SANote note)
    {
        _note = note;
        SASaveCommand = new AsyncRelayCommand(Save);
        SADeleteCommand = new AsyncRelayCommand(Delete);
    }

    private async Task Save()
    {
        _note.SADate = DateTime.Now;
        _note.Save();
        await Shell.Current.GoToAsync($"..?saved={_note.SAFilename}");
    }

    private async Task Delete()
    {
        _note.Delete();
        await Shell.Current.GoToAsync($"..?deleted={_note.SAFilename}");
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("load"))
        {
            _note = SAModels.SANote.Load(query["load"].ToString());
            RefreshProperties();
        }
    }

    public void Reload()
    {
        _note = SAModels.SANote.Load(_note.SAFilename);
        RefreshProperties();
    }

    private void RefreshProperties()
    {
        OnPropertyChanged(nameof(SAText));
        OnPropertyChanged(nameof(SADate));
    }
}
