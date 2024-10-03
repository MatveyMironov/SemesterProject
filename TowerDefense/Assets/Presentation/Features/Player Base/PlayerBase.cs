using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private GameObject baseObject;

    public void DestroyBase()
    {
        Destroy(baseObject);
    }
}
