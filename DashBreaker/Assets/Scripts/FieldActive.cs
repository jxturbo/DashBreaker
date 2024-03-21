using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldActive : MonoBehaviour
{
    public List<GameObject> childObjects;
    public GameObject boss;
    public bool check = false;
    // Start is called before the first frame update
    void Start()
    {
        Transform parentTransform = transform;

        // Loop through each child of the parent GameObject
        foreach (Transform childTransform in parentTransform)
        {
            // Access the child GameObject
            GameObject childObject = childTransform.gameObject;
            childObjects.Add(childObject);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject childObject in childObjects)
            {
                childObject.SetActive(true);
            }
            if (boss != null)
            {
                boss.SetActive(true);
            }
        }
        if (other.tag == "Boss" && check == false)
        {
            boss = other.gameObject;
            boss.SetActive(false);
            check = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject childObject in childObjects)
            {
                if (childObject.tag != "Sprite")
                {
                    childObject.SetActive(false);
                }
            }
        }
    }
}
