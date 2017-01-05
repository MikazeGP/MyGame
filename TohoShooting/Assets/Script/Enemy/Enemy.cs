using UnityEngine;
using System.Collections;
using ZakoMovePtn;
using ZakoBulletPtn;

public class Enemy : Origin {

    [SerializeField]
    /// HP
    private int _hp = 0;

    /// フレーム
    public float _frame = 0.0f;

    //　敵のID
    public int _type;

    // 現在のアニメーションクリップのインデックス
    private int currentClipWait;
    private int currentClipLeft;
    private int currentClipRight;


    public Sprite[] waitspr;
    public Sprite[] turnspr;

    /// 敵管理
    public static OriginMgr<Enemy> parent = null;

    /// 移動管理
    ZakoMovePtn.ZakoMovePtn zakoMovePtn = null;

    /// 弾発射管理
    ZakoBulletPtn.ZakoBulletPtn zakoBulletPtn = null;

    // 敵の追加
    public static Enemy Add(int type, int moveptn, int bulletptn,float x, float y, float speed, float direction, float angle)
    {

        Enemy ene = parent.Add(x, y, direction, angle, speed);

        if (ene == null){ return null;}

        // 初期パラメーター設定
        ene.SetParam(type, speed, moveptn, bulletptn);

        return ene;
    }

    // 初期パラメータを設定
    void SetParam(int type,float speed,int moveptn,int bulletptn) {

        _type = type;

        // スプライトを設定
        this._SetSprite(type);

        // 行動パターンを設定
        this.SetMovePtn(SetZakoMove(moveptn));

        // 弾発射パターンを設定
        this.SetBulletPtn(SetZakoBullet(bulletptn));

        // HP,アイテムNoの設定
        this.SetHp(type);

        _frame = 0;
    }


    // スプライトを設定
    void _SetSprite(int type) {


        if(type >= 109) { return; }

        // Resorces内の敵スプライトを読み込む
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Enemy/enemy");

        

        // 初期スプライトを設定
        SetSprite(System.Array.Find<Sprite>(sprites, (sprite) => sprite.name.Equals("enemy_" + type)));

        // 待機用スプライトを設定する
        for (int i = 0; i < 5; i++) {

            int num = i + type;
            waitspr[i] = System.Array.Find<Sprite>(sprites, (sprite) => sprite.name.Equals("enemy_" + num));

        }

        // 移動用スプライトを設定する
        for (int i = 0; i < 7; i++) {

            int num = i + type + 5;
            turnspr[i] = System.Array.Find<Sprite>(sprites, (sprite) => sprite.name.Equals("enemy_" + num));
        }
    }

    void SetHp(int id) {

        switch (id) {

            case 0:
            case 12:
                _hp = 2;
                break;
            case 24:
            case 36:
                _hp = 4;
                break;
            case 48:
            case 60:
                _hp = 20;
                break;
            case 72:
                _hp = 40;
                break;
            case 84:
                _hp = 60;
                break;
            case 96:
                _hp = 80;
                break;
        }
    }

    int ItemNum(int type) {

        int num = 0;

        if(type == 12 || type == 36) { return num; }
        else if(type == 0 || type == 24) { num = 2; return num; }
        else if(type == 72 || type == 84) { num = 1; return num; }
        else if(type == 96) { num = 5; return num; }
        else { num = 3; return num; }

    }

    void Start() {

        //　アニメーションクリップを初期化
        currentClipWait = 0;
        currentClipLeft = 0;
        currentClipRight = 0;
    }

   void Update() {

        // フレームを更新
        _frame++;
        // アニメーション処理を行う
        this.AnimationFunc();

        // 行動処理を読み込む
        this.GetMovePtn();

        // 弾発射処理を読み込む
        this.GetBulletPtn();

        if (IsOutside())
        {
            // 画面外に出たら消える
            Vanish();

        }
    }

    //　アニメーション処理
    void  AnimationFunc() {
        
        //3フレームに１度呼ぶ
        if(_frame%4 == 0) {

            // 左に移動しているなら
            if (VX <= -2.0f)
            {
                // 待機用アニメーションクリップを０にする
                currentClipWait = 0;

                // 右移動用アニメーションクリップを０にする
                if (currentClipRight != 0) { currentClipRight = 0; }
                // 左移動用アニメーションクリップが７以上なら
                // ループしない
                if (currentClipLeft > 6) { currentClipLeft = 3; }

                // スプライトを反対にする
                ScaleX = -2.0f;
                // スプライトを設定
                SetSprite(turnspr[currentClipLeft]);
                // アニメーションクリップを更新
                currentClipLeft++;

                // 右に移動しているなら
            }
            else if (VX >= 2.0f){

                // 待機用アニメーションクリップを０にする
                currentClipWait = 0;

                // 左移動用アニメーションクリップを０にする
                if (currentClipLeft != 0) { currentClipLeft = 0; }
                // 右移動用アニメーションクリップが７以上なら
                // ループしない
                if (currentClipRight > 6) { currentClipRight = 3; }

                // スプライトの向きを元に戻す
                ScaleX = 2.0f;
                // スプライトを設定
                SetSprite(turnspr[currentClipRight]);
                // アニメーションクリップを更新
                currentClipRight++;

            }// 左右に移動していないとき
            else{

                
                // 左右のアニメーションクリップを０にする
                currentClipLeft = 0;
                currentClipRight = 0;

                // スプライトの向きを元に戻す
                ScaleX = 2.0f;
                // スプライトを設定
                SetSprite(waitspr[currentClipWait]);

                // アニメーションクリップを更新
                currentClipWait++;
                // アニメーション４以上なら０にして、ループする
                if (currentClipWait > 4) { currentClipWait = 0; }
            }
        }
    }

    // 行動パターンの設定
    void SetMovePtn(ZakoMovePtn.ZakoMovePtn _zakoptn) {

        this.zakoMovePtn = _zakoptn;
    }

    // 行動パターンを呼び出す
    void GetMovePtn() {


        this.zakoMovePtn.Move(this);
    }

    // 呼び出したい行動パターンを引数で設定
    ZakoMovePtn.ZakoMovePtn SetZakoMove(int ptn) {


        ZakoMovePtn.ZakoMovePtn result = null;


        switch (ptn) {

            case 0:
                result = new Move0();
                break;
            case 1:
                result = new Move1();
                break;
            case 2:
                result = new Move2();
                break;
            case 3:
                result = new Move3();
                break;
            case 4:
                result = new Move4();
                break;
            case 5:
                result = new Move5();
                break;
            case 6:
                result = new Move6();
                break;

        }

        return result;
    }

    // 弾発射パターンの設定
    void SetBulletPtn(ZakoBulletPtn.ZakoBulletPtn _zakoptn){

        this.zakoBulletPtn = _zakoptn;
    }

    // 弾発射パターンを呼び出す
    void GetBulletPtn(){

        this.zakoBulletPtn.ShotBullet(gameObject);
    }

    // 呼び出したい行動パターンを引数で設定
    ZakoBulletPtn.ZakoBulletPtn SetZakoBullet(int ptn) {

        ZakoBulletPtn.ZakoBulletPtn result = null;

        switch (ptn) {

            case 0:
                result = new Bullet0();
                break;
            case 1:
                result = new Bullet1();
                break;
            case 2:
                result = new Bullet2();
                break;
            default:
                result = new Bullet0();
                break;
        }

        return result;
    }

    // 死亡エフェクトの生成
    EnemyDieEffect AddDieEffect(int id ,float x,float y ,float speed,float direction,float angle,int count) {

        return EnemyDieEffect.Add(id, x, y, speed, direction, angle, count);
    }

    // アイテムの生成
    Item AddItem(int id,float x,float y,float direction,float angle, float speed) {

        return Item.Add(id, x, y, direction, angle, speed);
    }

    // 衝突したとき
    void OnTriggerEnter2D(Collider2D col) {

        if(col.tag == "Player") {

            Vanish();
        }
        if(col.tag == "PlrBul") {

            _hp--;

            if(_hp <= 0) {

                Vanish();

                AddDieEffect(1, X, Y, 0, 0, 0, 0);
                AddDieEffect(1, X, Y, 0, 0, 0, 1);

                float posX = Random.Range(-0.3f, 0.3f) + X;

                AddItem(ItemNum(_type), posX, Y, 90, 0, 3.0f);
                
                
            }

            
        }
    }

}
