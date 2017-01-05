using UnityEngine;
using System.Collections;

public class Graze : Origin {

    public Sprite[] spr;
    float _alpha = -0.04f;
    float _scale = -0.04f;

    // グレイズ管理
    public static OriginMgr<Graze> parent = null;

    // グレイズの追加
    public static Graze Add(float x,float y) {

        float direction = Random.Range(0f, 360f);

        Graze gra = parent.Add(x, y, direction, 0, 4);

        if( gra == null) { return null; }

        gra.SetParam();

        return gra;
    }
    
    void SetParam() {

        SetSprite(Load("Sprites/Effect/Graze", "eff_base_" + Random.Range(0, 3)));

        SetAlpha(1.0f);

        SetScale(1, 1);
    }

	// Use this for initialization
	void Start () {

        spr = Resources.LoadAll<Sprite>("Sprites/Effect/Graze");

    }
	
	// Update is called once per frame
	void Update () {

        AddAlpha(_alpha);
        AddScale(_scale);
        
        if(GetAlpha() <= 0) {

            Vanish();
        }


	}
}
