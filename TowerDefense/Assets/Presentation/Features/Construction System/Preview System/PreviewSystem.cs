using UnityEngine;

public class PreviewSystem
{
    private GameObject _previewObject;

    public void CreatePreview(GameObject previewPrefab)
    {
        DestroyPreview();
        _previewObject = Object.Instantiate(previewPrefab);
    }

    public void ShowPreview(ConstructionSite constructionSite)
    {
        _previewObject.transform.position = constructionSite.transform.position;
        _previewObject.transform.rotation = constructionSite.transform.rotation;
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
