using UnityEngine;

public class GunShooting : MonoBehaviour
{
    public ProjectileBehaviour ProjectilePrefab;
    [SerializeField] private Transform LaunchOffset;
    [SerializeField] private Transform Direction;
    public float bulletSpeed = 10f;
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
            if (Input.GetMouseButtonDown(0))
                Shoot();
        }
    }
    private void Shoot()
    {
        Debug.Log("Using SHOOT");
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
        ProjectileBehaviour bullet = Instantiate(ProjectilePrefab, LaunchOffset.transform.position, rot);

        bullet.SetDirection(direction, bulletSpeed);

        //Debug.Log($"Shoot: dir={direction}, angle={angle}");
    }
}
