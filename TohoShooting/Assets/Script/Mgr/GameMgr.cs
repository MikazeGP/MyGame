using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMgr : Origin {

    // UIテキスト
    public Text tFPS,tHiScore,tScore,tStock,tPower,tGraze,tBossName,tTimer;
    // UIスライダー
    public Slider sHPVer;
    // UIメソッド
    public int highScore, score, stock, bomb, power,graze,bossHp;
    // ボス出現フラグ
    public bool encountBoss;

    public Timer timer = new Timer();
    

	// Use this for initialization
	void Start () {
        // オブジェクトを作成
        Enemy.parent = new OriginMgr<Enemy>("Enemy/enemy_0", 128);
        Bullet.parent = new OriginMgr<Bullet>("Bullet/EneBul/Enebul", 512);
        EnemyDieEffect.parent = new OriginMgr<EnemyDieEffect>("Effect/eff_deadcircle",64);
        PlayerDieEffect.parent = new OriginMgr<PlayerDieEffect>("Effect/eff_Plrdeadcircle", 1);
        Item.parent = new OriginMgr<Item>("Item/item", 64);
        ItemLog.parent = new OriginMgr<ItemLog>("Item/itemLog", 32);
        Graze.parent = new OriginMgr<Graze>("Effect/eff_graze", 128);

        highScore = GrobalData.Instance.m_highScore;
        score = GrobalData.Instance.m_score;
        stock = GrobalData.Instance.m_stock;
        bomb = GrobalData.Instance.m_bomb;
        graze = 0;
	}

	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Escape)){ Application.Quit(); }
        //if (Input.GetKey(KeyCode.S)) { Time.timeScale = 0; }
        //if (Input.GetKey(KeyCode.T)) { Time.timeScale = 1; }
        this.SetUI();
        

    }

    void SetUI() {

        // スコアがハイスコアを超えていたら更新する
        if (score > highScore) { highScore = score; }

        // パワーが128ならMaxと表示する
        string sPower = power.ToString();
        if(power > 127) { sPower = "Max"; }

        // テキストの更新
        tFPS.text = Mathf.Round(1f / Time.deltaTime).ToString("F1") + "fps";
        tHiScore.text = "<b>"+highScore.ToString("D9")+"</b>";
        tScore.text = "<b>"+score.ToString("D9")+"</b>";
        tStock.text = "<b>" + new string('★', stock) + "</b>";
        tPower.text = "<b>" + sPower + "</b>";
        tGraze.text = "<b>" + graze + "</b>";

        if (encountBoss) {
            
            tTimer.text = "<b>" +timer.RemainingTime.ToString("D2")+ "</b>";
            sHPVer.value = bossHp;

        }

    }

    void SetTimer(int second,string name) {

        timer.LimitTime = second;
        bossHp = 100;

        tBossName.rectTransform.position = new Vector2(425, -50);
        tTimer.rectTransform.position = new Vector2(830, -50);
        

        tBossName.text = "<b>" + name + "</b>";
    }

}
