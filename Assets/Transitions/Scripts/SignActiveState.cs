using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignActiveState : MonoBehaviour
{

    public Transform thePlayer;
    public float sensitiveDistance = 15;
    public GameObject planeInfoBox;
    public GameObject planeInfoBox2;

    private float distance;

    private bool littleStatus;

    // Use this for initialization
    void Start()
    {
        planeInfoBox.SetActive(false);
        planeInfoBox2.SetActive(false);
        StartCoroutine(checkIfUserGetsClose());
    }


    IEnumerator checkIfUserGetsClose()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            distance = Vector3.Distance(thePlayer.position, transform.position);
            littleStatus = gameObject.GetComponent<Resizable>().isLittle;

            if (!littleStatus)
            {
                if (distance < sensitiveDistance)
                {
                    //Debug.Log("I am plane, I am sensitive and about to show the UI");
                    planeInfoBox.SetActive(true);
                    planeInfoBox2.SetActive(true);
                }
                else
                {
                    planeInfoBox.SetActive(false);
                    planeInfoBox2.SetActive(false);
                }
            }
            else
            {
                planeInfoBox.SetActive(false);
                planeInfoBox2.SetActive(false);
            }

            //Debug.Log("I am Plane, I am at distance of " + distance + " from player!! And my little status is " + littleStatus);
        }
    }
}
