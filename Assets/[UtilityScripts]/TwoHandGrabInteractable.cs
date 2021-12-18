using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class TwoHandGrabInteractable : XRGrabInteractable
{

    [Header("Rotation Settings")]
    private Quaternion attachInitialRotation;
    private Quaternion initialRotationOffset;
    public List<XRSimpleInteractable> secondHandGrabPoints;
    public enum TwoHandRotationType { NONE, FIRST, SECOND }


    [Header("Grap Points Settings")]
    private XRBaseInteractor secondInteractor;
    [SerializeField] private readonly bool snapToSecondHand = true;
    [SerializeField] private TwoHandRotationType twoHandRotationType;

    private void Start()
    {
        foreach (var item in secondHandGrabPoints)
        {
            item.selectEntered.AddListener(OnSecondHandGrab);
            item.selectExited.AddListener(OnSecondHandRelease);
        }
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (secondInteractor && selectingInteractor)
        {
            if (snapToSecondHand)
                selectingInteractor.attachTransform.rotation = GetTwoHandRotation();

            else
                secondInteractor.attachTransform.rotation = GetTwoHandRotation() * initialRotationOffset;
        }

        base.ProcessInteractable(updatePhase);
    }

    private Quaternion GetTwoHandRotation()
    {
        Quaternion targetRotation;

        if (twoHandRotationType == TwoHandRotationType.NONE)
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position);
        }

        else if (twoHandRotationType == TwoHandRotationType.FIRST)
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, selectingInteractor.transform.up);
        }

        else
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, secondInteractor.transform.up);
        }

        return targetRotation;
    }

    public void OnSecondHandGrab(SelectEnterEventArgs args)
    {
        secondInteractor = args.interactor;
        initialRotationOffset = Quaternion.Inverse(GetTwoHandRotation()) * secondInteractor.attachTransform.rotation;
    }

    public void OnSecondHandRelease(SelectExitEventArgs args)
    {
        secondInteractor = null;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().PickedUpWeapon();
        attachInitialRotation = args.interactor.attachTransform.localRotation;
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        secondInteractor = null;
        args.interactor.attachTransform.localRotation = attachInitialRotation;
        base.OnSelectExited(args);
    }

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        var already = selectingInteractor && !interactor.Equals(selectingInteractor);

        return base.IsSelectableBy(interactor) && !already;
    }
}