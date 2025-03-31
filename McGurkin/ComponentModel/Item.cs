using McGurkin.Runtime.Serialization;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace McGurkin.ComponentModel;

public interface IItem
{
    Guid Id { get; set; }
}

[JsonDerivedType(typeof(Item), typeDiscriminator: "base")]
[System.Diagnostics.DebuggerStepThrough()]
public partial class Item(Guid id) : NotifyPropertyChanged, IEditableObject, IItem
{
    private bool isEditing = false;

    private object? _Backup;
    public object? Backup
    {
        get
        {
            return _Backup;
        }
    }

    public virtual void BeginEdit()
    {
        if (!isEditing)
        {
            _Backup = Serializer.Copy<object>(this);
            isEditing = true;
        }
    }

    public virtual void CancelEdit()
    {
        // This class will handle backup,
        // derived class responible for CancelEdit()
        throw new NotImplementedException();
    }

    public virtual void EndEdit()
    {
        if (this.isEditing)
        {
            this._Backup = null;
            this.isEditing = false;
        }
    }

    private Guid _Id = id;
    public Guid Id
    {
        get => _Id;
        set
        {
            if (value != _Id)
            {
                _Id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }
    }

    public Item() : this(Guid.NewGuid()) { }
}
