using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedLifetime : MonoBehaviour
{

    [SerializeField]
    private float _lifetimeSeconds;

    private float _timeLeftSeconds;

    // Start is called before the first frame update
    private void Start()
    {
        _timeLeftSeconds = _lifetimeSeconds;
    }

    // Update is called once per frame
    private void Update()
    {
        _timeLeftSeconds -= Time.deltaTime;
        if (_timeLeftSeconds <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
