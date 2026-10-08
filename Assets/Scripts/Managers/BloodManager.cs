using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodManager : MonoBehaviour {
    public static BloodManager Instance;
    [SerializeField] private int maxAmountofBloodPool;
    [SerializeField] private int maxAmountofGib;
    [Space]
    [SerializeField] private Transform bloodPoolParent;
    [SerializeField] private Transform gibPoolParent;
    [Space]
    [SerializeField] private GameObject bloodPoolPrefab;
    [SerializeField] private GameObject gibPrefab;

    private List<GameObject> bloodPools = new List<GameObject>();
    private List<GameObject> gibPool = new List<GameObject>();


    // Start is called before the first frame update
    void Start() {
        Instance = this;
        InitializeBloodPool();
        InitializeGibPool();
    }

    void InitializeBloodPool() {
        for (int i = 0; i < maxAmountofBloodPool; i++) {
            GameObject obj = Instantiate(bloodPoolPrefab);
            obj.SetActive(false);
            obj.transform.SetParent(bloodPoolParent);
            bloodPools.Add(obj);
        }
    }
    void InitializeGibPool() {
        for (int i = 0; i < maxAmountofGib; i++) {
            GameObject obj = Instantiate(gibPrefab);
            obj.SetActive(false);
            obj.transform.SetParent(gibPoolParent);
            gibPool.Add(obj);
        }
    }

    public void SpawnBloodPool(Vector2 pos) {
        GameObject obj = GetPooledBloodPool();
        if (obj == null) return;

        obj.transform.position = pos;
        obj.gameObject.SetActive(true);
        if (obj.TryGetComponent<ParticleSystem>(out ParticleSystem ps)) {
            ps.Play();
        }
    }
    public void SpawnGib(Vector2 pos) {
        GameObject obj = GetPooledGibPool();
        if (obj == null) return;

        obj.transform.position = pos;
        obj.gameObject.SetActive(true);
        if (obj.TryGetComponent<ParticleSystem>(out ParticleSystem ps)) {
            ps.Play();
        }
    }

    GameObject GetPooledBloodPool() {
        foreach (GameObject item in bloodPools) {
            if (!item.gameObject.activeInHierarchy) {
                return item;
            }
        }
        return null;
    }
    GameObject GetPooledGibPool() {
        foreach (GameObject item in gibPool) {
            if (!item.gameObject.activeInHierarchy) {
                return item;
            }
        }
        return null;
    }
}
