using Cysharp.Threading.Tasks;

using Luban;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class GameUI : MonoBehaviour
{
    #region Serializable Fields
    public Text _countDownText;
    public VideoPlayer _videoPlayer;
    public Transform _noteRoot;
    #endregion

    #region Fields & Properties
    private LevelConfig _levelConfig;
    #endregion

    #region Magical Methods
    private void Start()
    {
        _levelConfig = Resources.Load<LevelConfig>("LevelConfig");
        StartGame(_levelConfig).Forget();
    }
    #endregion

    #region Public Methods
    public async UniTask StartGame(LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
        LoadVideo();
        await CountDown();
        PlayVideo();
        await ShowNotes();
    }
    #endregion

    #region Private Methods
    private void LoadVideo()
    {
        var videoClip = _levelConfig.Video;
        _videoPlayer.clip = videoClip;
        // Stop video
        _videoPlayer.Stop();

        // set self size
        var videoSize = new Vector2(videoClip.width, videoClip.height);
        var rectTransform = transform as RectTransform;
        rectTransform.sizeDelta = videoSize;
    }
    private async UniTask CountDown()
    {
        _countDownText.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            _countDownText.text = i.ToString();
            await UniTask.Delay(1000);
        }
        _countDownText.gameObject.SetActive(false);
    }
    private void PlayVideo()
    {
        _videoPlayer.Play();
    }
    private async UniTask ShowNotes()
    {
        var noteUnits = _levelConfig.Notes.OrderBy(note => note.TimeMs);
        var tasks = new List<UniTask>();

        var canvasSize = new Vector2(_videoPlayer.width, _videoPlayer.height);
        foreach (var noteUnit in noteUnits)
        {
            if (noteUnit.NoteType == NoteType.SideNote)
            {
                var creator = new SideNoteCreator();
                var task = UniTask.Create(() => creator.Create(noteUnit, _noteRoot, canvasSize));
                tasks.Add(task);
            }
            else if (noteUnit.NoteType == NoteType.TrackNote)
            {
                var creator = new TrackNoteCreator();
                var task = UniTask.Create(() => creator.Create(noteUnit, _noteRoot, canvasSize));
                tasks.Add(task);
            }
        }

        await UniTask.WhenAll(tasks);
    }
    #endregion
}
