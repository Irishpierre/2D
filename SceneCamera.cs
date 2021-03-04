using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SceneCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        //Set cinemachineVC.Target = Player.Instance
        cinemachineVirtualCamera.Follow = Player.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
