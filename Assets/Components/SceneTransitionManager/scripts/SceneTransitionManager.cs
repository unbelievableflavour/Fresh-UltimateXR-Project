using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UltimateXR.CameraUtils;
using System;

public class SceneTransitionManager : MonoBehaviour
{
    public Color fadeColor;
    private Camera mainCamera;

    public void SwitchToScene(int sceneIndex)
    {
        getMainCamera();

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        Action fadeOutFinishedCallback = ( ) => {
            operation.allowSceneActivation = true;
        };

        UxrCameraFade.StartFade(mainCamera, 1.0f,1.0f, fadeColor, fadeOutFinishedCallback, null);
    }

    void getMainCamera()
    {
        if(mainCamera) {
            return;
        }
        mainCamera = GameObject.FindObjectOfType<Camera>();
    }
}
