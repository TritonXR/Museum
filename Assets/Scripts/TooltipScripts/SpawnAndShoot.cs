using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndShoot : MonoBehaviour {

    public GameObject plasma;

    public Transform spawnPos;

    public GameObject leftController;

    public float fireForce = 3f;

    private SteamVR_Controller.Device leftDevice;
    private GameObject myPlasma;
    private GameObject myLine;

    private Vector3 zeroVec = new Vector3(0, 0, 0);
    private GameObject hittedObj;
    private int leftIndex;

    // Use this for initialization
    void Start () {
        if (leftController.activeSelf)
        {
            leftIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
            leftDevice = SteamVR_Controller.Input(leftIndex);
        }

        myPlasma = null;

        myLine = new GameObject();

        myLine.AddComponent<LineRenderer>();
        myLine.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        if(leftDevice == null)
        {
            leftDevice = SteamVR_Controller.Input(leftIndex);
        }
        if (leftController.activeSelf)
        {
            if (leftDevice.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (myPlasma == null)
                {
                    myPlasma = (GameObject)Instantiate(plasma, spawnPos.transform.position, spawnPos.transform.localRotation);
                    myPlasma.GetComponent<Rigidbody>().useGravity = false;
                    myPlasma.transform.parent = spawnPos.parent;
                }

                
            }

            if (leftDevice.GetPress(SteamVR_Controller.ButtonMask.Trigger))
            {
                RaycastActivate();
            }
            else
            {
                myLine.SetActive(false);
            }
        }


		
	}




    private void RaycastActivate()
    {
        myLine.SetActive(true);
        Ray myRay = new Ray(spawnPos.position, spawnPos.forward);
        RaycastHit hit;

        if (Physics.Raycast(myRay, out hit, Mathf.Infinity))
        {


            DrawLine(spawnPos.position, hit.point, Color.green);

            if ((hit.collider.gameObject.CompareTag("resizable")))
            {
                // show up text, release trigger!

                if (leftDevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                {
                    ShootPlasma();
                }


            }

        }
        else
        {
            DrawLine(zeroVec, zeroVec, Color.green);
        }
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
        //       GameObject.Destroy(myLine, duration);
    }


    void ShootPlasma()
    {
        myPlasma.GetComponent<Rigidbody>().velocity = transform.forward * fireForce;
        Destroy(myPlasma, 10);

        myPlasma = null;
    }


}
