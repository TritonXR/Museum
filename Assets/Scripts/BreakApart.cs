using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakApart : MonoBehaviour {

    public GameObject explosion;

    private List<GameObject> childsToDelete;

    bool exploded = false;

    private void Start()
    {
        childsToDelete = new List<GameObject>();
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.collider.gameObject.tag == "ground" && !exploded)
        {
            //Debug.Log("lalal");
            Boom();
            if(transform.childCount == 0)
            {
                //Debug.Log("Now I have " + transform.childCount);
                exploded = true;
                StartCoroutine(DeletePiece());
            }
        }
    }

    void Boom() {
        if(!explosion.activeSelf) explosion.SetActive(true);
        //Debug.Log("I have " + transform.childCount);
        
        foreach (Transform child in transform)
        {
            childsToDelete.Add(child.gameObject);
        }

        foreach (GameObject child in childsToDelete)
        {
            child.transform.parent = null;
            //if (child.tag != "Fire") is optional in case you don't wanna fire to fall
            child.AddComponent<Rigidbody>();
            child.GetComponent<Rigidbody>().mass = 30;
        }
        //Debug.Log("What's up");
    }


    IEnumerator DeletePiece()
    {
        yield return new WaitForSeconds(10);
        foreach(GameObject childObj in childsToDelete)
        {
            Destroy(childObj);
        }

        childsToDelete.Clear();
        Destroy(transform.gameObject);
    }
}
