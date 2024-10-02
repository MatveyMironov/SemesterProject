using System;

namespace ConstructionSystem
{
    public class ConstructionBlueprint
    {
        public TurretDataSO TurretData { get; private set; }

        public ConstructionBlueprint(TurretDataSO turretData)
        {
            TurretData = turretData;
        }

        public event Action<ConstructionBlueprint> OnBlueprintSelectionCalled;

        public void CallBlueprintSelection()
        {
            OnBlueprintSelectionCalled?.Invoke(this);
        }
    }
}
