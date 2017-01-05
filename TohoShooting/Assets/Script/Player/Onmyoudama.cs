using UnityEngine;
using System.Collections;

public class Onmyoudama : Origin {

    public int num;
    public Player plr;
    

    // Use this for initialization
    void Start(){
       
    }

    void FixedUpdate() {

        this.MoveFunc();

        
    }

    // Update is called once per frame
    void Update(){

        this.CheckState(plr.Power);
    }

    void MoveFunc() {

        Angle -= 5;

        if (Input.GetButton("Fire3")){


            if(num == 1) { Vector3 v = new Vector3(-0.7f, 0); transform.localPosition = v; }
            if(num == 2) { Vector3 v = new Vector3(0.7f, 0); transform.localPosition = v; }
            if(num == 3) { Vector3 v = new Vector3(0, 0.7f); transform.localPosition = v; }
            if(num == 4) { Vector3 v = new Vector3(0, -0.7f); transform.localPosition = v; }

        }else {

            if (num == 1) { Vector3 v = new Vector3(-1.0f, 0); transform.localPosition = v; }
            if (num == 2) { Vector3 v = new Vector3(1.0f, 0); transform.localPosition = v; }
            if (num == 3) { Vector3 v = new Vector3(0, 1.0f); transform.localPosition = v; }
            if (num == 4) { Vector3 v = new Vector3(0, -1.0f); transform.localPosition = v; }
        }
    }
    
    void CheckState(float power) {

        if(power < 16.0f) {

            SetAlpha(0);
            return;

        }else if (power >= 16.0f && power < 32.0f){

            if (num == 1) { SetAlpha(255); }
            else { SetAlpha(0); }
            return;

        }else if(power >= 32.0f && power < 64.0f) {

            if(num == 1 || num == 2) { SetAlpha(255); }
            else { SetAlpha(0); }
            return;

        }else if(power >= 64.0f && power < 128.0f) {

            if(num == 1 || num == 2 || num == 3) { SetAlpha(255); }
            else { SetAlpha(0); }
            return;

        }else if(power == 128.0f) {

            SetAlpha(255);
            return;
        }
    }

}
