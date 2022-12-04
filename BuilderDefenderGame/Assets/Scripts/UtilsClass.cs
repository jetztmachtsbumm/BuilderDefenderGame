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

}
