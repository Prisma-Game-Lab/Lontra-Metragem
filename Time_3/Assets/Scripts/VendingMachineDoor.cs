using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineDoor : MonoBehaviour
{
    private int hitCount;
    public GameObject wall;
    public GameObject secretWall;
    public Animator secretDoorAnim;
    public GameObject secretDoor;
    public GameObject doorLight;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hitCount++;
            if (hitCount == 3)
                StartCoroutine(OpenDoor());
            else
                StartCoroutine(ActivateLight());
        }
    }
    private IEnumerator ActivateLight()
    {
        doorLight.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        doorLight.SetActive(false);
    }
    private IEnumerator OpenDoor()
    {
        doorLight.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        doorLight.SetActive(false);
        secretDoorAnim.SetBool("activated", true);
        yield return new WaitForSeconds(0.5f);
        wall.SetActive(false);
        secretWall.SetActive(true);
        secretDoor.SetActive(true);
    }
}
