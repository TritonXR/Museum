using UnityEngine;
using System.Collections;

public class Glow : MonoBehaviour {

    private float rr=255f, gg = 255f, bb = 255f;
    bool isGrow = false;
    private Color myColor;
    

    private Material[] currentMaterials;
    private Color[] originColors;
    private Material newMaterial;

    void Start() {
        //StartCoroutine(ChangeColor(0.1f));
        myColor = new Color(146, 244, 66);
        currentMaterials = GetComponent<Renderer>().materials;
        originColors = new Color[currentMaterials.Length];
        for(int ii = 0; ii < currentMaterials.Length; ii++)
        {
            originColors[ii] = currentMaterials[ii].color;
            print(currentMaterials[ii]);
        }


    }


    // Update is called once per frame
    void Update()
    {

        for(int ii = 0; ii < currentMaterials.Length; ii++)
        {
            Color lerpedColor = Color.Lerp(originColors[ii], Color.white, Mathf.PingPong(Time.time, 1));
            currentMaterials[ii].SetColor("_Color", lerpedColor);
        }
        
        GetComponent<Renderer>().materials = currentMaterials;


    }


    //IEnumerator ChangeColor(float seconds) {


    //    while (true)
    //    {
    //        Material[] currentMaterial = GetComponent<Renderer>().materials;
    //        for (int i = 0; i < currentMaterial.Length; i++)
    //        {
    //            Material newMaterial = new Material(currentMaterial[i]);

    //            Color newColor = new Color(rr/255,gg/255,bb/255);

    //            //Debug.Log(newColor);

    //            newMaterial.SetColor("_Color", newColor);
    //            //Debug.Log("hi" + newMaterial.color);

    //            currentMaterial[i] = newMaterial;
    //        }

    //        GetComponent<Renderer>().materials = currentMaterial;


    //        UpdateColorValue();

    //        yield return new WaitForSeconds(seconds);
    //    }
    //}

    //void UpdateColorValue()
    //{
    //    if (isGrow)
    //    {
    //        rr += 10;
    //        bb += 10;
    //    }
    //    else
    //    {
    //        rr -= 10;
    //        bb -= 10;
    //    }

    //    if(rr < 170)
    //    {
    //        isGrow = true;
    //    }

    //    if(rr > 250){
    //        isGrow = false;
    //    }

    //}


}
