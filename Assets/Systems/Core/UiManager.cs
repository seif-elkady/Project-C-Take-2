
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public Camera uiCamera;
    public Camera mainCamera;
    public Canvas mainCanvas;
    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
}
