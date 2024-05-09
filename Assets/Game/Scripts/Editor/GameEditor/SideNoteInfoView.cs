using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
public class SideNoteInfoView : VisualElement
{
    private IntegerField _timeMsField;
    private IntegerField _scoreField;
    private TextField _effectPathField;
    private TextField _soundPathField;
    private Vector2Field _positionField;
    private EnumField _directionField;
    private IntegerField _speedField;
    private IntegerField _durationMsField;

    private NoteUnit _sideNote;
    public NoteUnit SideNoteUnit
    {
        get => _sideNote;
        set
        {
            _sideNote = value;
            _timeMsField.value = _sideNote.TimeMs;
            _scoreField.value = _sideNote.Score;
            _effectPathField.value = _sideNote.EffectPath;
            _soundPathField.value = _sideNote.SoundPath;
            _positionField.value = _sideNote.Position;
            _directionField.value = _sideNote.Direction;
            _speedField.value = _sideNote.Speed;
            _durationMsField.value = _sideNote.DurationMs;
        }
    }
    public SideNoteInfoView()
    {
        // layout is horizontal
        style.flexDirection = FlexDirection.Row;
        // background is #FF9494
        style.backgroundColor = new StyleColor(new Color(1, 0.58f, 0.58f));

        // 宽度都为200,并且组件的label和区域都紧凑排列
        var width = 200;
        var labelWidth = 50;
        var labelColor = new StyleColor(Color.black);

        // time, 用于显示时间，只能是整数
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

        // position
        var positionLabel = new Label("Position");
        positionLabel.style.width = labelWidth;
        positionLabel.style.color = labelColor;
        Add(positionLabel);
        _positionField = new Vector2Field();
        _positionField.style.width = width - labelWidth;
        _positionField.style.color = labelColor;
        Add(_positionField);

        // direction
        var directionLabel = new Label("Direction");
        directionLabel.style.width = labelWidth;
        directionLabel.style.color = labelColor;
        Add(directionLabel);
        _directionField = new EnumField(NoteDirection.Left);
        _directionField.style.width = width - labelWidth;
        Add(_directionField);

        // speed
        var speedLabel = new Label("Speed");
        speedLabel.style.width = labelWidth;
        speedLabel.style.color = labelColor;
        Add(speedLabel);
        _speedField = new IntegerField();
        _speedField.style.width = width - labelWidth;
        Add(_speedField);

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
            _sideNote.TimeMs = evt.newValue;
        });
        _scoreField.RegisterValueChangedCallback(evt =>
        {
            _sideNote.Score = evt.newValue;
        });
        _effectPathField.RegisterValueChangedCallback(evt =>
        {
            _sideNote.EffectPath = evt.newValue;
        });
        _soundPathField.RegisterValueChangedCallback(evt =>
        {
            _sideNote.SoundPath = evt.newValue;
        });
        _positionField.RegisterValueChangedCallback(evt =>
        {
            _sideNote.Position = evt.newValue;
        });
        _directionField.RegisterValueChangedCallback(evt =>
        {
            _sideNote.Direction = (NoteDirection)evt.newValue;
        });
        _speedField.RegisterValueChangedCallback(evt =>
        {
            _sideNote.Speed = evt.newValue;
        });
        _durationMsField.RegisterValueChangedCallback(evt =>
        {
            _sideNote.DurationMs = evt.newValue;
        });
    }
}
