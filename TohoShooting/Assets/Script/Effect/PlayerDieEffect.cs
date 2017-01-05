using UnityEngine;
using System.Collections;

public class PlayerDieEffect : Origin {

    // エフェクトの管理
    public static OriginMgr<PlayerDieEffect> parent = null;

    // エフェクトの生成
    public static PlayerDieEffect Add(float x,float y,float speed,float direction,float angle) {

        PlayerDieEffect plrdie = parent.Add(x, y, direction, angle, speed);

        if(plrdie == null) { return null; }

        return plrdie;
    }

	// Use this for initialization
	void Start () {
	    

	}
	
	// Update is called once per frame
	void Update () {

        AddAlpha(-0.04f);

        if (GetAlpha() <= 0) { SetAlpha(0); }

        AddScale(0.3f);

        if (GetAlpha() == 0)
        {

            Vanish();
            SetScale(1, 1);
            Alpha = 0.39f;
        }
    }
}
