using System.Collections;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private Coroutine destroyRoutine = null;
    public int brickScore = 1;

    private void OnCollisionEnter(Collision other)
    {
        if (destroyRoutine != null) return;
        if (!other.gameObject.CompareTag("Ball")) return;
        destroyRoutine = StartCoroutine(DestroyWithDelay());
        ScoreManager.Instance.AddScore(brickScore);
    }

    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(0.1f); // two physics frames to ensure proper collision
        GameManager.Instance.OnBrickDestroyed(transform.position);
        Destroy(gameObject);
    }
}
