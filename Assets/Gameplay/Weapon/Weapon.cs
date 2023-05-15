using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Weapon : MonoBehaviour
{
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Projectile projectile;
    [SerializeField] private float fireRate = 0.5f;

    private Sound _sound;
    private WeaponUpgrades _weaponUpgrades;
    private PlayerInput _input;
    private PoolMono<Projectile> _projectilePool;
    private Transform _projectileContainer;
    private float _nextFireTime = 0;
    private bool _isShooting;
    private Camera _mainCamera;

    [Inject]
    private void Init(WeaponUpgrades weaponUpgrades, Sound sound)
    {
        _weaponUpgrades = weaponUpgrades;
        _sound = sound;
    }
    
    private void Awake()
    {
        _input = new PlayerInput();
        _mainCamera = Camera.main;
        fireRate = _weaponUpgrades.FireRate;
    }

    private void OnEnable()
    {
        _input.Player.Shoot.started += StartShoot;
        _input.Player.Shoot.canceled += StopShoot;
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Player.Shoot.started -= StartShoot;
        _input.Player.Shoot.canceled -= StopShoot;
        _input.Disable();
    }

    private void Update()
    {
        if (Time.time > _nextFireTime && _isShooting)
        {
            Shoot();
            _nextFireTime = Time.time + fireRate;
        }
        
        RotateWeapon();
    }

    private void StartShoot(InputAction.CallbackContext context)
    {
        _isShooting = true;
    }

    private void StopShoot(InputAction.CallbackContext context)
    {
        _isShooting = false;
    }

    private void Shoot()
    {
        var el = _projectilePool.GetFreeElement();
        el.SetupProjectile(shootPoint.position, transform.rotation, Vector3.forward, _weaponUpgrades.Damage);
        particleSystem.Play();
        _sound.PlaySfx(shootSound);
    }
    
    private void RotateWeapon()
    {
        Vector3 mousePosition = _input.Player.Look.ReadValue<Vector2>();
        Ray cameraRay = _mainCamera.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(cameraRay, out rayDistance))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayDistance);
            Vector3 direction = pointToLook - transform.position;
            float rotationY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotationY, 0f);
        }
    }

    public void SetupProjectilePool(Transform container)
    {
        _projectileContainer = container;
        _projectilePool = new PoolMono<Projectile>(projectile, 30, _projectileContainer);
        _projectilePool.AutoExpand = true;
    }
}

