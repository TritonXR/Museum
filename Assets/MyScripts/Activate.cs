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

    void OnMouseDown()
    {

        //if (Input.GetMouseButtonDown(0))
        //{

            objActivate.SetActive(!objActivate.activeSelf);

        //}
    }
}
