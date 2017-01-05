using UnityEngine;
using System.Collections;

public class CollisionPoint : Origin {

    public bool clockwise;

    // Use this for initialization
    void Start () {

        SetScale(2.0f, 2.0f);
        SetAlpha(0);
       
	}
	
	// Update is called once per frame
	void Update () {

        this.Spin();

        StartCoroutine("CheckState");

    }

    void Spin() {

        if(clockwise == true) {

            Angle += 2.0f;

        }
        else {

            Angle -= 2.0f;
        }
    }

    IEnumerator CheckState() {

        if (Input.GetButton("Fire3"))
        {
            if (GetScale.x > 1.0) { AddScale(-0.1f); }

            SetAlpha(255);

            yield break;

        }
        else if (Input.GetButtonUp("Fire3"))
        {

            SetAlpha(0);

            SetScale(2.0f, 2.0f);

            yield break;
        }
    }

    
}
