using UnityEngine;
public class Asteroid : MonoBehaviour {
    [SerializeField] private float downwardSpeed;
    [SerializeField] private LayerMask layerOfInterest;
    [SerializeField] private GameObject AsteroidParticles;
    private void Start() { }
    private void Update() { MoveDownwards(); }
    void ObjectInit() { }
    void MoveDownwards() { transform.position += Vector3.down * downwardSpeed * Time.deltaTime; }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Road") || other.gameObject.CompareTag("Dirt")) {
            Instantiate(AsteroidParticles, transform.position, AsteroidParticles.transform.rotation, transform);
        }
    }
}