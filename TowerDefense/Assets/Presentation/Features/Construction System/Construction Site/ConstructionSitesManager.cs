using UnityEngine;


public class ConstructionSitesManager : MonoBehaviour
{
    [SerializeField] private ConstructionSite[] constructionSites = new ConstructionSite[0];

    public ConstructionSite SelectedSite { get; private set; }

    public void ShowConstructionSites()
    {
        foreach (var constructionSite in constructionSites)
        {
            if (constructionSite != null)
            {
                constructionSite.ShowConstructionSite();
            }
        }
    }

    public void HideConstructionSites()
    {
        foreach (var constructionSite in constructionSites)
        {
            if (constructionSite != null)
            {
                constructionSite.HideConstructionSite();
            }
        }
    }
}
