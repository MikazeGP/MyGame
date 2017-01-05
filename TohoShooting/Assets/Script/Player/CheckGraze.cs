using UnityEngine;
using System.Collections;

public class CheckGraze : Origin {

    GameMgr gameMgr;

	// Use this for initialization
	void Start () {

        gameMgr = GameObject.Find("GameMgr").GetComponent<GameMgr>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    Graze AddGraze(float x,float y) {

        return Graze.Add(x, y);
    }
    
    void OnTriggerEnter2D(Collider2D col){

        print("In");

        gameMgr.graze++;

        gameMgr.score += 10;

        // グレイズエフェクトを生成
        AddGraze(col.transform.position.x, col.transform.position.y);
    }
}
