using System;
using System.Collections.Generic;
using UnityEngine;

namespace ConstructionSystem
{
    public class ConstructionManager : MonoBehaviour
    {
        [ContextMenuItem("Reset Available Turrets", "ResetAvailableBlueprints")]
        [SerializeField] private List<TurretDataSO> defaultTurrets = new();

        [Space]
        [SerializeField] private Construction construction;
        [SerializeField] private ConstructionMenu constructionMenu;

        private List<ConstructionBlueprint> _availableBlueprints = new();

        public bool IsConstructionModeEntered { get; private set; }

        private void Start()
        {
            ExitConstructionMode();

            foreach (var turret in defaultTurrets)
            {
                AddTurret(turret);
            }
        }

        public void EnterConstructionMode()
        {
            construction.ShowConstructionSites();
            constructionMenu.OpenMenu();
            IsConstructionModeEntered = true;
        }

        public void ExitConstructionMode()
        {
            construction.HideConstructionSites();
            construction.AbortBuilding();
            constructionMenu.CloseMenu();
            IsConstructionModeEntered = false;
        }

        public void SelectConstructionSite()
        {
            construction.SelectConstructionSite();
        }

        public void SelectBlueprint(ConstructionBlueprint blueprint)
        {
            construction.SelectTurret(blueprint.TurretData);
        }

        #region Available Blueprints
        public void AddTurret(TurretDataSO turretData)
        {
            if (_availableBlueprints.Exists(blueprint => blueprint.TurretData == turretData))
            {
                throw new Exception("Available blueprints already contain blueprint of such turret");
            }

            ConstructionBlueprint blueprint = new(turretData);
            blueprint.OnBlueprintSelectionCalled += SelectBlueprint;
            constructionMenu.AddConstructionButton(blueprint);
            _availableBlueprints.Add(blueprint);
        }

        public void RemoveTurret(TurretDataSO turretData)
        {
            ConstructionBlueprint blueprint = _availableBlueprints.Find(blueprint => blueprint.TurretData == turretData);

            if (blueprint == null)
            {
                throw new Exception("Available blueprints don't contain blueprint of such turret");
            }

            RemoveBlueprint(blueprint);
        }

        public void RemoveBlueprint(ConstructionBlueprint blueprint)
        {
            constructionMenu.RemoveConstructionButton(blueprint);
            _availableBlueprints.Remove(blueprint);
        }

        public void ResetAvailableBlueprints()
        {
            for (int i = _availableBlueprints.Count - 1; i >= 0; i--)
            {
                RemoveBlueprint(_availableBlueprints[i]);
            }

            foreach (var turret in defaultTurrets)
            {
                AddTurret(turret);
            }
        }
        #endregion
    }
}
