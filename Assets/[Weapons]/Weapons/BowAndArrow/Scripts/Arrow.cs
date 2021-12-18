using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Arrow : XRGrabInteractable
{
    private new Collider collider;

    private Vector3 lastPosition = Vector3.zero;
    private bool launched;
    public LayerMask layerMask = ~Physics.IgnoreRaycastLayer;
    private Rigidbody rigidBody;

    [Header("Settings")] public float speed = 2000.0f;

    [Header("Hit")] public Transform tip = null;

    [Header("Particles")]
    public ParticleSystem trailParticle;
    public ParticleSystem hitParticle;
    public TrailRenderer trailRenderer;

    [Header("Sound")]
    public AudioClip launchClip;
    public AudioClip hitClip;

    void ArrowParticles()
    {
        if (launched)
        {
            trailParticle.Play();
            trailRenderer.emitting = true;
        }
        else
        {
            trailParticle.Stop();
            hitParticle.Play();
            trailRenderer.emitting = false;
        }
    }


    protected override void Awake()
    {
        base.Awake();
        collider = GetComponent<Collider>();
        rigidBody = GetComponent<Rigidbody>();
        trailRenderer = trailParticle.GetComponent<TrailRenderer>();
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        // DO THIS FIRST, SO WE GET THE RIGHT PHYSICS VALUES
        if (args.interactor is XRDirectInteractor)
            Clear();

        // MAKE SURE TO DO THIS
        base.OnSelectEntering(args);
    }

    private void Clear()
    {
        SetLaunch(false);
        TogglePhysics(true);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        // MAKE SURE TO DO THIS
        base.OnSelectExited(args);

        // IF IT'S A NOTCH, LAUNCH THE ARROW
        if (args.interactor is Notch notch)
            Launch(notch);
    }

    private void Launch(Notch notch)
    {
        // DOUBLE-CHECK IN CASE THE BOW IS DROPPED WITH ARROW SOCKETED
        if (notch.IsReady)
        {
            SetLaunch(true);
            UpdateLastPosition();
            ApplyForce(notch.PullMeasurer);
        }
    }

    private void SetLaunch(bool value)
    {
        collider.isTrigger = value;
        launched = value;
    }

    private void UpdateLastPosition()
    {
        // ALWAYS USE THE TIP'S POSITION
        lastPosition = tip.position;
    }

    private void ApplyForce(PullMeasurer pullMeasurer)
    {
        // APPLY FORCE TO THE ARROW
        var power = pullMeasurer.PullAmount;
        var force = transform.forward * (power * speed);
        rigidBody.AddForce(force);
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (launched)
        {
            // CHECK FOR COLLISION AS OFTEN AS POSSIBLE
            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {
                if (CheckForCollision())
                    launched = false;

                UpdateLastPosition();
            }


            ArrowParticles();

            // ONLY SET THE DIRECTION WITH EACH PHYSICS UPDATE
            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Fixed)
                SetDirection();
        }
    }

    private void SetDirection()
    {
        // LOOK IN THE DIRECTION THE ARROW IS MOVING
        if (rigidBody.velocity.z > 0.5f) transform.forward = rigidBody.velocity;
    }

    private bool CheckForCollision()
    {
        // CHECK IF THERE WAS A HIT
        if (Physics.Linecast(lastPosition, tip.position, out var hit, layerMask))
        {
            TogglePhysics(false);
            ChildArrow(hit);
            CheckForHittable(hit);
            if (hit.rigidbody.gameObject.GetComponent<Shootable>())
            {
                print("Git Confrimed");
                Shootable shootable = hit.rigidbody.gameObject.GetComponent<Shootable>();
                shootable.Hit(hit, this, this.transform.TransformDirection(Vector3.forward));
            }
        }

        return hit.collider != null;
    }

    private void TogglePhysics(bool value)
    {
        // DISABLE PHYSICS FOR CHILDING AND GRABBING
        rigidBody.isKinematic = !value;
        rigidBody.useGravity = value;
    }

    private void ChildArrow(RaycastHit hit)
    {
        // CHILD TO HIT OBJECT
        var newParent = hit.collider.transform;
        transform.SetParent(newParent);

    }

    private void CheckForHittable(RaycastHit hit)
    {
        // CHECK IF THE HIT OBJECT HAS A COMPONENT THAT USES THE HITTABLE INTERFACE
        var hitObject = hit.transform.gameObject;
        var hittable = hitObject ? hitObject.GetComponent<IArrowHittable>() : null;

        // IF WE FIND A VALID COMPONENT, CALL WHATEVER FUNCTIONALITY IT HAS
        if (hittable != null) hittable.Hit(this);
    }
}