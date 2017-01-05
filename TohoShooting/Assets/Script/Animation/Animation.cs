using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Animation : MonoBehaviour {

    public Sprite[] clip;   // アニメーション用のスプライト
    public float interval;  // スプライトを切り替える間隔
    public bool isLoop;     // アニメーションループさせるかどうか
    public bool destroyOnFinished;  // アニメーション終了時に消滅するかどうか

    public delegate void OnFinished();
    public OnFinished onFinished;   // アニメーション終了通知イベント

    private SpriteRenderer spriteRenderer;  // スプライトレンダラ
    private int currentClip;    // 現在のアニメーションクリップのインデックス

    // 必要なコンポーネントの参照を取得する。
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (destroyOnFinished)
        {
            onFinished += () => { GameObject.Destroy(this.gameObject); };
        }
    }

    // アニメーションを開始する。
    IEnumerator Start()
    {
        currentClip = 0;

        while (true)
        {
            // 一定時間待機
            yield return new WaitForSeconds(interval);

            // インデックス更新
            ++currentClip;
            if (currentClip >= clip.Length)
            {
                if (isLoop)
                {
                    // ループするのでインデックスを先頭に戻す
                    currentClip = 0;
                }
                else
                {
                    // ループしないのでアニメーション終了
                    break;
                }
            }

            // スプライト切り替え
            spriteRenderer.sprite = clip[currentClip];
        }


        // 終了通知イベント実行
        if (onFinished != null)
        {
            onFinished();
        }
    }

}
