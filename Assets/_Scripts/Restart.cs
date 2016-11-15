using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {
    

    // still don't why when restart the color of the material will change
    void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("LALALAL");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
