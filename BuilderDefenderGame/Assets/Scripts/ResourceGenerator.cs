using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{

    private ResourceGeneratorData resourceGeneratorData;
    private float timer;
    private float timerMax;
    private int nearbyResourceAmount = 0;

    private void Awake()
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
        timerMax = resourceGeneratorData.timerMax;
    }

    private void Start()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, resourceGeneratorData.resourceDetectionRadius);

        foreach(Collider2D collider in collider2DArray)
        {
            ResourceNode resourceNode = collider.GetComponent<ResourceNode>();
            if(resourceNode != null)
            {
                if(resourceNode.resourceType == resourceGeneratorData.resourceType)
                {
                    nearbyResourceAmount++;
                }
            }
        }

        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);

        if(nearbyResourceAmount == 0)
        {
            enabled = false;
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            timer += timerMax;
            ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, nearbyResourceAmount);
        }
    }

}
