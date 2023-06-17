using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoActionOnWallCollision : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(DelayInChecking());
    }

    IEnumerator DelayInChecking()
    {
        yield return new WaitForSeconds(0.3f);
        /*Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale / 2, 0);
        int i = 0;
        //Check when there is a new collider coming into contact with the box
        while (i < hitColliders.Length)
        {
            //Output all of the collider names
            Debug.Log("Hit : " + hitColliders[i].name + i);

            if (hitColliders[i].CompareTag("Walls"))
            {
                Debug.Log("YOOOOO!");
                Destroy(gameObject);
            }

            i++;
        }
*/
    }
}
