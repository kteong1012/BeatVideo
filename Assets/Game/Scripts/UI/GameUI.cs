using Cysharp.Threading.Tasks;
using Game.Cfg;
using Game.Cfg.Game;
using Luban;
using System.Collections;
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
    private Game.Cfg.LevelConfig _levelConfig;
    #endregion

    #region Magical Methods
    private async void Start()
    {
        var tables = new Tables();
        await tables.LoadAll(async (tableName) =>
        {
            await UniTask.CompletedTask;
            var bytes = Resources.Load<TextAsset>($"Config/Bytes/{tableName}").bytes;
            return new ByteBuf(bytes);
        });
        StartGame(tables.TbLevelConfig.GetOrDefault(1)).Forget();
    }
    #endregion

    #region Public Methods
    public async UniTask StartGame(Game.Cfg.LevelConfig levelConfig)
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
        var videoPath = _levelConfig.VideoPath;
        // Load video from resources path
        var videoClip = Resources.Load<VideoClip>(videoPath);
        _videoPlayer.clip = videoClip;
        // Stop video
        _videoPlayer.Stop();
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

        foreach (var noteUnit in noteUnits)
        {
            if (noteUnit is SideNoteUnit sideNoteUnit)
            {
                var creator = new SideNoteCreator();
                var task = UniTask.Create(() => creator.Create(sideNoteUnit, _noteRoot));
                tasks.Add(task);
            }
            else if (noteUnit is TrackNoteUnit trackNoteUnit)
            {
                var creator = new TrackNoteCreator();
                var task = UniTask.Create(() => creator.Create(trackNoteUnit, _noteRoot));
                tasks.Add(task);
            }
        }

        await UniTask.WhenAll(tasks);
    }
    #endregion
}
