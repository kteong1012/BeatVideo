using Cysharp.Threading.Tasks;
using UnityEngine;

public class NoteClickEffect : MonoBehaviour
{
    public ResourcesGameObjectPool Pool { get; set; }
    private void Awake()
    {
        FadeOut().Forget();
    }

    private async UniTask FadeOut()
    {
        await UniTask.Delay(500);
        Pool.Release(gameObject);
    }
}
