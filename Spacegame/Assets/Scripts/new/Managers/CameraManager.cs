using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    enum VirtualCameras
    {
        NoCamera = -1,
        CockpitCamera = 0,
        FollowCamera = 1,
        enemyFollowCamera = 2;
        
    }
    [SerializeField]
    List<GameObject> virtualCamera;

    VirtualCameras CameraKeyPressed
    {
        get
        {
            for (int i = 0; i < virtualCamera.Count; i++) 
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i)) return (VirtualCameras)i;
            }
            return VirtualCameras.NoCamera;
        }
        
    }
    
    void Start()
    {
        SetActiveCamera(VirtualCameras.CockpitCamera);
    }

    
    void Update()
    {
        SetActiveCamera(CameraKeyPressed);
    }

    void SetActiveCamera(VirtualCameras activeCamera)
    {
        if (activeCamera == VirtualCameras.NoCamera)
        {
            
            return;
        }

       
        foreach (GameObject cam in virtualCamera) 
        {
            cam.SetActive(cam.tag.Equals(activeCamera.ToString()));
        }
    }
}
