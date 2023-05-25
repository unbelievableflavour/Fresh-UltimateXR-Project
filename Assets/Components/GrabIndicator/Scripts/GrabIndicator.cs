using UnityEngine;

public class GrabIndicator : MonoBehaviour
{
    Transform mainCam;

    void Update()
    {
        AssignCamera();

        if (mainCam == null)
        {
            return;
        }

        transform.LookAt(mainCam);
    }

    public virtual void AssignCamera()
    {
        if (mainCam == null)
        {
            // Find By Tag instead of Camera.main as the camera could be disabled
            if (GameObject.FindGameObjectWithTag("MainCamera") != null)
            {
                mainCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
            }
        }
    }
}
