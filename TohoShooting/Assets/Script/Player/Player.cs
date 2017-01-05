using UnityEngine;
using System.Collections;

public class Player : Origin {


    [SerializeField]
    private float MoveSpeed;
    // 残機数
    public int Stock;
    // ボム数
    public int bomb;
    [SerializeField]
    // 無敵フラグ
    private bool invincibleFlag;
    [SerializeField]
    // リスポーンフラグ
    private bool respawnFlag;
    /// Shiftキーフラグ true...押されている.false...押されていない
    public bool shiftFlag;
    // パワー
    public int Power;
    [SerializeField]
    // フレーム
    private float Frame;
    [SerializeField]
    // カウンター
    private float Count;
    [SerializeField]
    // 無敵時間
    private float invincibleTime;

    public Vector2 EndposMin;
    public Vector2 EndposMax;

    public Sprite spr0;
    public Sprite spr1;
    public Sprite spr2;

    public GameObject[] _onmyoudama;

    public GameMgr gameMgr;

    // Use this for initialization
    void Start () {

        MoveSpeed = GrobalData.Instance.m_moveSpeed;
        Power = GrobalData.Instance.m_power;
        Frame = 0;
        Count = 2.5f;
        Stock = GrobalData.Instance.m_stock;
        bomb = GrobalData.Instance.m_bomb;
        invincibleFlag = true;
        shiftFlag = false;
        gameMgr = GameObject.Find("GameMgr").GetComponent<GameMgr>();

        ReimuShot.parent = new OriginMgr<ReimuShot>("Player/Reimu_Normalshot", 128);

    }


    // Update is called once per frame
    void Update () {

        if (invincibleFlag == true) { this.Invincible(); }

        if (respawnFlag == true) {

            AddPosion(0, 0.02f);

            if(Y >= -3.5f) { respawnFlag = false; }

            return;
        }

        this.MoveFunc();

        this.Shot();

        this.State();

    }

    // 移動処理
    void MoveFunc() {

        Vector2 v = Util.GetInputVector();

        if (Input.GetButton("Fire3")) {

            Animator.SetFloat("VX", Input.GetAxisRaw("Horizontal"));
            float speed = (MoveSpeed * Time.deltaTime)/2;
            ClampScreenAndMove(v * speed, EndposMin, EndposMax);
            shiftFlag = true;
           


        }else {

            Animator.SetFloat("VX", Input.GetAxisRaw("Horizontal"));
            float speed = (MoveSpeed * Time.deltaTime);
            ClampScreenAndMove(v * speed, EndposMin, EndposMax);
            shiftFlag = false;
            
        }

        gameMgr.power = Power;
        gameMgr.stock = Stock;

    }

    // 弾の生成
    ReimuShot AddReimuShot1(int id ,float addX,float addY, float direction,float angle,float speed) {

        return ReimuShot.Add(id, X + addX, Y + addY, direction,angle, speed);
    }

    ReimuShot AddReimuShot2(int id,float posX,float posY,float direction,float angle,float speed) {

        return ReimuShot.Add(id, posX, posY, direction, angle, speed);
    }

    // 弾を撃つ
    void Shot() {

        if (Input.GetButton("Fire1")) {
            if(Frame%Count == 0) {

                AddReimuShot1(0, -0.2f, 0.1f, 90,90, 35);
                AddReimuShot1(0, 0.2f, 0.1f, 90, 90, 35);

                if(Power >= 16) {


                        AddReimuShot2(1, _onmyoudama[0].transform.position.x + 0.1f, _onmyoudama[0].transform.position.y + 0.4f, 90, 90, 35);
                        AddReimuShot2(1, _onmyoudama[0].transform.position.x - 0.1f, _onmyoudama[0].transform.position.y + 0.4f, 90, 90, 35);

                }

                if(Power >= 32) {

                  
                        AddReimuShot2(1, _onmyoudama[1].transform.position.x + 0.1f, _onmyoudama[1].transform.position.y + 0.4f, 90, 90, 35);
                        AddReimuShot2(1, _onmyoudama[1].transform.position.x - 0.1f, _onmyoudama[1].transform.position.y + 0.4f, 90, 90, 35);

                }

                if (Power >= 64){

                    AddReimuShot2(1, _onmyoudama[2].transform.position.x + 0.1f, _onmyoudama[2].transform.position.y + 0.4f, 90, 90, 35);
                    AddReimuShot2(1, _onmyoudama[2].transform.position.x - 0.1f, _onmyoudama[2].transform.position.y + 0.4f, 90, 90, 35);


                }

                if (Power == 128){

                    AddReimuShot2(1, _onmyoudama[3].transform.position.x + 0.1f, _onmyoudama[3].transform.position.y + 0.4f, 90, 90, 35);
                    AddReimuShot2(1, _onmyoudama[3].transform.position.x - 0.1f, _onmyoudama[3].transform.position.y + 0.4f, 90, 90, 35);
                }
            }   
        }

        Frame++;

    }

    PlayerDieEffect AddDieEffect(float posX, float posY, float direction, float angle, float speed) {

        return PlayerDieEffect.Add(posX, posY, speed, direction, angle);
    }



    // 衝突したとき
    void OnTriggerEnter2D(Collider2D col){

        // 敵の弾or敵と衝突したとき...
        if(col.tag == "EneBul" || col.tag == "Enemy") {

            // 無敵でないときは被弾
            if (invincibleFlag == false){

                // 残機を１下げる
                Stock--;

                // 残機があれば復活
                if (Stock != 0){

                    AddDieEffect(X, Y, 0, 0, 0);

                    // パワーを1/2にする
                    Power -= Mathf.RoundToInt(Power / 2);
                   

                    // ポジションを初期位置に変更
                    SetPosion(0, -5.5f);

                    // 無敵フラグオン
                    invincibleFlag = true;

                    // リスポーンフラグオン
                    respawnFlag = true;

                }else{

                    // ゲームオーバー
                }

            }
        }

        if(col.tag == "Item") {

        }

        
    }

    void Invincible() {

        // 点滅処理
        if (Frame % 5 == 0){

            Renderer.enabled = !Renderer.enabled;
        }

        invincibleTime++;

        if(invincibleTime >= 180f) {

            invincibleFlag = false;

            Renderer.enabled = true;

            invincibleTime = 0;
        }
    }
    void State() {

        if(Y >= 3.0f && Power == 128) {

            Item.parent.ForEachExitst(i => i.autoCollect = true);
        }
    }
}
