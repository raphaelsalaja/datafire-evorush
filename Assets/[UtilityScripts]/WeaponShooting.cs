using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    public float fire_wait = 0.1f;
    public int recoil_amount = 25;

    private new Rigidbody rigidbody;

    public GameObject projectile_prefab;

    private Coroutine firing_rotuine;
    private WaitForSeconds wait;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        wait = new WaitForSeconds(fire_wait);
    }


    public void StartFiring()
    {
        firing_rotuine = StartCoroutine(FiringSequence());
    }

    private IEnumerator FiringSequence()
    {
        while (gameObject.activeSelf)
        {
            CreateProjectile();
            ApplyRecoil();
            yield return wait;
        }
    }

    private void CreateProjectile()
    {
        GameObject projectile_object = Instantiate(projectile_prefab, transform.position, transform.rotation);
        Projectile projectile = projectile_object.GetComponent<Projectile>();
        projectile.Launch();
    }

    public void StopFiring()
    {
        StopCoroutine(firing_rotuine);
    }

    public void PullTrigger()
    {
        print("TRIGGER PRESSED");
        StartFiring();
    }

    public void ReleaseTrigger()
    {
        StopFiring();
    }

    public void ApplyRecoil()
    {
        rigidbody.AddRelativeForce(Vector3.back * recoil_amount, ForceMode.Impulse);
    }
}
