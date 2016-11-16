using UnityEngine;
using System.Collections;

public class RotateWingAngle : MonoBehaviour
{

    [Tooltip("Maximum of degreeX you want to rotate")]
    public float degreeX;
    [Tooltip("Maximum of degreeY you want to rotate")]
    public float degreeY;
    [Tooltip("Maximum of degreeZ you want to rotate")]
    public float degreeZ;
    [Tooltip("Amount should be from 0 to 1. If 1 then rotate the max amount of degree")]
    public float amount;
    [Tooltip("The time it takes to finish rotating")]
    public float seconds;
    public bool reverse;

    private Quaternion start;
    private Quaternion end;
    private Vector3 startRotation;
    private Vector3 endRotation;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateWing(reverse);
        }
    }

    public void RotateWing(bool reverse)
    {
        StartCoroutine(RotateWingCoroutine(seconds, amount, reverse));
    }

    IEnumerator RotateWingCoroutine(float seconds, float amount, bool reverse)
    {

        // get the original eulerAngles in Quaternion
        start = transform.localRotation;
        //Debug.Log("Start = " + start.eulerAngles);

        // whether we want to reverse
        if (reverse)
        {
            degreeX = -degreeX;
            degreeY = -degreeY;
            degreeZ = -degreeZ;
        }

        // standarlize the amount value
        if (amount > 1) amount = 1;
        if (amount < 0) amount = 0;
        
        startRotation = transform.localRotation.eulerAngles;

        //Debug.Log("StartRotation = " + startRotation);

        endRotation = startRotation;
        endRotation.x += degreeX;
        endRotation.y += degreeY;
        endRotation.z += degreeZ;
        //Debug.Log("EndRotation = " + endRotation);

        // get the end and start Quaternion position based on the calculated eulerAngles
        end = Quaternion.Euler(endRotation);
        start = Quaternion.Euler(startRotation);

        for (float i = 0; i < seconds; i += Time.deltaTime)
        {
            transform.localRotation = Quaternion.Lerp(start, end, i / seconds * amount);
            yield return null;
        }
    }


}
