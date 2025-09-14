using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraSettings : MonoBehaviour
{
    private Transform cameraPos;
    [SerializeField] private Transform playerPos;
    private void Start()
    {
        cameraPos = GetComponent<Camera>().transform;
    }
    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 newPos = new Vector3(playerPos.position.x, playerPos.position.y, cameraPos.position.z);
        transform.position = newPos;
    }
}
