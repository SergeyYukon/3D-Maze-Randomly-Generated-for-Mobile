using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera cm;
    private CinemachineTransposer transposer;

    private void Awake()
    {
        cm = GetComponent<CinemachineVirtualCamera>();
        transposer = cm.GetCinemachineComponent<CinemachineTransposer>();
        EventController.OnSetWidthHeightEvent += SetCameraOffset;
    }

    private void SetCameraOffset()
    {
        int size;
        if (MazeSpawner.WidthMaze >= MazeSpawner.HeightMaze) size = MazeSpawner.WidthMaze;
        else size = MazeSpawner.HeightMaze;

        int cameraOffsetY = size * 10;
        int cameraOffsetX = -(cameraOffsetY / 4);
        transposer.m_FollowOffset = new Vector3(cameraOffsetX, cameraOffsetY, transposer.m_FollowOffset.z);
    }
}
