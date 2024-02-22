using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class ARObjectPlacementScript : MonoBehaviour
{
    public ARSessionOrigin ar_session_origin;
    public GameObject cube;
    public List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
    GameObject instantiatedCube;

    public Touch touch;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // detect users touch on screen
        if (Input.GetMouseButton(0))
        {
            // project a raycatst
            bool collision = ar_session_origin.GetComponent<ARRaycastManager>().Raycast(Input.mousePosition, raycastHits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);

            if (collision)
            {
                if (instantiatedCube == null) { 
                    instantiatedCube = Instantiate(cube); 
                    foreach (var plane in ar_session_origin.GetComponent<ARPlaneManager>().trackables) {
                        plane.gameObject.SetActive(false);
                    }
                    ar_session_origin.GetComponent<ARPlaneManager>().enabled = false;
                    }
                instantiatedCube.transform.position = raycastHits[0].pose.position;
            }

        }
        //  instantiate a virtual cube at the point where raycast meets the detected plane
    }
}
