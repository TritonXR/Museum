using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {
    
    void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("LALALAL");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
