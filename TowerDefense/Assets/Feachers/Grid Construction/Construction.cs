using System;
using UnityEngine;

public class Construction : MonoBehaviour
{
    [SerializeField] private ConstructionPreview _constructionPreview = new();
    [SerializeField] private BuildingRemovement _buildingRemovement = new();
    [SerializeField] private Resource _resource = new();

    private GridData _buildingsData = new();

    private IConstructionState _constructionState;

    [Space]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Grid grid;

    [Space]
    [SerializeField] private GameObject gridVisualization;

    public static event Action OnConstructionModeEntered;
    public static event Action OnConstructionModeExited;

    private Vector3Int _lastGridPosition = Vector3Int.zero;

    private bool _isConstructionModeEntered;

    private void Awake()
    {
        _constructionPreview.PrepareConstructionPreview();
    }

    private void Start()
    {
        ExitConstructionMode();
    }

    private void Update()
    {
        if (_constructionState == null)
            return;

        Vector3 mousePosition = inputManager.GetSelectedMapposition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        if (_lastGridPosition != gridPosition)
        {
            _constructionState.UpdateState(gridPosition);

            _lastGridPosition = gridPosition;
        }
    }

    private void ToggleConstruction()
    {
        if (_isConstructionModeEntered)
        {
            ExitConstructionMode();
        }
        else
        {
            EnterConstructionMode();
        }
    }

    public void EnterConstructionMode()
    {
        gridVisualization.SetActive(true);

        OnConstructionModeEntered?.Invoke();

        _isConstructionModeEntered = true;
    }

    public void ExitConstructionMode()
    {
        AbortConstructionAction();

        gridVisualization.SetActive(false);

        OnConstructionModeExited?.Invoke();

        _isConstructionModeEntered = false;
    }

    public void StartPlacement(ConstructionBlueprintSO blueprint)
    {
        if (!_isConstructionModeEntered)
            return;

        AbortConstructionAction();

        _constructionState = new PlacingState(grid, _constructionPreview, blueprint, _buildingsData, _resource);
    }

    public void StartRemovement()
    {
        if (!_isConstructionModeEntered)
            return;

        AbortConstructionAction();

        _constructionState = new RemovingState(_constructionPreview, _buildingsData, _buildingRemovement);
    }

    public void ExecuteConstructionAction()
    {
        if (_constructionState == null)
            return;

        if (inputManager.IsOverUI())
            return;

        Vector3 mousePosition = inputManager.GetSelectedMapposition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        _constructionState.OnAction(gridPosition);
    }

    public void AbortConstructionAction()
    {
        if (_constructionState == null)
            return;

        _constructionState.EndState();
        _constructionState = null;

        _lastGridPosition = Vector3Int.zero;
    }

    private void OnEnable()
    {
        InputListener.OnConstructionToggled += ToggleConstruction;

        InputListener.OnLeftMouseClicked += ExecuteConstructionAction;
        InputListener.OnRightMouseClicked += AbortConstructionAction;
    }

    private void OnDisable()
    {
        InputListener.OnConstructionToggled -= ToggleConstruction;

        InputListener.OnLeftMouseClicked -= ExecuteConstructionAction;
        InputListener.OnRightMouseClicked -= AbortConstructionAction;
    }
}
