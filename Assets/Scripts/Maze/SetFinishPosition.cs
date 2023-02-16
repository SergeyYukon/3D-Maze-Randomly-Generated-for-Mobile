using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class SetFinishPosition
{
    public SetFinishPosition(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    private int width;
    private int height;
    private enum Algorithm : byte { wallFinishAlgorithm, farthestFinishAlgorithm, noWallFinishAlgorithm, rightUpFinishAlgorithm }
    private Algorithm algorithm;

    public Vector2 FinishCell(Cell[,] maze)
    {
        Cell farthest = maze[0, 0];
        algorithm = ChooseRandomAlgorithm();

        switch (algorithm)
        {
            case Algorithm.wallFinishAlgorithm:
                WallFinishAlgorithm(maze, ref farthest);
                break;
            case Algorithm.farthestFinishAlgorithm:
                FarthestFinishAlgorithm(maze, ref farthest);
                break;
            case Algorithm.noWallFinishAlgorithm:
                NoWallFinishAlgorithm(maze, ref farthest);
                break;
            case Algorithm.rightUpFinishAlgorithm:
                RightUpFinishAlgorithm(maze, ref farthest);
                break;
        }
        return new Vector2(farthest.X, farthest.Z);
    }

    private Algorithm ChooseRandomAlgorithm()
    {
        int algorithmCount = Enum.GetNames(typeof(Algorithm)).Length;
        int random = Random.Range(0, algorithmCount);
        return (Algorithm)random;
    }

    private void WallFinishAlgorithm(Cell[,] maze, ref Cell farthest) // самый дальний путь у стен лабиринта   
    {
        for (int x = 0; x < maze.GetLength(0); x++)            
        {
            if (maze[x, height - 1].distanceFromStart > farthest.distanceFromStart) farthest = maze[x, height - 1];
            if (maze[x, 0].distanceFromStart > farthest.distanceFromStart) farthest = maze[x, 0];
        }
        for (int y = 0; y < maze.GetLength(1); y++)
        {
            if (maze[width - 1, y].distanceFromStart > farthest.distanceFromStart) farthest = maze[width - 1, y];
            if (maze[0, y].distanceFromStart > farthest.distanceFromStart) farthest = maze[0, y];
        }
    }

    private void FarthestFinishAlgorithm(Cell[,] maze, ref Cell farthest) // самый дальний путь в любом месте лабиринта 
    {
        for (int x = 0; x < maze.GetLength(0); x++)                
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                if (maze[x, y].distanceFromStart > farthest.distanceFromStart) farthest = maze[x, y];
            }
        }
    }

    private void NoWallFinishAlgorithm(Cell[,] maze, ref Cell farthest) // самый дальний путь не у стен лабиринта   
    {
        for (int x = 1; x < maze.GetLength(0) - 2; x++)            
        {
            for (int y = 1; y < maze.GetLength(1) - 2; y++)
            {
                if (maze[x, y].distanceFromStart > farthest.distanceFromStart) farthest = maze[x, y];
            }
        }
    }

    private void RightUpFinishAlgorithm(Cell[,] maze, ref Cell farthest) // правый верхний угол
    {
        farthest = maze[width - 1, height - 1]; 
    }
}
