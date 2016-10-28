using UnityEngine;
using VRTK;
using System.Collections;

public class Activate : MonoBehaviour {

    public AudioClip myClip;
    public MovieTexture mov;

    private GameObject objActivate;
 
    void Start() {
        objActivate = GetComponentInChildren<VRTK_ObjectTooltip>().gameObject;
        objActivate.SetActive(false);

        this.gameObject.AddComponent<AudioSource>();
        this.GetComponent<AudioSource>().clip = myClip;

        mov = GetComponent<Renderer>().material.mainTexture as MovieTexture;
    }


    void OnMouseDown()
    {
        this.GetComponent<AudioSource>().Play();
    }

    void OnMouseEnter()
    {

        //if (Input.GetMouseButtonDown(0))
        //{

            objActivate.SetActive(true);

        if (Input.GetMouseButtonDown(1)) {
            mov.Play();
        }

        //}
    }

    void OnMouseExit()
    {

        //if (Input.GetMouseButtonDown(0))
        //{

        objActivate.SetActive(false);

        //}
    }


}
