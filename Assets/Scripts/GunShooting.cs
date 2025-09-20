using UnityEngine;

public class GunShooting : MonoBehaviour
{
    public ProjectileBehaviour ProjectilePrefab;

    [SerializeField] private Transform LaunchOffset;
    [SerializeField] private Transform Direction;

    public float bulletSpeed = 10f;
    public float fireRate = 0.05f; // Интервал между выстрелами
    public float damage;
    private float nextFireTime = 0f;

    [SerializeField] private AudioClip shoot;
    private AudioSource src;
    private void Start()
    {
        src = GetComponent<AudioSource>();
        src.clip = shoot;
    }
    private void Awake()
    {
        LaunchOffset = GameObject.Find("LaunchPos").transform;
        Direction = GameObject.Find("Direction").transform;
    }

    private void LateUpdate()
    {
        if (QuickSlots.Instance.usingGun)
        {
            QuickSlots.Instance.SetHandsPositionForGun();
            if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    private void Shoot()
    {
        if (ProjectilePrefab == null || LaunchOffset == null || Direction == null)
        {
            Debug.LogWarning("Shoot: ProjectilePrefab или LaunchOffset или Direction не задан.");
            return;
        }

        Vector3 dir3 = Direction.position - LaunchOffset.position;
        dir3.z = 0f;

        Vector2 direction = new Vector2(dir3.x, dir3.y).normalized;
        if (direction.sqrMagnitude < 0.0001f)
        {
            Debug.LogWarning("Shoot: направление слишком маленькое (направление = 0).");
            return;
        }
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rot = Quaternion.Euler(0f, 0f, angle);
        src.Play();
        ProjectileBehaviour bullet = Instantiate(ProjectilePrefab, LaunchOffset.transform.position, rot);

        bullet.SetDirection(direction, bulletSpeed);
    }
}
