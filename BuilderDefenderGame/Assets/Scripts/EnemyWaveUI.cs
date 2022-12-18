using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyWaveUI : MonoBehaviour
{

    [SerializeField] private EnemyWaveManager enemyWaveManager;

    private Camera mainCamera;
    private TextMeshProUGUI waveMessageText;
    private TextMeshProUGUI waveNumberText;
    private RectTransform enemyWaveSpawnPositionindicator;
    private RectTransform enemyClosestPositionindicator;

    private void Awake()
    {
        waveMessageText = transform.Find("WaveMessageText").GetComponent<TextMeshProUGUI>();
        waveNumberText = transform.Find("WaveNumberText").GetComponent<TextMeshProUGUI>();
        enemyWaveSpawnPositionindicator = transform.Find("EnemyWaveSpawnPositionIndicator").GetComponent<RectTransform>();
        enemyClosestPositionindicator = transform.Find("EnemyClosestPositionIndicator").GetComponent<RectTransform>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
        SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber());
    }

    private void EnemyWaveManager_OnWaveNumberChanged(object sender, System.EventArgs e)
    {
        SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber());
    }

    private void Update()
    {
        HandleNextWaveMessage();
        HandleEnemyWaveSpawnPositionIndicator();
        HandleEnemyClosestPositionIndicator();
    }

    private void HandleNextWaveMessage()
    {
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
        if (nextWaveSpawnTimer <= 0)
        {
            waveMessageText.gameObject.SetActive(false);
        }
        else
        {
            SetMessageText("Next Wave in " + nextWaveSpawnTimer.ToString("F1") + "s");
        }
    }

    private void HandleEnemyWaveSpawnPositionIndicator()
    {
        Vector3 dirToNextSpawnPosition = (enemyWaveManager.GetSpawnPosition() - mainCamera.transform.position).normalized;

        enemyWaveSpawnPositionindicator.anchoredPosition = dirToNextSpawnPosition * 300f;
        enemyWaveSpawnPositionindicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToNextSpawnPosition));

        float distanceToNextSpawnPosition = Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
        enemyWaveSpawnPositionindicator.gameObject.SetActive(distanceToNextSpawnPosition > mainCamera.orthographicSize * 1.5f);
    }

    private void HandleEnemyClosestPositionIndicator()
    {
        Enemy closestEnemy = GetClosestEnemy();

        if (closestEnemy != null)
        {
            Vector3 dirToClosestEnemy = (closestEnemy.transform.position - mainCamera.transform.position).normalized;

            enemyClosestPositionindicator.anchoredPosition = dirToClosestEnemy * 250f;
            enemyClosestPositionindicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToClosestEnemy));

            float distanceToClosestEnemy = Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
            enemyClosestPositionindicator.gameObject.SetActive(distanceToClosestEnemy > mainCamera.orthographicSize * 1.5f);
        }
        else
        {
            enemyClosestPositionindicator.gameObject.SetActive(false);
        }
    }

    private void SetMessageText(string message)
    {
        waveMessageText.gameObject.SetActive(true); 
        waveMessageText.SetText(message);
    }

    private void SetWaveNumberText(string text)
    {
        waveNumberText.SetText(text);
    }

    private Enemy GetClosestEnemy()
    {
        float targetMaxRadius = 9999f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(mainCamera.transform.position, targetMaxRadius);

        Enemy closestEnemy = null;
        foreach (Collider2D collider2D in collider2DArray)
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (closestEnemy == null)
                {
                    closestEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, closestEnemy.transform.position))
                    {
                        closestEnemy = enemy;
                    }
                }
            }
        }

        return closestEnemy;
    }

}
