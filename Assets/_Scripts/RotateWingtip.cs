using UnityEngine;
using System.Collections;

public class RotateWingtip : MonoBehaviour {

    [Tooltip("Amount should be from -1 to 1, neg moves down, pos moves up")]
    public float amount;
    [Tooltip("The time it takes to finish rotating")]
    public float seconds;
    [Tooltip("Need to manually find the max point of rotating up")]
    public Vector3 upEnd;
    [Tooltip("Need to manually find the max point of rotating down")]
    public Vector3 downEnd;

    private Quaternion start; 
    private Quaternion end;

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
            end = Quaternion.Euler(downEnd);
        }
        else
        {
            end = Quaternion.Euler(upEnd);
        }

        //end.y = start.eulerAngles.y;
        //end.z = start.eulerAngles.z;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(RotateWing(seconds, amount));
        }
    }

    IEnumerator RotateWing(float seconds, float amount) {
        for (float i = 0; i < seconds; i+= Time.deltaTime)
        {
            
            transform.rotation = Quaternion.Lerp(start, end , i/seconds * amount);
            yield return null;
        }
    }


}
