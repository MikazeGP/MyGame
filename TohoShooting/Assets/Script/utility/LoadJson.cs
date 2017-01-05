using UnityEngine;
using System.Collections;
using System.IO;
using System;


[Serializable]
public class LaunchEnemies{

    public LaunchEnemy[] enemies;
}

[Serializable]
public class LaunchEnemy {

    public float frame;
    public int moveptn;
    public int bulletptn;
    public int enetype;
    public float posX;
    public float posY;
    public float speed;
    public float direction;
    public float angle;
}

public class LoadJson : MonoBehaviour {

    // 読み込むJsonファイル名
    public string fileName;

    void Start() {

        StartCoroutine("Generation");
    }

    IEnumerator Generation() {


        string path = "file://" + Application.dataPath + "/Script/utility/" + fileName;
        //string path = "file://" + "C:/Users/Keishi/Documents/My Games/TohoShooting/Assets" + "/Script/utility/" + fileName;
        //string path = "file://" + Application.dataPath + "/json/" + fileName;

        // パスを作成
        WWW www = new WWW(path);
        yield return www;

        print(www.text);

        string eneJson = www.text;

        var es = JsonUtility.FromJson<LaunchEnemies>(eneJson);
        
        
        foreach (var ene in es.enemies){

            
            for(float i = 0; i < ene.frame; i++) {

                yield return null;
            }

            // 敵を作成
            AddEnemy(
                ene.enetype,
                ene.moveptn, 
                ene.bulletptn,
                ene.posX,
                ene.posY,
                ene.speed,
                ene.direction,
                ene.angle);

        }
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    /// <summary>
    /// 敵を出現
    /// </summary>
    /// <param name="type">敵の種類</param>
    /// <param name="moveptn">行動パターン</param>
    /// <param name="bulletptn">弾発射パターン</param>
    /// <param name="x">出現位置X</param>
    /// <param name="y">出現位置YY</param>
    /// <param name="speed">速度</param>
    /// <param name="direction">方向</param>
    /// <param name="angle">スプライトの向き</param>
    /// <returns></returns>
    Enemy AddEnemy(int type, int moveptn, int bulletptn, float x, float y, float speed, float direction, float angle) {

        return Enemy.Add(type, moveptn, bulletptn, x, y, speed, direction, angle);
    }

}
