using System.ComponentModel;
using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace SmallMenu.Configuration;

internal class Settings : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public virtual float Scale { get; set; } = 1;
    public virtual float PosY { get; set; } = 0f;

    internal void RaisePropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}