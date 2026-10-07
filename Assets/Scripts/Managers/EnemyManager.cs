using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    [SerializeField] private int maxEnemySpawn = 8;
    [SerializeField] private int enemyPoolCount = 20;
    [SerializeField] private GameObject enemyGameObject;
    private List<GameObject> enemyPool = new List<GameObject>();

    float screenX;
    float screenY;
    float cameraSize;
    int currentRelativeSpawnCount = 1;

    // Start is called before the first frame update
    void Start() {
        cameraSize = Camera.main.orthographicSize;
        screenY = cameraSize;
        screenX = cameraSize * Camera.main.aspect;

        InitializePool();
        InvokeRepeating("SpawnNewEnenmy", 1, 5);
    }

    private void SpawnNewEnenmy() {
        int scoreRelativeSpawn = Mathf.RoundToInt(ScoreManager.Instance.GetCurrentScore / 5);
        int randomCount = currentRelativeSpawnCount + scoreRelativeSpawn;
        randomCount = Mathf.Clamp(randomCount, 1, maxEnemySpawn);

        for (int i = 0; i < randomCount; i++) {
            GameObject spawnObj = GetPooledEnemy();
            spawnObj.transform.position = GetRandomPosition();
            spawnObj.SetActive(true);
        }
    }


    private void InitializePool() {
        for (int i = 0; i < enemyPoolCount; i++) {
            GameObject newEnemy = CreateNewEnemy();
            enemyPool.Add(newEnemy);
        }
    }

    private GameObject CreateNewEnemy() {
        GameObject obj = Instantiate(enemyGameObject);
        obj.transform.SetParent(transform);
        obj.SetActive(false);
        return obj;
    }
    private GameObject GetPooledEnemy() {
        foreach (var enemy in enemyPool) {
            if (!enemy.activeInHierarchy) {
                return enemy;
            }
        }
        return null;
    }
    private Vector2 GetRandomPosition() {
        int side = Random.Range(0, 4);
        Vector2 spawnPos = Camera.main.transform.position;

        // Add offset to ensure it's outside the view
        float buffer = 2f;

        if (side == 0) spawnPos = new Vector2(screenX + buffer, Random.Range(-screenY, screenY));
        else if (side == 1) spawnPos = new Vector2((-screenX) - buffer, Random.Range(-screenY, screenY));
        else if (side == 2) spawnPos = new Vector2(Random.Range(-screenX, screenX), screenY + buffer);
        else spawnPos = new Vector2(Random.Range(-screenX, screenX), (-screenY) - buffer);

        return spawnPos;
    }

    void OnDrawGizmosSelected() {
        Vector2 boxSize = new Vector2(screenX, screenY);
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
