using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndShoot : MonoBehaviour {

    // handle controller input
    public GameObject leftController;
    private SteamVR_TrackedController leftDevice;
    private SteamVR_Controller.Device leftDeviceGeneral;
    private SteamVR_TrackedObject trackedObj;
    


    // handle plasma interaction
    public GameObject plasma;
    public Transform spawnPos;
    public float fireForce = 3f;
    public GameObject arrow;
    private GameObject myPlasma;
    private GameObject myLine;
    private GameObject hittedObj;


    // Use this for initialization
    void Start () {

        leftDevice = leftController.gameObject.GetComponent<SteamVR_TrackedController>();
        trackedObj = leftController.gameObject.GetComponent<SteamVR_TrackedObject>();
        //leftDeviceGeneral = leftController.gameObject.GetComponent<SteamVR_TrackedObject>();
        myPlasma = null;
        arrow.SetActive(false);
        // handle trigger events
        leftDevice.TriggerClicked += TriggerClicked;
        leftDevice.TriggerUnclicked += TriggerUnClicked;

    }

    void TriggerClicked(object sender, ClickedEventArgs e)
    {
        CreatePlasma();
        arrow.SetActive(true);
    }
    
    void TriggerUnClicked(object sender, ClickedEventArgs e)
    {
        ShootPlasma();
        arrow.SetActive(false);
    }

    void CreatePlasma()
    {
        Debug.Log("I am about to create a plasma");
        if (myPlasma == null)
        {
            myPlasma = (GameObject)Instantiate(plasma, spawnPos.transform.position, spawnPos.transform.localRotation);
            myPlasma.GetComponent<Rigidbody>().useGravity = false;
            myPlasma.transform.parent = spawnPos.parent;
        }
    }

    private void Update()
    {
        leftDeviceGeneral = SteamVR_Controller.Input((int)trackedObj.index);
    }



    //private void RaycastActivate()
    //{
    //    myLine.SetActive(true);
    //    Ray myRay = new Ray(spawnPos.position, spawnPos.forward);
    //    RaycastHit hit;

    //    if (Physics.Raycast(myRay, out hit, Mathf.Infinity))
    //    {


    //        DrawLine(spawnPos.position, hit.point, Color.green);

    //        if ((hit.collider.gameObject.CompareTag("resizable")))
    //        {
    //            // show up text, release trigger!

    //            if (leftDevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
    //            {
    //                ShootPlasma();
    //            }


    //        }

    //    }
    //    else
    //    {
    //        DrawLine(zeroVec, zeroVec, Color.green);
    //    }
    //}


    //void DrawLine(Vector3 start, Vector3 end, Color color)
    //{
    //    myLine.transform.position = start;
    //    LineRenderer lr = myLine.GetComponent<LineRenderer>();
    //    lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
    //    lr.SetColors(color, color);
    //    lr.SetWidth(0.01f, 0.01f);
    //    lr.SetPosition(0, start);
    //    lr.SetPosition(1, end);
    //    //       GameObject.Destroy(myLine, duration);
    //}


    void ShootPlasma()
    {
        Debug.Log("I am going to shoot the plasma ");
        myPlasma.transform.parent = null;
        myPlasma.GetComponent<Rigidbody>().velocity = spawnPos.transform.forward * fireForce;
        leftDeviceGeneral.TriggerHapticPulse(3500);
        Destroy(myPlasma, 10);
        myPlasma = null;
    }


}
