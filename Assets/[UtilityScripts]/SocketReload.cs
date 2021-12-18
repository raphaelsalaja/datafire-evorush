using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public partial class SocketReload : XRSocketInteractor
{
    public string targetTag = string.Empty;
    public XRBaseInteractable selectTarget { get; }

    public override bool CanHover(XRBaseInteractable interactable)
    {
        return base.CanHover(interactable) && MatchUsingTag(interactable);
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        return base.CanSelect(interactable) && MatchUsingTag(interactable);
    }

    private bool MatchUsingTag(XRBaseInteractable interactable)
    {
        return interactable.CompareTag(targetTag);
    }

    [System.Obsolete]
    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {
        print(interactable);
        SendMagazine(interactable);
        base.OnSelectEntered(interactable);
    }

    public void SendMagazine(XRBaseInteractable interactable)
    {
        SimpleShoot parentWeapon = GetComponentInParent<SimpleShoot>();
        print(parentWeapon);
        parentWeapon.magazine = interactable.gameObject;
    }
}
