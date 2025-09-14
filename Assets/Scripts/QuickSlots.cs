using UnityEngine;
using UnityEngine.UI;

public class QuickSlots : MonoBehaviour
{
    public static QuickSlots Instance;
    public GameObject[] quickSlots;
    [SerializeField] private GameObject hand0, hand1;
    private int currentSlot = 2;
    private Vector3 baseHand0Pos, baseHand1Pos;
    public bool usingGun = false;
    [SerializeField] private GameObject WeaponSlot;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        baseHand0Pos = hand0.transform.localPosition;
        baseHand1Pos = hand1.transform.localPosition;
    }

    private void Update()
    {
        ChooseSlot();
    }
    private void ChooseSlot()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectSlot(3);
    }

    private void SelectSlot(int slotIndex)
    {
        if (currentSlot != slotIndex)
            TurnOffLastSlot();

        var slotRenderer = quickSlots[slotIndex].GetComponent<SpriteRenderer>();
        if (slotRenderer != null)
        {
            slotRenderer.color = new Color(slotRenderer.color.r, slotRenderer.color.g, slotRenderer.color.b, 1);
        }

        currentSlot = slotIndex;

        // ѕеред спавном нового оружи€ Ч очищаем WeaponSlot
        foreach (Transform child in WeaponSlot.transform)
        {
            Destroy(child.gameObject);
        }

        // ѕровер€ем, есть ли оружие в слоте
        Transform gunSlot = quickSlots[slotIndex].transform.Find("Gun");
        if (gunSlot != null && gunSlot.childCount > 0)
        {
            // достаем иконку (AwpYELLOW(Clone))
            GameObject gunIcon = gunSlot.GetChild(0).gameObject;

            // у неЄ есть GunTake, из которого мы получаем сам префаб
            GunTake gunTake = gunIcon.GetComponent<GunTake>();
            if (gunTake != null && gunTake.GunThatHolding != null)
            {
                // создаЄм оружие в WeaponSlot
                GameObject weaponInstance = Instantiate(gunTake.GunThatHolding, WeaponSlot.transform);

                // ставим в ноль локально
                weaponInstance.transform.localPosition = Vector3.zero;
                weaponInstance.transform.localRotation = Quaternion.identity;
                weaponInstance.transform.localScale = Vector3.one;

                usingGun = true;
                SetHandsPositionForGun();
            }
        }
        else
        {
            usingGun = false;
            StandartHandsPosition();
            Debug.Log($"ќружи€ нет в слоте {slotIndex}");
        }
    }

    private void TurnOffLastSlot()
    {
        if (currentSlot < quickSlots.Length)
        {
            var slotRenderer = quickSlots[currentSlot].GetComponent<SpriteRenderer>();
            if (slotRenderer != null)
            {
                slotRenderer.color = new Color(slotRenderer.color.r, slotRenderer.color.g, slotRenderer.color.b, 0);
            }
        }
    }

    public void SetHandsPositionForGun()
    {
        hand0.transform.localPosition = new Vector3(0.4f, 0, 0);
        hand1.transform.localPosition = new Vector3(0.8f, -0.166f, 0);
    }

    public void StandartHandsPosition()
    {
        Debug.Log("ћен€ю позицию рук на стандарт");
        hand0.transform.localPosition = baseHand0Pos;
        hand1.transform.localPosition = baseHand1Pos;
    }

    public Transform CheckFreeGunSlot()
    {
        for (int i = 0; i < quickSlots.Length; i++)
        {
            Transform gunSlot = quickSlots[i].transform.Find("Gun");
            if (gunSlot != null && gunSlot.childCount == 0)
            {
                Debug.Log($"—вободный слот найден: {i}");
                return gunSlot;
            }
        }
        Debug.Log("—вободных слотов не найдено");
        return null;
    }

    public Transform CheckFreeGunIconSlot()
    {
        for (int i = 0; i < quickSlots.Length; i++)
        {
            Transform gunIcon = quickSlots[i].transform.Find("GunIcon");
            if (gunIcon != null && gunIcon.gameObject.GetComponent<Image>().sprite == null)
            {
                Debug.Log($"—вободный слот найден: {i} sprite");
                return gunIcon;
            }
        }
        Debug.Log("—вободных слотов не найдено sprite");
        return null;
    }
}