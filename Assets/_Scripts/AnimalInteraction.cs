using UnityEngine;
using UnityEngine.UI;

public class AnimalInteraction : MonoBehaviour
{
    [SerializeField]
    private MoodController animal;

    [SerializeField]
    private GameObject animalMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
           if (Vector3.Distance(transform.position, animal.transform.position) < 5f) {
                Debug.Log("Menu Open");
                animalMenu.SetActive(true);
           }
           else { Debug.Log("Too Far"); }
           
        }
        if (Vector3.Distance(transform.position, animal.transform.position) > 5f) {
            animalMenu.SetActive(false);
        }
    }
}
