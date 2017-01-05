using UnityEngine;
using System.Collections;

public class ItemLog : Origin {

    // ログの画像
    public Sprite[] logSpr;

    public Item item;

    public int logNum;

    public static OriginMgr<ItemLog> parent = null;

    public static ItemLog Add(int id,float x,float y,float direction,float angle,float speed,Item item) {

        ItemLog ilog = parent.Add(x, y, direction, angle, speed);

        if(ilog == null) { return null; }

        ilog.SetParam(id, item);

        return ilog;
    }

    void SetParam(int id,Item _item) {

        SetSprite(Load("Sprites/Item", "item_" + id));

        logNum = id;

        item = _item;

    }


	// Use this for initialization
	void Start () {

        // ログのスプライトを読み込む
        logSpr = Resources.LoadAll<Sprite>("Sprites/Item");
	}
	
	// Update is called once per frame
	void Update () {

        this.State();
	}

    void State() {

        if(item.Y < 4.66f || item.activeFlag == false) {

            Vanish();

            logNum = 0;
        }

        

        
    }
}
