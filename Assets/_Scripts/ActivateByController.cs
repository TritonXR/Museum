namespace VRTK.Examples
{
    using UnityEngine;
    using System.Collections;

    public class ActivateByController : VRTK_InteractableObject
    {
        public AudioClip myClip;
        private GameObject objActivate;
        
        protected override void Start()
        {
            objActivate = GetComponentInChildren<VRTK_ObjectTooltip>().gameObject;
            objActivate.SetActive(false);

            this.gameObject.AddComponent<AudioSource>();
            this.GetComponent<AudioSource>().clip = myClip;

        }

        public override void StartUsing(GameObject currentUsingObject)
        {
            Debug.Log("ACTIVATE");
            base.StartUsing(currentUsingObject);
            objActivate.SetActive(true);
        }

        public override void StopUsing(GameObject previousUsingObject)
        {
            base.StopUsing(previousUsingObject);
            objActivate.SetActive(false);
        }
    }
}
