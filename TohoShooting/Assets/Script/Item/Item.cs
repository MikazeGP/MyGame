using UnityEngine;
using System.Collections;

public class Item : Origin {

    // アイテムの画像
    public Sprite[] itemSpr;
    // プレイヤー
    public Player plr;
    // 能力ナンバー
    public int abilityNum;
    // ログ生成フラグ
    private bool logGeneFlag;
    // 自動回収フラグ
    public bool autoCollect;
    // アイテム生存フラグ
    public bool activeFlag;
    


    // アイテムの管理
    public static OriginMgr<Item> parent = null;

    // アイテムの生成
    public static Item Add(int id,float x,float y,float direction,float angle,float speed) {

        Item item = parent.Add(x, y,direction,angle,speed);

        if( item == null) { return null; }

        item.SetParam(id);

        return item;
    }

    
    void SetParam(int id) {

        // スプライトを設定
        SetSprite(Load("Sprites/Item","item_"+id));

        abilityNum = id;

        if(logGeneFlag == false ) { logGeneFlag = true; }
        if (autoCollect == true) { autoCollect = false; }

        activeFlag = true;

    }

    void Start() {

        // アイテムのスプライトを読み込む
        itemSpr = Resources.LoadAll<Sprite>("Sprites/Item");
        // プレイヤーを取得
        plr = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
    }

    void Update() {

        this.State();
        // 自動回収フラグがオンならば以降の処理は行われない
        if (autoCollect == true) { this.Collet(); return; }
        this.MoveFunc();
        
    }

    // アイテムの効果
    void Ability(int id) {

        if(id < 2) {

            // パワーを上げる
            plr.Power += 1 + 7 * id;
            return;
        }

        switch (id) {

            case 2:
                // スコアを加点

                break;
            case 3:
                // ボムを＋１
                plr.bomb++;
                break;

            case 4:
                // パワーを最大値にする
                plr.Power = 128;
                break;
            case 5:
                // 残機を＋１する
                plr.Stock++;
                break;
            case 6:
                // スコアを加点
                break;
            default:
                // なし
                break;
        }
    }

    // プレイヤーがアイテムを取得したとき
    void OnTriggerEnter2D(Collider2D col) {

        // アイテムの効果
        Ability(abilityNum);

        // 消滅
        Vanish();

    }
    ItemLog AddItemLog(int id,float x,float y,float directin,float angle,float speed,Item item) {

        return ItemLog.Add(id, x, y, directin, angle, speed, item);
    }

    Item AddItem(int id,float x,float y,float direction,float angle,float speed) {

        return Item.Add(id, x, y, direction, angle, speed);
    }

    // 状態管理
    void State() {

        if(plr.Power == 128f && abilityNum < 2) {

            AddItem(2, X, Y, 0, 0, 0);

            activeFlag = false;

            Vanish();
        }

        if (plr.shiftFlag == true) {

            CheckCollect();
        }

        // 上画面外に出たとき
        if(Y >= 4.66f && logGeneFlag == true) {

            // ログを生成
            AddItemLog(abilityNum + 8, X, 4.61f, 0, 0, 0, this);

            // 一度しか生成しない
            logGeneFlag = false;

        }
        // ↓画面外に出たとき
        if (Y < -4.7f){

            activeFlag = false;

            // 消滅
            Vanish();
        }
    }

    // 移動処理
    void MoveFunc() {

        VY -= 0.05f;

        VY = Mathf.Clamp(VY, -4.0f, 4.0f);
    }

    // 自動収集
    void CheckCollect() {

        float fixX = plr.X - X;
        float fixY = plr.Y - Y;

        float fixPos = Mathf.Sqrt(fixX * fixX + fixY * fixY);

        if(fixPos < 1.2) {

            autoCollect = true;
        }
    }
    
    public void Collet() {

        this.SetVelocity(0, 0);
        float fixX = plr.X - X;
        float fixY = plr.Y - Y;

        SetVelocityXY(0, 0);
        AddPosion(fixX / 8, fixY / 8);
    }
}
