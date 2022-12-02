using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private Transform pfWoodHarvester;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(pfWoodHarvester, GetMouseWorldPosition(), Quaternion.identity);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
    }

}
