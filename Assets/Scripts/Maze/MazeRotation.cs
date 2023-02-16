using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRotation : MonoBehaviour
{
    [SerializeField] private float rotationLimit;
    private InputController inputController;
    private Vector2 direction;
    private Rigidbody rb;

    private void Awake()
    {
        inputController = new InputController();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        inputController.Enable();
    }

    private void OnDisable()
    {
        inputController.Disable();
    }

    private void Update()
    {
        direction = inputController.Maze.Rotate.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (!GameManager.IsCutScene)
        {
            Rotation();
        }
    }

    private void Rotation()
    {
        rb.MoveRotation(Quaternion.Euler(direction.y * rotationLimit * Time.fixedDeltaTime, 0, -direction.x * rotationLimit * Time.fixedDeltaTime));
    }
}
