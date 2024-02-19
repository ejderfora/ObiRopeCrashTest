using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubukScript : MonoBehaviour
{
    private Transform transform1; // �lk transform
    private Transform transform2; // �kinci transform
    private Transform dubaSeritBitis;
    public GameObject prefab; // Prefab de�i�keni

    public GameObject kapatilacakBitis;

    DubaBitis dubaBitis;

    private bool handEntered = false;
    private bool connected = false;
    public static int counter = 0;
    private float distance;
    private GameObject instantiatedPrefab; // Olu�turulan prefab nesnesi

    void Start()
    {
        dubaBitis=GetComponentInChildren<DubaBitis>();
        transform1 = transform.Find("dubaSeritBasla");
        dubaSeritBitis = transform.Find("dubaSeritBitis");
    }

    void Update()
    {
        UpdatePrefabPositionAndRotationAndScale();

        
    }

    void UpdatePrefabPositionAndRotationAndScale()
    {
        if (handEntered==true&& connected==false)
        {

            if (dubaBitis!=null && dubaBitis.wentDestination==false)
            {
                dubaSeritBitis.transform.position = transform2.transform.position;
            }
            
            // �ki transform aras�ndaki pozisyonu hesapla
            Vector3 newPosition = (transform1.position + dubaSeritBitis.position) / 2f;

            // Nesneyi bu pozisyona ta��
            instantiatedPrefab.transform.position = newPosition;

            // �ki transform aras�ndaki y�nelimi hesapla
            Vector3 direction = dubaSeritBitis.position - transform1.position;

            // Y�nelimi kullanarak nesnenin rotasyonunu ayarla
            instantiatedPrefab.transform.rotation = Quaternion.LookRotation(direction);
            distance = CalculateDistance(transform1, transform2);
            instantiatedPrefab.transform.localScale = new Vector3(instantiatedPrefab.transform.localScale.x, instantiatedPrefab.transform.localScale.y, distance);
            //instantiatedPrefab.transform.Rotate(new Vector3(0, 0, 90), Space.Self);
            if (dubaBitis != null && dubaBitis.wentDestination == true)
            {
                connected = true;
                instantiatedPrefab.GetComponent<BoxCollider>().isTrigger = false;
            }
        }

    }
    float CalculateDistance(Transform t1, Transform t2)
    {
        // �ki transform aras�ndaki uzakl��� vekt�r fark�n� kullanarak hesapla
        return Vector3.Distance(t1.position, t2.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Band")&& handEntered==false)
        {
            if (kapatilacakBitis != null)
            {
                kapatilacakBitis.SetActive(false);
                counter++;
                if (counter>=2)
                {
                    Debug.Log("Serit Cekildi");
                    LOTOSceneManagement.stage = 4;
                }
            }

            //Debug.Log("trigger calisti");
            if (dubaBitis.gameObject.activeInHierarchy==true)
            {
                //Debug.Log(gameObject.name+": activemi�");
                transform2 = other.gameObject.transform;
                instantiatedPrefab = Instantiate(prefab);
                handEntered = true;
            }
            
        }
    }
}
