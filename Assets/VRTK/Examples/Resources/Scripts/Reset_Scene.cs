namespace VRTK.Examples
{
	using UnityEngine;
	using UnityEngine.SceneManagement;

	public class Reset_Scene : MonoBehaviour
	{
		private void Start()
		{
			if (GetComponent<VRTK_SimplePointer>() == null)
			{
				Debug.LogError("VRTK_ControllerPointerEvents_ListenerExample is required to be attached to a Controller that has the VRTK_SimplePointer script attached to it");
				return;
			}

			GetComponent<VRTK_ControllerEvents>().TouchpadPressed += new ControllerInteractionEventHandler(DoTouchpadPressed);
			GetComponent<VRTK_ControllerEvents>().TouchpadReleased += new ControllerInteractionEventHandler(DoTouchpadReleased);
		}

		private void DoTouchpadPressed(object sender, ControllerInteractionEventArgs e)
		{
			
		}

		private void DoTouchpadReleased(object sender, ControllerInteractionEventArgs e)
		{
			Scene scene = SceneManager.GetActiveScene();

			Debug.Log("Reloading " + scene.name);
			SceneManager.LoadScene(scene.name);
		}
	}
}