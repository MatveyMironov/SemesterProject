using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithDelay : MonoBehaviour
{
    [SerializeField] private int delay = 1;

    IEnumerator Destruction()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(Destruction());
    }
}
