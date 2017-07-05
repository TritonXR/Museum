using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.Collections;
#endif

[RequireComponent (typeof (Rigidbody))]
[DisallowMultipleComponent]
public class ViveGrip_Grabbable : MonoBehaviour {
  public enum RotationMode { Disabled, ApplyGrip, ApplyGripAndOrientation }
  [System.Serializable]
  public class Position {
    [Tooltip("Should the grip connect to the Local Anchor position?")]
    public bool enabled = false;
    [Tooltip("The local position that will be gripped if enabled.")]
    public Vector3 localPosition = Vector3.zero;
  }
  [System.Serializable]
  public class Rotation {
    [Tooltip("The rotations that will be applied to a grabbed object.")]
    public RotationMode mode = RotationMode.ApplyGrip;
    [Tooltip("The local orientation that can be snapped to when grabbed.")]
    public Vector3 localOrientation = Vector3.zero;
  }
  public Position anchor;
  public Rotation rotation;
  private Vector3 grabCentre;

  void Start() {
    ViveGrip_Highlighter.AddTo(gameObject);
  }

    // These are called this on the scripts of the attached object and children of the controller:

    // Called when touched and moved away from, respectively
    //   void ViveGripTouchStart(ViveGrip_GripPoint gripPoint) {}
    //   void ViveGripTouchStop(ViveGrip_GripPoint gripPoint) {}

    // Called when touched and the grab button is pressed and released, respectively
    //   void ViveGripGrabStart(ViveGrip_GripPoint gripPoint) {}
    //   void ViveGripGrabStop(ViveGrip_GripPoint gripPoint) {}

    // Called when highlighting changes
    //   void ViveGripHighlightStart(ViveGrip_GripPoint gripPoint) {}
    //   void ViveGripHighlightStop(ViveGrip_GripPoint gripPoint) {}
  
    void ViveGripGrabStart(ViveGrip_GripPoint gripPoint) {
        if (gameObject.tag == "resizable")
        {
            gameObject.GetComponent<PlanePhysics>().enabled = false;

            gameObject.GetComponent<Resizable>().CloseInfoBox();
            gameObject.GetComponent<Resizable>().ToggleGrabbed(true);
            //Plane Physics
            gameObject.GetComponent<Rigidbody>().freezeRotation = false;
            gameObject.GetComponent<Rigidbody>().mass = 1f;
        }
    }
    public float throwThreshold;
    void ViveGripGrabStop(ViveGrip_GripPoint gripPoint) {
        Debug.Log("Released!");
        if (gameObject.tag == "resizable")
        {
            gameObject.GetComponent<Resizable>().ToggleGrabbed(false);
            //Plane Physics
            if (gameObject.GetComponent<Rigidbody>().velocity.magnitude >= throwThreshold)
            {
                Debug.Log("Magnitude: " + gameObject.GetComponent<Rigidbody>().velocity.magnitude);
                Debug.Log("Forward Vector Magnitude: " + (System.Math.Pow(gameObject.GetComponent<Rigidbody>().velocity.x, 2) + System.Math.Pow(gameObject.GetComponent<Rigidbody>().velocity.z, 2)));
                gameObject.GetComponent<PlanePhysics>().enabled = true; 
            
            }
           
        }
    }

    public void OnDrawGizmosSelected() {
    if (anchor != null && anchor.enabled) {
      Gizmos.DrawIcon(transform.position + RotatedAnchor(), "ViveGrip/anchor.png", true);
    }
  }

  public Vector3 RotatedAnchor() {
    return transform.rotation * anchor.localPosition;
  }

  public void GrabFrom(Vector3 jointLocation) {
    grabCentre = anchor.enabled ? anchor.localPosition : (jointLocation - transform.position);
  }

  public Vector3 WorldAnchorPosition() {
    return transform.position + (transform.rotation * grabCentre);
  }

  public bool ApplyGripRotation() {
    return rotation.mode != RotationMode.Disabled;
  }

  public bool SnapToOrientation() {
    return rotation.mode == RotationMode.ApplyGripAndOrientation;
  }
}
