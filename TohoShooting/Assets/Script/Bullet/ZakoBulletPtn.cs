using UnityEngine;
using System.Collections;
using System;

namespace ZakoBulletPtn {

    public abstract class ZakoBulletPtn{

        public abstract void ShotBullet(GameObject gameobj);

        public Bullet AddBullet(int id, float posX, float posY, float direction, float angle, float speed) {

            return Bullet.Add(id, posX, posY, direction, angle, speed);
        }

        public float GetAim(GameObject gameobj) {

            float dx = plr.X - gameobj.transform.position.x;
            float dy = plr.Y - gameobj.transform.position.y;
            return Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
            
        }

        Player plr = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // 弾を撃たない
    public class Bullet0: ZakoBulletPtn {

        public override void ShotBullet(GameObject gameobj){

            return;
        }
    }

    // 全方位弾
    public class Bullet1 : ZakoBulletPtn
    {

        public override void ShotBullet(GameObject gameobj)
        {

            Enemy ene = gameobj.GetComponent<Enemy>();
            float frame = ene._frame;

            if (frame >= 120)
            {
                if (frame % 120 == 0){

                    for (int rad = 0; rad < 360; rad += 6){

                        AddBullet(33, ene.X, ene.Y, rad, 180, 2.0f);
                    }
                }
            }
        }
    }


    public class Bullet2 : ZakoBulletPtn
    {

        public override void ShotBullet(GameObject gameobj){

            Enemy ene = gameobj.GetComponent<Enemy>();
            float frame = ene._frame;
            bool flag = true;

            if (frame >= 120){

                if (frame % 30 == 0){

                    Debug.Log(flag);
                    flag = !flag;
                }

                if (frame % 7  == 0 && flag == true) {

                    float rad = GetAim(gameobj);

                    AddBullet(33, ene.X, ene.Y, rad, 0, 4.0f);

                }
            }
        }
    }


}
