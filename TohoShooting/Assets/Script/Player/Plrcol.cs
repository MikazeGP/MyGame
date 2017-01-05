using UnityEngine;
using System.Collections;

public class Plrcol : Origin {

	// Use this for initialization
	void Start () {

        SetAlpha(0);
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Fire3")){

            SetAlpha(255);
        }else if (Input.GetButtonUp("Fire3")) {

            SetAlpha(0);
        }


	}
}
