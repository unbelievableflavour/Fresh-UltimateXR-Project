using UnityEngine;
using UnityEngine.SceneManagement;
using UltimateXR.CameraUtils;

public class SceneTransitionManager : MonoBehaviour
{
    public Color fadeColor;
    private Camera mainCamera;

    public void SwitchToScene(int sceneIndex)
    {
        GetMainCamera();

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        void fadeOutFinishedCallback()
        {
            operation.allowSceneActivation = true;
        }

        UxrCameraFade.StartFade(mainCamera, 1.0f, 1.0f, fadeColor, fadeOutFinishedCallback, null);
    }

    private void GetMainCamera()
    {
        if (mainCamera)
        {
            return;
        }

        mainCamera = FindObjectOfType<Camera>();
    }
}
