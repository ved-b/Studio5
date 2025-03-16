using System.Collections;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private Coroutine destroyRoutine = null;
    [SerializeField] private GameObject hitParticlePrefab;

    public int brickScore = 1;



    private void OnCollisionEnter(Collision other)
    {
        if (destroyRoutine != null) return;
        if (!other.gameObject.CompareTag("Ball")) return;

        ContactPoint contact = other.contacts[0];
        Quaternion rotation = Quaternion.LookRotation(contact.normal);
        Instantiate(hitParticlePrefab, transform.position, rotation);

        destroyRoutine = StartCoroutine(DestroyWithDelay());
    }

    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(0.1f); // two physics frames to ensure proper collision
        GameManager.Instance.OnBrickDestroyed(transform.position);
        Destroy(gameObject);
    }
}
