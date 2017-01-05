using UnityEngine;
using System.Collections;

public class EnemyDieEffect : Origin {

    public Sprite[] EffSpr;

    public int num;
    public float al;
    public float angle1,angle2;
    public bool random;
    private float _addscale = 0.2f;
    private float nowscale;

    // エフェクト管理
    public static OriginMgr<EnemyDieEffect> parent = null;

    // エフェクトの追加
    public static EnemyDieEffect Add(int type,float x,float y,float speed,float direction,float angle,int count) {

        EnemyDieEffect enedie = parent.Add(x, y, direction, angle, speed);

        if(enedie == null) { return null; }

        enedie.SetParam(type,count);

        return enedie;
    }
    
    void SetParam(int type,int count) {

        SetSprite(Load("Sprites/Effect", "eff_deadcircle_" + type));
        num = count;
        angle1 = Random.Range(10f, 70f);
        angle2 = Random.Range(110f, 170f);
        random = RandomBool();
        
        if (num == 0) { }
        else{

            if (random == true){

                transform.eulerAngles = new Vector3(angle1, 55f, 0);
            }
            else{

                transform.eulerAngles = new Vector3(angle2, 55f, 0);
            }
        }
    }

    // Use this for initialization
    void Start () {

        EffSpr = Resources.LoadAll<Sprite>("Sprites/Effect");
    }
	
	// Update is called once per frame
	void Update () {

        // 次第に透明にする
        
        AddAlpha(-0.02f);

        if (GetAlpha() <= 0) { SetAlpha(0); }

        

        AddScale(_addscale);

        if(GetAlpha() == 0 ) {

            Vanish();
            SetScale(1, 1);
            Alpha = 0.39f;
        }
    }

    
}
