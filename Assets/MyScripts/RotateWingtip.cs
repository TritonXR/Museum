using UnityEngine;
using System.Collections;

public class RotateWingtip : MonoBehaviour {

    public float amount;
    public float seconds;
    public Vector3 end;

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

        print(start.eulerAngles);

        endQ = Quaternion.Euler(end);

        //end.y = start.eulerAngles.y;
        //end.z = start.eulerAngles.z;

        StartCoroutine(RotateWing(seconds, amount));

    }

    IEnumerator RotateWing(float seconds, float amount) {
        for (float i = 0; i < seconds; i+= Time.deltaTime)
        {
            
            transform.rotation = Quaternion.Lerp(start, Quaternion.Euler(end*amount), i/seconds);
            yield return null;
        }
    }


}
