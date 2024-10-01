using UnityEngine;

public class PreviewSystem
{
    private GameObject _previewObject;

    public void CreatePreview(GameObject previewPrefab)
    {
        DestroyPreview();
        _previewObject = Object.Instantiate(previewPrefab);
    }

    public void ShowPreview(Vector3 position, Quaternion rotation)
    {
        _previewObject.transform.position = position;
        _previewObject.transform.rotation = rotation;
        _previewObject.SetActive(true);
    }

    public void HidePreview()
    {
        _previewObject.SetActive(false);
    }

    public void DestroyPreview()
    {
        Object.Destroy(_previewObject);
    }
}
