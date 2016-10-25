using UnityEngine;
using VRTK;
using System.Collections;

public class Activate : MonoBehaviour {


    private GameObject objActivate;

    void Start() {
        objActivate = GetComponentInChildren<VRTK_ObjectTooltip>().gameObject;
        objActivate.SetActive(false);
    }

	// Update is called once per frame
	void Update () {



    }

    void OnMouseEnter()
    {

        //if (Input.GetMouseButtonDown(0))
        //{

            objActivate.SetActive(true);

        //}
    }

    void OnMouseExit()
    {

        //if (Input.GetMouseButtonDown(0))
        //{

        objActivate.SetActive(true);

        //}
    }


}
