using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class TrackNoteInfoView : VisualElement
{
    private IntegerField _timeMsField;
    private IntegerField _scoreField;
    private TextField _effectPathField;
    private TextField _soundPathField;
    private Vector2Field _startPositionField;
    private Vector2Field _endPositionField;
    private IntegerField _countField;
    private IntegerField _durationMsField;

    private NoteUnit _trackNote;
    public NoteUnit TrackNoteUnit
    {
        get => _trackNote;
        set
        {
            _trackNote = value;
            _timeMsField.value = _trackNote.TimeMs;
            _scoreField.value = _trackNote.Score;
            _effectPathField.value = _trackNote.EffectPath;
            _soundPathField.value = _trackNote.SoundPath;
            _startPositionField.value = _trackNote.StartPosition;
            _endPositionField.value = _trackNote.EndPosition;
            _countField.value = _trackNote.Count;
            _durationMsField.value = _trackNote.DurationMs;
        }
    }

    public TrackNoteInfoView()
    {
        // layout is horizontal
        style.flexDirection = FlexDirection.Row;
        // background is #D1BF23
        style.backgroundColor = new StyleColor(new Color(0.82f, 0.75f, 0.14f));

        // 宽度都为200,并且组件的label和区域都紧凑排列
        var width = 200;
        var labelWidth = 50;
        var labelColor = new StyleColor(Color.black);

        // time, used to display time, can only be an integer
        var timeLabel = new Label("Time");
        timeLabel.style.width = labelWidth;
        timeLabel.style.color = labelColor;
        Add(timeLabel);
        _timeMsField = new IntegerField();
        _timeMsField.style.width = width - labelWidth;
        Add(_timeMsField);

        // score
        var scoreLabel = new Label("Score");
        scoreLabel.style.width = labelWidth;
        scoreLabel.style.color = labelColor;
        Add(scoreLabel);
        _scoreField = new IntegerField();
        _scoreField.style.width = width - labelWidth;
        Add(_scoreField);

        // effect
        var effectLabel = new Label("Effect");
        effectLabel.style.width = labelWidth;
        effectLabel.style.color = labelColor;
        Add(effectLabel);
        _effectPathField = new TextField();
        _effectPathField.style.width = width - labelWidth;
        Add(_effectPathField);

        // sound
        var soundLabel = new Label("Sound");
        soundLabel.style.width = labelWidth;
        soundLabel.style.color = labelColor;
        Add(soundLabel);
        _soundPathField = new TextField();
        _soundPathField.style.width = width - labelWidth;
        Add(_soundPathField);

        // start position
        var startPositionLabel = new Label("Start Pos");
        startPositionLabel.style.width = labelWidth;
        startPositionLabel.style.color = labelColor;
        Add(startPositionLabel);
        _startPositionField = new Vector2Field();
        _startPositionField.style.width = width - labelWidth;
        Add(_startPositionField);

        // end position
        var endPositionLabel = new Label("End Pos");
        endPositionLabel.style.width = labelWidth;
        endPositionLabel.style.color = labelColor;
        Add(endPositionLabel);
        _endPositionField = new Vector2Field();
        _endPositionField.style.width = width - labelWidth;
        Add(_endPositionField);

        // count
        var countLabel = new Label("Count");
        countLabel.style.width = labelWidth;
        countLabel.style.color = labelColor;
        Add(countLabel);
        _countField = new IntegerField();
        _countField.style.width = width - labelWidth;
        Add(_countField);

        // duration
        var durationLabel = new Label("Duration");
        durationLabel.style.width = labelWidth;
        durationLabel.style.color = labelColor;
        Add(durationLabel);
        _durationMsField = new IntegerField();
        _durationMsField.style.width = width - labelWidth;
        Add(_durationMsField);

        // listen to value change
        _timeMsField.RegisterValueChangedCallback(evt =>
        {
            _trackNote.TimeMs = evt.newValue;
        });
        _scoreField.RegisterValueChangedCallback(evt =>
        {
            _trackNote.Score = evt.newValue;
        });
        _effectPathField.RegisterValueChangedCallback(evt =>
        {
            _trackNote.EffectPath = evt.newValue;
        });
        _soundPathField.RegisterValueChangedCallback(evt =>
        {
            _trackNote.SoundPath = evt.newValue;
        });
        _startPositionField.RegisterValueChangedCallback(evt =>
        {
            _trackNote.StartPosition = evt.newValue;
        });
        _endPositionField.RegisterValueChangedCallback(evt =>
        {
            _trackNote.EndPosition = evt.newValue;
        });
        _countField.RegisterValueChangedCallback(evt =>
        {
            _trackNote.Count = evt.newValue;
        });
        _durationMsField.RegisterValueChangedCallback(evt =>
        {
            _trackNote.DurationMs = evt.newValue;
        });
    }
}
