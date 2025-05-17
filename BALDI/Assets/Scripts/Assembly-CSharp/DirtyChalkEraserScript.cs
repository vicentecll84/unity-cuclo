using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyChalkEraserScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        base.StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        float t = ps.duration + 1f;
        while (t > 0)
        {
            t -= Time.deltaTime;
            yield return null;
        }
        Destroy(base.gameObject);
    }
    [SerializeField]
    private ParticleSystem ps;
}
