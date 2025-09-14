using UnityEngine;

public class BoxDestruction : MonoBehaviour
{
    [SerializeField] private GameObject[] guns;
    private int boxHP = 4;
    private Transform box;
    private Vector3 initialScale;
    [SerializeField] private AudioClip hit, crush;
    private AudioSource src;
    private Vector3 pos;
    private void Start()
    {
        src = GetComponent<AudioSource>();
        src.clip = hit;
        box = GetComponent<Transform>();
        initialScale = transform.localScale;
        pos = transform.position;
    }
    public void takeDamage()
    {
        Debug.Log("trying to deal damage");
        boxHP--;
        boxHP = Mathf.Clamp(boxHP, 0, 4);
        src.Play();
        float scaleFactor = (boxHP / 4f); // от 1 до 0
        scaleFactor = Mathf.Max(scaleFactor, 0.25f); // не меньше 0.25 (1/4)
        Debug.Log(boxHP);
        box.localScale = initialScale * scaleFactor;
        if (boxHP <= 0)
        {
            GameObject tempAudio = new GameObject("TempAudio");
            AudioSource tempSrc = tempAudio.AddComponent<AudioSource>();
            tempSrc.clip = crush;
            tempSrc.Play();
            Destroy(tempAudio, crush.length);
            DestroyBox();
            Destroy(gameObject); 
        }
    }
    private void DestroyBox()
    {
        int index = Random.Range(0, guns.Length);
        Instantiate(guns[index], pos ,Quaternion.identity);
        guns[index].SetActive(true);
    }

}
