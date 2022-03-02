using UnityEngine;
public class AvoidOverlapping : MonoBehaviour {
    [SerializeField] float objectDetectionRadius;
    [SerializeField] float detectDistance;
    [SerializeField] LayerMask layerOfInterest;
    GameObject hitResult;
}