using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GCode3D.ViewModels
{
    public class StandardViewModel : INotifyPropertyChanged, IDisposable
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string info = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        protected bool Set<T>(ref T backingField, T value, [CallerMemberName]string propertyName = "")
        {
            if (object.Equals(backingField, value))
                return false;

            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
        #region IDisposable
        public virtual void Dispose() {}
        #endregion
    }
}