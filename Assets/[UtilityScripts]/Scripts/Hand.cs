using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Hand : XRDirectInteractor
{
    private SkinnedMeshRenderer skinned_mesh_renderer;
    protected override void Awake()
    {
        base.Awake();
        skinned_mesh_renderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public void SetVisibility(bool value)
    {
        skinned_mesh_renderer.enabled = value;
    }
}
