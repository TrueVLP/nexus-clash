using UnityEngine;

public class FPS_Limit : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
