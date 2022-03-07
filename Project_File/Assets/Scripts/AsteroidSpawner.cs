using UnityEngine;
public class AsteroidSpawner : MonoBehaviour {
    [Header("Spawn Configs")]
    [SerializeField] private float spawnRate;
    [SerializeField] private Vector3 randRange;
    [SerializeField] private Transform spawnRoot;
    [SerializeField] private GameObject[] asteroids;
    float nextFire = 0.0f;
    GameObject asteroidHolder;
    private void Start() { ObjectInit(); }
    void ObjectInit(){ asteroidHolder = new GameObject(); }
    private void Update() { SpawnObjectsPeriodically(); }
    void SpawnObjectsPeriodically() {
        if(Time.time > nextFire) {
            nextFire = Time.time + spawnRate + Random.Range(0, 0.3f);
            Vector3 randPos = new Vector3(spawnRoot.position.x + Random.Range(-randRange.x, randRange.x), spawnRoot.position.y, spawnRoot.position.z + Random.Range(-randRange.z, randRange.z));
            GameObject temp = Instantiate(asteroids[Random.Range(0, asteroids.Length)], randPos, Quaternion.identity, asteroidHolder.transform);
            temp.name = "Asteroid";
        }
    }

}