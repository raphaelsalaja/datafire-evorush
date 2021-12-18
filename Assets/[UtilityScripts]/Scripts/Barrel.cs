using System.Collections;
using UnityEngine;

public class Barrel : MonoBehaviour
{

    public float fire_wait = 0.1f;

    public GameObject projectile_prefab;

    private Weapon weapon;

    private Coroutine firing_rotuine;
    private WaitForSeconds wait;
    public void Setup(Weapon weapon)
    {
        this.weapon = weapon;
    }

    private void Awake()
    {
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
            weapon.ApplyRecoil();
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
}
