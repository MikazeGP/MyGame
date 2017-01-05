using UnityEngine;
using System.Collections;

public class Bullet : Origin {

    public Sprite[] BulSpr;

    [SerializeField]
    private bool OnCollider;

    public static OriginMgr<Bullet> parent = null;

    public static Bullet Add(int id, float x, float y, float direction, float angle, float speed) {

        Bullet bul = parent.Add(x, y, direction, angle, speed);

        if(bul == null) {

            return null;
        }

        // 初期パラメーターを設定
        bul.SetParam(id);

        return bul;
    }

    /// 弾のID
    int _id = 0;

    // IDからパラメータを設定
    public void SetParam(int id) {

        if(_id != 0) {

        }

        if(id != 0) {

        }

        _id = id;

        SetSprite(Load("Sprites/Bullet","etama_"+_id));

        SetCollider(_id);
    }
    

    // Use this for initialization
    void Start () {

        BulSpr = Resources.LoadAll<Sprite>("Sprites/Bullet");


    }
	
	// Update is called once per frame
	void Update () {

        if (IsOutside()){

            // 画面外に出たら消える
            Vanish();
        }

    }

    void SetCollider(int id) {

        // サークルコライダー
        if(id < 64) {

            CircleColliderEnabled = true;
            BoxColliderEnabled = false;
            CollisionRadius = 0.07f;

        }

        // ボックスコライダー
        if(id > 63 && id < 144) {

            CircleColliderEnabled = false;
            BoxColliderEnabled = true;
            SetBoxColliderSize(0.09f, 0.13f);
            
        }

        // ボックスコライダー
        if(id > 143 && id < 160) {

            CircleColliderEnabled = false;
            BoxColliderEnabled = true;
            SetBoxColliderSize(0.15f, 0.15f);
        }
    }

    // 衝突したとき
    void OnTriggerEnter2D(Collider2D col) {

        if(col.tag == "Graze") { return; }

        // 消滅
        Vanish();
       
    }



}
