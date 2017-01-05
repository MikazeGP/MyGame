using UnityEngine;
using System.Collections;

public class ReimuShot : Origin {

    // スプライト
    public Sprite Spr0;
    public Sprite Spr1;
    public Sprite Spr2;
    public Sprite Spr3;
    public Sprite Spr4;

    [SerializeField]
    private bool OnCollider;

    /// 弾管理
    public static OriginMgr<ReimuShot> parent = null;

    // 弾の追加
    public static ReimuShot Add(int id, float x, float y, float direction,float angle, float speed){

        ReimuShot rs = parent.Add(x, y, direction,angle, speed);

        if (rs == null)
        {

            return null;
        }

        // 初期パラメーター設定
        rs.SetParam(id);
        return rs;
    }

    /// 弾のID
    int _id = 0;

    // IDからパラメータを設定
    public void SetParam(int id){

        if(_id != 0) {

            // 前回のコルーチンを終了する
            StopCoroutine("_Update" + _id);
        }

        if(id != 0) {

            // 新しいコルーチンを新しく開始する
            StartCoroutine("_Update" + id);
        }

        // IDを設定
        _id = id;

        this._SetSprite(id);


        if (OnCollider == false) {

            this.SetCollider(id);
                           
        }
        
        
    }

    // コライダーを取り付ける
    void SetCollider(int id) {

            switch (id)
            {

                case 0:

                    AddBoxCollider();
                    SetBoxColliderSize(0.52f, 0.1f);
                    SetBoxColliderOffset(0.052f, 0f);
                    BoxColliderIsTrigger = true;

                    break;

                case 1:

                    AddBoxCollider();
                    SetBoxColliderSize(0.54f, 0.06f);
                    SetBoxColliderOffset(0f, 0f);
                    BoxColliderIsTrigger = true;

                    break;

                case 2:

                    AddCircleCollider();
                    CollisionRadius = 0.12f;
                    CircleColliderIsTrigger = true;

                    break;

                case 3:

                    AddBoxCollider();
                    SetBoxColliderSize(0.3f, 0.2f);
                    SetBoxColliderOffset(0.08f, 0f);
                    BoxColliderIsTrigger = true;

                    break;

                case 4:

                    AddBoxCollider();
                    SetBoxColliderSize(0.3f, 0.2f);
                    SetBoxColliderOffset(0.08f, 0f);
                    BoxColliderIsTrigger = true;

                    break;
                default:

                    break;
            }
    }

    // スプライトを設定する
    void _SetSprite(int id) {

        // スプライトテーブル
        Sprite[] sprs = { Spr0, Spr1, Spr2, Spr3, Spr4 };

        // スプライトを設定
        SetSprite(sprs[id]);
    }

    void Start() {

        OnCollider = false;
    }

    // Update is called once per frame
    void Update(){

        if (IsOutside()){

            // 画面外に出たら消える
            Vanish();

            if(OnCollider == false) {

                OnCollider = true;
            }
            
        }
    }

    void OnTriggerEnter2D(Collider2D col) {

        Vanish();
    }

    // 更新
    IEnumerator _Update1(){

        yield break;
    }


}
