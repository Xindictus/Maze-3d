using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Constants
{
  public const int PORT = 6610;
  public const int UPPER_BOUND = 30;
  public const int LOWER_BOUND = -30;
  public static Quaternion ball_init_rot = new Quaternion(0f, 0f, 0f, 0f);
  public const int MAX_NO_DATA = 5000;

  public readonly struct InitBallPosResult
  {
    public Vector3 Pos { get; }
    public int Index { get; }
    public InitBallPosResult(Vector3 pos, int index) => (Pos, Index) = (pos, index);
  }

  static List<Vector3> ball_init_pos = new List<Vector3>()
    {
        new Vector3(-0.19f, 0.025f, 0.19f), //upper right corner
        new Vector3(0.1f, 0.025f, 0.19f), //bottom right corner
        new Vector3(-0.19f, 0.025f, -0.1f) //upper left corner
        // new Vector3(-0.12f, 0.025f, -0.19f), //bellow upper obstacle
        // new Vector3(0.19f, 0.025f, 0.12f) //bellow lower obstacle
    };

  static int currentPosIndex = 0;

  public static InitBallPosResult get_ball_init_pos()
  {
    if (ball_init_pos.Count == 0)
    {
      Debug.LogError("No initial positions available.");
      return new InitBallPosResult(Vector3.zero, -1); // Default position
    }

    Vector3 currentPosition = ball_init_pos[currentPosIndex];
    currentPosIndex = (currentPosIndex + 1) % ball_init_pos.Count;
    return new InitBallPosResult(currentPosition, currentPosIndex);
  }

  public static Vector3 get_ball_test_init_pos()
  {
    Vector3 v0 = ball_init_pos[0];
    Vector3 v1 = ball_init_pos[1];
    Vector3 v2 = ball_init_pos[2];

    float u = UnityEngine.Random.value;
    float v = UnityEngine.Random.value;

    // Ensure that u and v lie within the triangle
    if (u + v > 1)
    {
      u = 1 - u;
      v = 1 - v;
    }

    // Calculate the point using barycentric coordinates
    Vector3 randomPoint = v0 + u * (v1 - v0) + v * (v2 - v0);
    return randomPoint;
  }
}