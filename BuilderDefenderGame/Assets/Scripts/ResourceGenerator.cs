using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{

    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
    {
        int nearbyResourceAmount = 0;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);

        foreach (Collider2D collider in collider2DArray)
        {
            ResourceNode resourceNode = collider.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                if (resourceNode.resourceType == resourceGeneratorData.resourceType)
                {
                    nearbyResourceAmount++;
                }
            }
        }

        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
        return nearbyResourceAmount;
    }

    private ResourceGeneratorData resourceGeneratorData;
    private float timer;
    private float timerMax;

    private void Awake()
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
        timerMax = resourceGeneratorData.timerMax;
    }

    private void Start()
    {
        if(GetNearbyResourceAmount(resourceGeneratorData, transform.position) == 0)
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
            ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, GetNearbyResourceAmount(resourceGeneratorData, transform.position));
        }
    }

    public float GetTimerNormalized()
    {
        return timer / timerMax;
    }

}
