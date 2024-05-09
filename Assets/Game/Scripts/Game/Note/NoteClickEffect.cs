using Cysharp.Threading.Tasks;
using UnityEngine;

public class NoteClickEffect : MonoBehaviour
{
    private void Awake()
    {
        FadeOut().Forget();
    }

    private async UniTask FadeOut()
    {
        await UniTask.Delay(500);
    }
}
