using UnityEngine;
using UnityEngine.UI;

public class GunTake : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    [SerializeField] private GameObject signal;
    [SerializeField] private float xOffset = 0.3f;
    [SerializeField] public GameObject GunThatHolding;
    private Image img;
    private bool playerInZone = false;
    
    private void Start()
    {
        img = GetComponent<Image>();   
    }

    private void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.F))
        {
            PutInGunSlot();
            PutInGunIconSlot();
            TakeLootSignal.Instance.gameObject.SetActive(false);
            playerInZone = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 spawnPos = transform.position + new Vector3(xOffset, -1f, 0);
            TakeLootSignal.Instance.transform.position = spawnPos;
            TakeLootSignal.Instance.gameObject.SetActive(true);
            playerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TakeLootSignal.Instance.gameObject.SetActive(false);
            playerInZone = false;
        }
    }
    private void PutInGunSlot()
    {
        Transform freeGunSlot = QuickSlots.Instance.CheckFreeGunSlot();
        if (freeGunSlot != null)
        {
            transform.SetParent(freeGunSlot);
            transform.gameObject.SetActive(false);
        }
    }
    private void PutInGunIconSlot()
    {
        Transform freeGunIconSlot = QuickSlots.Instance.CheckFreeGunIconSlot();
        if (freeGunIconSlot != null)
        {
            freeGunIconSlot.GetComponent<Image>().sprite = img.sprite;

            Image imgComponent = freeGunIconSlot.GetComponent<Image>();
            Color color = imgComponent.color;
            color.a = 1f;
            imgComponent.color = color;
        }
    }
}
