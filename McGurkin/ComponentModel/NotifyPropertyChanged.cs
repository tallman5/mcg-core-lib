using System.ComponentModel;
using System.Reflection;

namespace McGurkin.ComponentModel
{
    [System.Diagnostics.DebuggerStepThrough()]
    public partial class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual void RaisePropertyChanged()
        {
            // Raise property changed for all properties
            // Helpful when control bound to calculated properties with no set

            var t = GetType();
            TypeInfo ti = t.GetTypeInfo();
            foreach (var prop in ti.DeclaredProperties)
                this.RaisePropertyChanged(prop.Name);
        }

        public virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
