using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class LevelEditorView : GraphView
{
    private List<NoteUnit> _noteUnits = new List<NoteUnit>();
    private ListView _list;
    private ObjectField _configField;
    private ObjectField _videoField;

    public LevelEditorView()
    {
        // toolbar
        var toolbar = new Toolbar();
        // green save button
        var saveButton = new Button(() =>
        {
            SaveConfig();
        });
        saveButton.text = "Save";
        saveButton.style.backgroundColor = new StyleColor(Color.green);
        saveButton.style.color = new StyleColor(Color.black);
        toolbar.Add(saveButton);
        Add(toolbar);

        _configField = new ObjectField();
        _configField.objectType = typeof(LevelConfig);
        _configField.SetEnabled(false);
        Add(_configField);

        _videoField = new ObjectField();
        _videoField.objectType = typeof(VideoClip);
        Add(_videoField);

        // horizontal area for buttons
        var buttonArea = new VisualElement();
        buttonArea.style.flexDirection = FlexDirection.Row;
        // add button
        var addButton = new Button(() =>
        {
            var noteUnit = new NoteUnit();
            _noteUnits.Add(noteUnit);
            _list.Rebuild();
        });
        addButton.text = "+";
        buttonArea.Add(addButton);
        // remove button
        var removeButton = new Button(() =>
        {
            if (_list.selectedIndex >= 0)
            {
                _noteUnits.RemoveAt(_list.selectedIndex);
                _list.Rebuild();
            }
        });
        removeButton.text = "-";
        buttonArea.Add(removeButton);
        Add(buttonArea);

        // _list
        _list = new ListView();
        _list.makeItem = OnMakeItem;
        _list.bindItem = OnBindItem;
        _list.itemsSource = _noteUnits;
        Add(_list);

        LoadConfig();
    }

    private void LoadConfig()
    {
        // set config value
        var configResPath = "LevelConfig";
        var levelConfig = Resources.Load<LevelConfig>(configResPath);
        if (levelConfig == null)
        {
            // create a new asset to configResPath
            var newLevelConfig = ScriptableObject.CreateInstance<LevelConfig>();
            AssetDatabase.CreateAsset(newLevelConfig, $"Assets/Game/Resources/{configResPath}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        _configField.value = levelConfig;

        _videoField.value = levelConfig.Video;
        _noteUnits = new List<NoteUnit>(levelConfig.Notes);


        _list.itemsSource = _noteUnits;
        _list.Rebuild();
    }

    private void SaveConfig()
    {
        var levelConfig = _configField.value as LevelConfig;

        levelConfig.Video = _videoField.value as VideoClip;
        levelConfig.Notes = _noteUnits.ToArray();

        EditorUtility.SetDirty(levelConfig);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private VisualElement OnMakeItem()
    {
        return new NoteInfoView();
    }

    private void OnBindItem(VisualElement element, int index)
    {
        var noteUnit = _noteUnits[index];
        var noteInfoView = element as NoteInfoView;
        noteInfoView.Type = noteUnit.NoteType;
        if (noteUnit.NoteType == NoteType.SideNote)
        {
            var sideNoteInfoView = new SideNoteInfoView();
            sideNoteInfoView.SideNoteUnit = noteUnit;
            element.Add(sideNoteInfoView);
        }
        else if (noteUnit.NoteType == NoteType.TrackNote)
        {
            var trackNoteInfoView = new TrackNoteInfoView();
            trackNoteInfoView.TrackNoteUnit = noteUnit;
            element.Add(trackNoteInfoView);
        }
        noteInfoView.OnTypeChanged = type =>
        {
            noteUnit.NoteType = type;
            _list.Rebuild();
        };
    }
}
