using UnityEngine;

public interface IFireMode
{
    void Fire(Transform firePoint, WeaponData data);
}

public class SingleShotMode : IFireMode
{
    public void Fire(Transform firePoint, WeaponData data)
    {
        GameObject projectile = Object.Instantiate(data.projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.forward * data.projectileSpeed;
        Object.Destroy(projectile, data.projectileLifetime);
    }
}

public class BurstFireMode : IFireMode
{
    public void Fire(Transform firePoint, WeaponData data)
    {
        for (int i = 0; i < data.projectilesPerShot; i++)
        {
            GameObject projectile = Object.Instantiate(data.projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.linearVelocity = firePoint.forward * data.projectileSpeed;
            Object.Destroy(projectile, data.projectileLifetime);
        }
    }
}

public class ShotgunSpreadMode : IFireMode
{
    public void Fire(Transform firePoint, WeaponData data)
    {
        for (int i = 0; i < data.projectilesPerShot; i++)
        {
            Quaternion spread = Quaternion.Euler(Random.Range(-data.projectileSpread, data.projectileSpread), Random.Range(-data.projectileSpread, data.projectileSpread), 0);
            GameObject projectile = Object.Instantiate(data.projectilePrefab, firePoint.position, firePoint.rotation * spread);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.linearVelocity = (firePoint.forward * data.projectileSpeed);
            Object.Destroy(projectile, data.projectileLifetime);
        }
    }
}