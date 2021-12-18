using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.XR.Interaction.Toolkit;
public class SimpleShoot: MonoBehaviour
{
    [Space(15)]
    [Header("Weapon Settings")]
    public bool automatic = false;
    public int bulletsLeft = 0;
    public int magazineSize = 30;
    public float recoil = 0.1f;
    public float fireWait = 0.1f;
    public float bloom = 0.1f;
    public readonly float destroyTimer = 2f;
    public readonly float ejectPower = 200f;
    public readonly float shotPower = 500f;

    [Space(15)]
    [Header("Sounds and FX")]
    public AudioSource source;
    public AudioClip fireSound;
    public AudioClip dryFireSound;
    public GameObject muzzleFlashPrefab;
    public Transform muzzleFlashLocation;

    [Space(15)]
    [Header("Gun Components")]
    public Transform casingExitLocation;
    public Transform barrelLocation;
    public GameObject casingPrefab;
    public GameObject bulletPrefab;

    public Coroutine firing_rotuine;
    public Rigidbody rigidbody;
    public WaitForSeconds wait;

    public GameObject magazineHolder;
    public GameObject magazine;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        rigidbody = GetComponent <Rigidbody> ();
        if (barrelLocation == null)
            barrelLocation = transform;
    }
    private void Awake()
    {
        wait = new WaitForSeconds(fireWait);
    }
    
    private void Shoot()
    {
        source.PlayOneShot(fireSound);

        if (muzzleFlashPrefab)
        {
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab,  barrelLocation.position, barrelLocation.rotation);
            Destroy(tempFlash, destroyTimer);
        }

        if (!bulletPrefab)
        {
            return;
        }
        
        Instantiate(bulletPrefab, barrelLocation.position, bulletPrefab.transform.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
    }
    private void CasingRelease()
    {
        if (!casingExitLocation || !casingPrefab)
        {
            return;
        }
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation);
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f, 1f);
        Destroy(tempCasing, destroyTimer);
    }

    public void PullTrigger()
    {
        if (bulletsLeft != 0)
        {
            if (!automatic)
            {
                CreateProjectile();
                CasingRelease();
                ApplyRecoil();
                CreateFlash();
                source.PlayOneShot(fireSound);
            }
            else
            {
                ShouldDestroyMagazine();
                StartFiring();
            }
        }
        else
        {
            ShouldDestroyMagazine(); 
            source.PlayOneShot(dryFireSound);
        }
    }

    public void ReleaseTrigger() { StopFiring(); }
    public void ShouldDestroyMagazine()
    {
        if (bulletsLeft == 0)
        {
            magazine.GetComponent<Magazine>().DestroyMagazine();
        }
    }
    public void StartFiring()
    {
        ShouldDestroyMagazine();
             firing_rotuine = StartCoroutine(FiringSequence());
    }
    public void StopFiring() { StopCoroutine(firing_rotuine); }
    private void CreateFlash()
    {
        if (muzzleFlashPrefab)
        {
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, muzzleFlashLocation.position, muzzleFlashLocation.rotation);
            Destroy(tempFlash, destroyTimer);
        }
    }
    private IEnumerator FiringSequence()
    {
        while (gameObject.activeSelf)
        {
            if (bulletsLeft == 0)
            {
                ShouldDestroyMagazine();
                break;
            }
            CreateProjectile();
            CreateFlash();
            CasingRelease();
            ApplyRecoil();
            source.PlayOneShot(fireSound);
            yield return wait;
        }
    }
    private void CreateProjectile()
    {
        var projectile_object = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
        var projectile = projectile_object.GetComponent<Projectile>();
        projectile.Launch();
        bulletsLeft--;
    }

    public void ApplyRecoil() { rigidbody.AddRelativeForce(Vector3.back * recoil, ForceMode.Impulse); }
    public void Reload() { bulletsLeft = magazineSize; }
    public void EmptyMagazine() { bulletsLeft = 0; }
}