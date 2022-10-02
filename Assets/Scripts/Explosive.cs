using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField]
    private AudioClip _explosionSound;

    private AudioManager _audioManager;

    private Animator _animator;

    [SerializeField]
    private float _explosionRadius;

    // Start is called before the first frame update
    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _animator = GetComponent<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var projectileComponent = col.gameObject.GetComponent<Projectile>();
        // Only include player shots
        if (projectileComponent != null && projectileComponent.OwnerTag == "Player")
        {
            _animator.SetBool("explosionTriggered", true);
            Destroy(col.gameObject);
        }
    }

    public void Explode()
    {

        _audioManager.Play(_explosionSound);
        foreach (var health in FindObjectsOfType<Health>())
        {
            Debug.Log("Sup");
            if (Vector3.Distance(health.transform.position, transform.position) < _explosionRadius)
            {
                health.DecreaseLife();
            }
        }
    }

    public void ExplosionEnded()
    {
        Destroy(gameObject);
    }

}
