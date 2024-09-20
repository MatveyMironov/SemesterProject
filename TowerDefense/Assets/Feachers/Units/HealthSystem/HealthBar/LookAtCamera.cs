using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(transform.position - Camera.main.transform.position);
    }
}
