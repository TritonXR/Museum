using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DrawLine2D : MonoBehaviour {

    private GameObject myLine;

    // Use this for initialization
    void Start () {
        myLine = new GameObject();
        myLine.AddComponent<LineRenderer>();
        myLine.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

    }

    void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        myLine.transform.position = start;
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(0.01f, 0.01f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}
