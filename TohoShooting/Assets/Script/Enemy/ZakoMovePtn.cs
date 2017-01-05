using UnityEngine;
using System.Collections;
using System;

namespace ZakoMovePtn {

    public abstract class ZakoMovePtn{

        public abstract void Move(Enemy ene);
        
        public int count = 1;
        public int step = 0;
    }

    // 初期のパラメータで移動
    public class Move0 : ZakoMovePtn {

        public override void Move(Enemy ene){

            return;
        }
    }

    // 左に移動
    public class Move1 : ZakoMovePtn
    {

        public override void Move(Enemy ene){

            float frame = ene._frame;

            if (frame >= 120) { ene.AddVelocity(-0.1f, 0.00f); }
        }
    }
    // 右に移動
    public class Move2 : ZakoMovePtn
    {

        public override void Move(Enemy ene)
        {

            float frame = ene._frame;

            if (frame >= 120) { ene.AddVelocity(0.1f, 0.00f); }
        }
    }
    // 真ん中で時計回り
    public class Move3 : ZakoMovePtn
    {

        public override void Move(Enemy ene)
        {
           
            float frame = ene._frame;
            if (frame == 0 && step != 0 && count != 0) { step = 0; count = 0; }

            if (ene.X >= 0 && step == 0) {step++;ene.SetVelocity(0, 0); }
            
            if (step == 1 && count <210) {
               
                ene.RigidBody.MovePosition(new Vector2(1.5f * Mathf.Sin(count * 0.03f), 1.5f+1.5f * Mathf.Cos(count * 0.03f)));
                count++;
            }
            if(count == 210) { ene.SetVelocity(0, 2.5f); }
        }
    }

    // 真ん中で反時計周り
    public class Move4 : ZakoMovePtn
    {

        public override void Move(Enemy ene)
        {

            float frame = ene._frame;
            if (frame == 0 && step != 0 && count != 0) { step = 0; count = 0; }

            if (ene.X <= 0 && step == 0) { step++; ene.SetVelocity(0, 0); }

            if (step == 1 && count < 210){

                ene.RigidBody.MovePosition(new Vector2(-1.5f * Mathf.Sin(count * 0.03f), 1.5f + 1.5f * Mathf.Cos(count * 0.03f)));
                count++;
            }
            if (count == 210) { ene.SetVelocity(180, 2.5f); }

        }
    }

    
    public class Move5 : ZakoMovePtn {

        public override void Move(Enemy ene){

            float frame = ene._frame;

            
           
        }
    }

    
    public class Move6 : ZakoMovePtn
    {

        public override void Move(Enemy ene)
        {
            float frame = ene._frame;

            

        }
    }



}


