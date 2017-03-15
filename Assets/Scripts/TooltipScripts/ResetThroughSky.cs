
namespace VRTK.Examples
{
    using UnityEngine;
    using System.Collections;

    public class ResetThroughSky : VRTK_InteractableObject
    {

        public override void StartUsing(GameObject currentUsingObject)
        {
            Debug.Log("ACTIVATE");
            base.StartUsing(currentUsingObject);

        }

        public override void StopUsing(GameObject previousUsingObject)
        {
            base.StopUsing(previousUsingObject);
            Application.LoadLevel("HalfScaleTest");
        }
    }
}
