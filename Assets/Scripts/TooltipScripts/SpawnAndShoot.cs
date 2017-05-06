using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Transform loadingBar;
    public float ActivationTime;

    private GameObject myPlasma;
    private GameObject myLine;
    private GameObject hittedObj;
    private bool readToShoot;
    private float timeElapsed = 0;

    // Use this for initialization
    void Start () {
        readToShoot = false;
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
        timeElapsed = 0;
        readToShoot = false;
        StartCoroutine(StartLoading());
        CreatePlasma();
        arrow.SetActive(true);
    }
    
    void TriggerUnClicked(object sender, ClickedEventArgs e)
    {
        if (readToShoot)
        {
            ShootPlasma();
        }
        else
        {
            // Destroy Plasma and do nothing
            DestroyPlasma();
        }
        timeElapsed = 0;
        arrow.SetActive(false);
    }


    IEnumerator StartLoading()
    {
        while (true)
        {
            timeElapsed += Time.deltaTime;
            LoadingImage(ActivationTime);

            if (timeElapsed >= ActivationTime)
            {
                // LoadingBar Disappear
                // set readyToShoot to be true
                readToShoot = true;
                break;
            }

            yield return null;
        }

        yield return null;
    }
    void CreatePlasma()
    {
        //Debug.Log("I am about to create a plasma");
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
        //Debug.Log("I am going to shoot the plasma ");
        myPlasma.transform.parent = null;
        myPlasma.GetComponent<Rigidbody>().velocity = spawnPos.transform.forward * fireForce;
        leftDeviceGeneral.TriggerHapticPulse(3500);
        Destroy(myPlasma, 10);
        myPlasma = null;
    }

    void DestroyPlasma()
    {
        Destroy(myPlasma);
        myPlasma = null;
    }

    void LoadingImage(float activationTime)
    {
        var image = loadingBar.GetComponent<Image>();
        loadingBar.GetComponent<Image>().fillAmount = Mathf.Clamp(timeElapsed / activationTime, 0, 1);
        Color c = image.color;
        c = Color.white;
        c.a = 0.2f;
        image.color = c;
    }



}
