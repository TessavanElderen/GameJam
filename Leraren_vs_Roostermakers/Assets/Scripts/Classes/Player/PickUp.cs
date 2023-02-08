using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PickUp : MonoBehaviour
{
    [SerializeField] private List<GameObject> pickupObject;
    [SerializeField] private LayerMask pickupMask;
    [SerializeField] private Camera mainPlayerCamera;
    [SerializeField] private float pickupRange;

    [Header("Texts")]
    public int addscore; 

    private void Update()
    {
        PickingUp();
    }

    private void PickingUp()
    {
        RaycastHit hitInfo;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(mainPlayerCamera.transform.position, mainPlayerCamera.transform.forward);

            if (Physics.Raycast(ray, out hitInfo, pickupRange, pickupMask))
            {
                if (hitInfo.collider.gameObject.CompareTag("Rooster"))
                {
                    for (int i = 0; i < pickupObject.Count;)
                    {
                        if (pickupObject.Count == 0)
                        {
                            Destroy(pickupObject[i].gameObject);
                            pickupObject.Remove(pickupObject[i]);
                            break;
                        }
                        else
                        {
                            print("I am hurt :( "); 
                        }
                    }

                    AddScore(); 
                }
            }
        }
    }

    // increase score
    public void AddScore()
    {
        addscore++;
        IncreasePoints.instance.UpdateScore(addscore);
        print(addscore);

        if (addscore == 5)
        {
            // Go to Win screen
            SceneManager.LoadScene(sceneBuildIndex: 4);
            print("You win");
        }
    }
}
