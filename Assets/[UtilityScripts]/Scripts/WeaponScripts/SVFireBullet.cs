using UnityEngine;
public class SVFireBullet : MonoBehaviour
{
    [HideInInspector] public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    [HideInInspector] public GameObject dryFirePrefab;
    [HideInInspector] public LayerMask hitLayers = -1;
    [HideInInspector] public GameObject muzzleFlashPrefab;
    [HideInInspector] public float muzzleVelocity = 1000f; // meters per second
    public void Fire()
    {
        var muzzleFlash = Instantiate(muzzleFlashPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        muzzleFlash.transform.localScale = gameObject.transform.lossyScale;
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        var bulletScript = bullet.GetComponent<Projectile>();
       // bulletScript.bulletVelocity = muzzleVelocity;
        bulletScript.hitLayers = hitLayers;
        bullet.transform.localScale = gameObject.transform.lossyScale;
    }
    public void DryFire()
    {
        var muzzleFlash = Instantiate(dryFirePrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        muzzleFlash.transform.localScale = gameObject.transform.lossyScale;
    }
}