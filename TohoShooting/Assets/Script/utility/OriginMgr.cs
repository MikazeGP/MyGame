using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OriginMgr<Type> where Type : Origin {

    int _size = 0;
    GameObject _prefab = null;
    List<Type> _pool = null;

    /// Order in Layer
    int _oder = 0;

    /// ForEach関数に渡す関数の型
    public delegate void FuncT(Type t);

    ///　コンストラクタ
    ///　プレハブは必ず"Resources/Prefabs/"に配置すること
    public OriginMgr(string prefabName,int size = 0) {

        _size = size;
        _prefab = Resources.Load("Prefabs/" + prefabName) as GameObject;
        
        if(_prefab == null) {

            Debug.Log("Not found prefab. name = " + prefabName);
        }

        _pool = new List<Type>();

        if(size > 0) {

            // サイズ指定があれば固定アロケーションとする
            for(int i = 0; i< size; i++) {

                GameObject g = GameObject.Instantiate(_prefab, new Vector3(), Quaternion.identity) as GameObject;
                Type obj = g.GetComponent<Type>();
                obj.vanishCannnotOverride();
                _pool.Add(obj);

            }
        }
    }

    /// オブジェクトを再利用する
    Type _Recycle(Type obj,float x,float y,float direction,float angle,float speed) {

        // 復活
        obj.Revive();
        obj.SetPosion(x, y);

        if(obj.RigidBody != null) {

            // Rigidbody2D があるときのみ速度を設定する
            obj.SetVelocity(direction, speed);
        }

        obj.Angle = angle;

        // Oeder in Layer をインクリメントして設定する
        //obj.SortingOrder = _oder;
        //_oder++;
        return obj;
    }

    /// インスタンスを取得する
    public Type Add(float x,float y,float direction = 0.0f,float angle = 0.0f,float speed = 0.0f) {

        foreach(Type obj in _pool) {

            if(obj.Exists == false) {

                // 未使用のオブジェクトを見つけた
                return _Recycle(obj, x, y, direction,angle, speed);
            }
        }

        if(_size == 0) {

            // 自働で拡張
            GameObject g = GameObject.Instantiate(_prefab, new Vector3(), Quaternion.identity) as GameObject;
            Type obj = g.GetComponent<Type>();
            _pool.Add(obj);
            return _Recycle(obj, x, y, direction,angle, speed);

        }
        return null;
    }

    /// 生存するインスタンスに対してラムダ式を実行する
    public void ForEachExitst(FuncT func) {

        foreach(var obj in _pool) {

            if (obj.Exists) {

                func(obj);
            }
        }
    }

    /// 生存しているインスタンスをすべて破棄する
    public void Vanish() {

        ForEachExitst(t => t.Vanish());
    }

    /// インスタンス生存数を取得する
    public int Count() {

        int ret = 0;
        ForEachExitst(t => ret++);

        return ret;
    }

}