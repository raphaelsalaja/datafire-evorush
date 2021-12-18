using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int bulletVelocity = 30;
    public int bulletDamage = 1;
    public int bulletLifetime = 60;
    public float bulletMass = 1f;

    public LayerMask hitLayers;
    private float startTime;
    private bool hasHit = false;
 
    public void Launch()
    {
        startTime = Time.time;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddRelativeForce((Vector3.forward * bulletVelocity)*-1, ForceMode.Impulse);
    }

    void Update()
    {
        if (hasHit)
        {
            Destroy(gameObject);
            return;
        }

        float distanceTraveled = Time.fixedDeltaTime * bulletVelocity;

        bool hit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hitOut, distanceTraveled, hitLayers);

        if (hit)
        {
            if (hitOut.rigidbody != null)
            {
                HitWithObject(hitOut.rigidbody.gameObject, hitOut);
            }
            else if (hitOut.collider != null)
            {
                HitWithObject(hitOut.collider.gameObject, hitOut);
            }
            // Wait til the next frame to destroy to ensure the trail shows up
            /*transform.position += transform.TransformDirection(Vector3.forward) * hitOut.distance;*/
            hasHit = true;
        }
        else
        {
            if (Time.time - startTime > bulletLifetime)
            {
                Destroy(gameObject);
            }
        }
    }

    private void HitWithObject(GameObject hitObject, RaycastHit hit)
    {
        if (hitObject.GetComponent<Shootable>())
        {
            Shootable shootable = hitObject.GetComponent<Shootable>();
            shootable.Hit(hit, this, this.transform.TransformDirection(Vector3.forward));
        }
    }


}


