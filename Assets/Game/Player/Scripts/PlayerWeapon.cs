using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public WeaponData weaponData;
    public Transform firePoint;

    private IFireMode fireMode;
    private float nextTimeToFire;

    void Awake()
    {
        firePoint = GameObject.Find("FirePoint").transform;
    }
    void Start()
    {
        SetFireMode(new SingleShotMode());
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            Fire();
        }
    }

    public void Fire()
    {
        nextTimeToFire = Time.time + 1f / weaponData.fireRate;
        fireMode.Fire(firePoint, weaponData);
        Debug.Log("Fired weapon: " + weaponData.weaponName);
    }

    public void SetFireMode(IFireMode newMode)
    {
        fireMode = newMode;
    }

    public void UpgradeWeapon(WeaponData newWeaponData, IFireMode newFireMode)
    {
        weaponData = newWeaponData;
        SetFireMode(newFireMode);
        Debug.Log("Upgraded to weapon: " + weaponData.weaponName);
    }
}
