using UnityEngine;
using System.Collections;

public class GrobalData : MonoBehaviour {

    //======================================
    // ここから Singleton 設定
    //======================================

    // 共有インスタンス
    public static GrobalData Instance;

    // ゾーンを移動した際にオブジェクトが生成された際に呼び出される
    void Awake()
    {

        // 共有インスタンスが存在しているかどうかのチェック
        if (Instance == null)
        {

            // ロードされた際にゲームオブジェクトを破棄しない
            DontDestroyOnLoad(gameObject);

            // 共有インスタンスに自身を設定
            Instance = this;

        }
        else if (Instance != this)
        {

            // 共有インスタンスが自身でなければゲームオブジェクトを破棄
            Destroy(gameObject);
        }
    }

    //======================================
    // ここまで Singleton 設定
    //======================================

    //======================================
    // ここからゲーム全体で共有したいメンバの宣言
    //======================================

    // ゲーム開始かどうか true...ゲームが開始している
    bool m_initFlag = false;

    // 最高スコア
    public int m_highScore = 0;
    // 現在のスコア
    public int m_score = 0;
    // 残機数
    public int m_stock = 5;

    // ボムの数
    public int m_bomb = 2;
    // パワー
    public int m_power = 0;
    // グレイズ
    //public int m_graze = 0;

    // プレイヤーの移動速度
    public float m_moveSpeed = 5.0f;

    // 難易度
    public int difficulty = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
