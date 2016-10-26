using UnityEngine;
using System.Collections;

public class RotateWingtip : MonoBehaviour {

    public float amount;
    public float seconds;
    public Vector3 upEnd;
    public Vector3 downEnd;

    private Quaternion start; 
    private Quaternion endQ;

    // Use this for initialization
    void Start () {

        if (amount > 1f){
            amount = 1f;
        }

        if (amount < -1f){
            amount = -1;
        }


        //if (amount)
        start = transform.localRotation;

        if (amount < 0)
        {
            amount = Mathf.Abs(amount);
            endQ = Quaternion.Euler(downEnd);
        }
        else
        {
            endQ = Quaternion.Euler(upEnd);
        }

        //end.y = start.eulerAngles.y;
        //end.z = start.eulerAngles.z;

        StartCoroutine(RotateWing(seconds, amount));

    }

    IEnumerator RotateWing(float seconds, float amount) {
        for (float i = 0; i < seconds; i+= Time.deltaTime)
        {
            
            transform.rotation = Quaternion.Lerp(start, endQ , i/seconds * amount);
            yield return null;
        }
    }


}
