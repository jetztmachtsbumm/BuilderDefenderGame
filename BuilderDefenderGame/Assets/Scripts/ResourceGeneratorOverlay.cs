using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{

    [SerializeField] private ResourceGenerator resourceGenerator;

    private Transform barTransform;

    private void Start()
    {
        barTransform = transform.Find("Bar");
    }

    private void Update()
    {
        barTransform.localScale = new Vector3(1 - resourceGenerator.GetTimerNormalized(), 1, 1);
    }

}
