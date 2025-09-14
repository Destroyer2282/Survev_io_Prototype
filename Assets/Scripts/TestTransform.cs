using UnityEngine;


public class TestTransform : MonoBehaviour
{
    [SerializeField] private Transform zombie;
    private void Update()
    {
        var direction = zombie.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0,0,angle);
    }
    
}
