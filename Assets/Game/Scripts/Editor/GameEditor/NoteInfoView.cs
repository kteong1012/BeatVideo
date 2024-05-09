using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
public class NoteInfoView : GraphElement
{
    private NoteType _type;
    public NoteType Type
    {
        get { return _type; }
        set
        {
            _type = value;
            _typeField.value = _type;
        }
    }
    public Action<NoteType> OnTypeChanged;
    private EnumField _typeField;

    public NoteInfoView()
    {
        // layout is horizontal
        style.flexDirection = FlexDirection.Row;

        // dropdown
        var typeLabel = new Label("Type");
        typeLabel.style.width = 50;
        Add(typeLabel);
        _typeField = new EnumField(NoteType.SideNote);
        _typeField.style.width = 150;
        _typeField.RegisterValueChangedCallback(evt =>
        {
            var newType = (NoteType)evt.newValue;
            if (_type != newType)
            {
                OnTypeChanged?.Invoke(newType);
            }
        });
        Add(_typeField);
    }
}
