using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(PullMeasurer))]
public class Notch : XRSocketInteractor
{
    [Range(0, 1)] public float releaseThreshold = 0.25f; // SETTINGS
    public PullMeasurer PullMeasurer { get; private set; } // NECESSARY STUFF
    public bool IsReady { get; private set; }
    private CustomInteractionManager CustomManager => interactionManager as CustomInteractionManager; // NEED TO CAST TO CUSTOM FOR FORCE DESELECT
    public override bool requireSelectExclusive => false; // THIS ENABLES THE SOCKET TO GRAB THE ARROW IMMEDIATELY
    public override XRBaseInteractable.MovementType? selectedInteractableMovementTypeOverride => XRBaseInteractable.MovementType.Instantaneous; // USE INSTANTANEOUS SO IT FOLLOWS SMOOTHLY

    protected override void Awake()
    {
        base.Awake();
        PullMeasurer = GetComponent<PullMeasurer>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        PullMeasurer.selectExited.AddListener(ReleaseArrow); // ARROW IS RELEASED ONCE THE PULLER IS RELEASED
        PullMeasurer.Pulled.AddListener(MoveAttach);  // MOVE THE POINT WHERE THE ARROW IS ATTACHED
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        PullMeasurer.selectExited.RemoveListener(ReleaseArrow);
        PullMeasurer.Pulled.RemoveListener(MoveAttach);
    }

    public void ReleaseArrow(SelectExitEventArgs args)
    {
        if (selectTarget is Arrow && PullMeasurer.PullAmount > releaseThreshold) CustomManager.ForceDeselect(this); // ONLY RELEASE IF THE TARGET IS AN ARROW USING CUSTOM DESELECT
    }

    public void MoveAttach(Vector3 pullPosition, float pullAmount)
    {
        attachTransform.position = pullPosition; // MOVE ATTACH WHEN BOW IS PULLED, THIS UPDATES THE RENDERER AS WELL
    }

    public void SetReady(BaseInteractionEventArgs args)
    {
        IsReady = args.interactable.isSelected; // SET THE NOTCH READY IF BOW IS SELECTED
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        // WE CHECK FOR THE HOVER HERE TOO, SINCE IT FACTORS IN THE RECYCLE TIME OF THE SOCKET
        // WE ALSO CHECK THAT NOTCH IS READY, WHICH IS SET ONCE THE BOW IS PICKED UP
        return base.CanSelect(interactable) && CanHover(interactable) && IsArrow(interactable) && IsReady;
    }

    private bool IsArrow(XRBaseInteractable interactable)
    {
        // SIMPLE ARROW CHECK, CAN BE TAG OR INTERACTION LAYER AS WELL
        return interactable is Arrow;
    }
}