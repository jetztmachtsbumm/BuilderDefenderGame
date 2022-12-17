using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass
{

    private static Camera mainCamera;

    public static Vector3 GetMouseWorldPosition()
    {
        if(mainCamera == null) mainCamera = Camera.main;

        return mainCamera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
    }

    public static Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        return radians * Mathf.Rad2Deg;
    }
}
