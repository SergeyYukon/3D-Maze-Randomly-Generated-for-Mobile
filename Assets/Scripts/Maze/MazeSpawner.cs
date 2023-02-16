using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private int minSize;
    [SerializeField] private int maxSize;
    [SerializeField] private GameObject cell;
    [SerializeField] private GameObject finish;
    [SerializeField] private GameObject bonusTime;
    [SerializeField] private Transform centerMaze;
    [SerializeField] private Vector3 cellSize; // указать размер €чейки

    private Vector2 finishPosition;
    public static int WidthMaze { get; private set; }
    public static int HeightMaze { get; private set; }

    private void Start()
    {
        SetSizeMaze();
        SpawnMaze();
    }

    private void SpawnMaze()
    {
        MazeGenerator generator = new MazeGenerator(WidthMaze + 1, HeightMaze + 1);
        Cell[,] maze = generator.GenerateMaze();

        SetFinish(maze);
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int z = 0; z < maze.GetLength(1); z++)
            {
                CellObjects walls = Instantiate(cell, new Vector3(x * cellSize.x, 0, z * cellSize.z), Quaternion.identity, transform).GetComponent<CellObjects>();

                SpawnBonusTime(x, z);

                walls.wallLeft.SetActive(maze[x, z].isWallLeftOn);
                walls.wallDown.SetActive(maze[x, z].isWallDownOn);
                walls.floor.SetActive(maze[x, z].isFloorOn);
            }
        }
        PivotToCenter();
    }

    private void PivotToCenter()
    {
        centerMaze.position = new Vector3(WidthMaze * cellSize.x / 2, 0, HeightMaze * cellSize.z / 2);
        centerMaze.localScale = new Vector3(WidthMaze, transform.localScale.y, HeightMaze);
        gameObject.transform.SetParent(centerMaze);
    }

    private void SetFinish(Cell[,] maze)
    {
        SetFinishPosition setFinish = new SetFinishPosition(WidthMaze, HeightMaze);
        const float offset = 5;
        finishPosition  = setFinish.FinishCell(maze);
        Vector3 finishCell = new Vector3(finishPosition.x * cellSize.x + offset, offset, finishPosition.y * cellSize.z + offset);
        Instantiate(finish, finishCell, Quaternion.identity, transform);
    }

    private void SpawnBonusTime(int x, int z)
    {
        const float offset = 5;
        Vector2 cell = new Vector2(x, z);
        if(x < WidthMaze && z < HeightMaze && cell != Vector2.zero && cell != finishPosition)
        Instantiate(bonusTime, new Vector3(x * cellSize.x + offset, offset / 2, z * cellSize.z + offset), Quaternion.identity, transform);
    }

    private void SetSizeMaze()
    {
        if(minSize != maxSize) minSize += GameManager.Level;
        int size = minSize;
        const int coeff = 3;
        WidthMaze = Random.Range(minSize / coeff, size - (minSize / coeff) + 1);
        HeightMaze = size - WidthMaze;
        EventController.OnSetWidthHeight();
    }
}
