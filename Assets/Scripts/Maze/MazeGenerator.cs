using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator 
{
    public MazeGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    private int width;
    private int height;

    public Cell[,] GenerateMaze()
    {
        Cell[,] maze = new Cell[width, height];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int z = 0; z < maze.GetLength(1); z++)
            {
                maze[x, z] = new Cell { X = x, Z = z };
            }
        }

        RemoveWalls(maze);
        BrokeRandomWalls(maze);

        return maze;
    }

    private void RemoveWalls(Cell[,] maze)
    {
        RemoveEdgeWalls(maze);
        Cell currentCell = maze[0, 0];
        currentCell.distanceFromStart = 0;
        currentCell.isVisitedCell = true;

        Stack<Cell> stackCell = new Stack<Cell>();
        do
        {
            List<Cell> unvisitedCells = new List<Cell>();

            int x = currentCell.X;
            int y = currentCell.Z;

            if (x > 0 && !maze[x - 1, y].isVisitedCell) unvisitedCells.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].isVisitedCell) unvisitedCells.Add(maze[x, y - 1]);
            if (x < width - 2 && !maze[x + 1, y].isVisitedCell) unvisitedCells.Add(maze[x + 1, y]);
            if (y < height - 2 && !maze[x, y + 1].isVisitedCell) unvisitedCells.Add(maze[x, y + 1]);

            if (unvisitedCells.Count > 0)
            {
                Cell chosenCell = unvisitedCells[Random.Range(0, unvisitedCells.Count)];
                RemoveCurrentWall(currentCell, chosenCell);
                chosenCell.isVisitedCell = true;
                stackCell.Push(chosenCell);
                chosenCell.distanceFromStart = stackCell.Count;
                currentCell = chosenCell;
            }
            else
            {
                currentCell = stackCell.Pop();
            }
        }
        while (stackCell.Count > 0);
    }

    private void RemoveCurrentWall(Cell current, Cell chosen)
    {
        if (current.X == chosen.X)
        {
            if (current.Z > chosen.Z) current.isWallDownOn = false;
            else chosen.isWallDownOn = false;
        }
        else
        {
            if (current.X > chosen.X) current.isWallLeftOn = false;
            else chosen.isWallLeftOn = false;
        }
    }

    private void RemoveEdgeWalls(Cell[,] maze) //удаляет лишние стены за пределом игрового лабиринта
    {
        for (int x = 0; x < width; x++)
        {
            maze[x, height - 1].isWallLeftOn = false;
            maze[x, height - 1].isFloorOn = false;
        }

        for (int y = 0; y < height; y++)
        {
            maze[width - 1, y].isWallDownOn = false;
            maze[width - 1, y].isFloorOn = false;
        }
    }

    private void BrokeRandomWalls(Cell[,] maze) // ломаем рандомные стены, чтобы запутать лабиринт
    {
        const int coeff = 20;
        for (int i = 0; i < (width * height) / coeff; i++)
        {
            int x = Random.Range(1, width - 1);
            int y = Random.Range(1, height - 1);
             
            maze[x, y].isWallLeftOn = false;               
            maze[x, y].isWallDownOn = false;                            
        }
    }
}
