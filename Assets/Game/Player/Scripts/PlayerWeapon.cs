using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerWeapon : MonoBehaviour
{
    public WeaponData weaponData;
    public Transform firePoint;

    private IFireMode fireMode;
    public float nextTimeToFire;

    private PlayerInput playerInput;
    private PlayerControls playerControls;
    InputAction fireAction;

    void Awake()
    {
        firePoint = GameObject.Find("FirePoint").transform;
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
        fireAction = playerControls.Controls.Fire; // <-- Correct way if using default Input System codegen
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    void Start()
    {
        SetFireMode(new ShotgunSpreadMode());
    }

    void Update()
    {
        if (fireAction.IsPressed() && Time.time >= nextTimeToFire)
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
