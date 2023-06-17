using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Player : MonoBehaviour
{
    public float velocityOfMovement = 10f;
    public float timeOfSnap = 0.1f;
    public GameObject Pointers;
    public float timeDelay = 0.2f;
    public int HorizontalMax = 11;
    public int VerticalMax = 17;
    public bool canMove =true;
    public string catalyst;

    bool isMoving = false;
    Rigidbody2D rb;
    GameObject instantiatedPointers;
    List<bool> restrictionList;
    // Start is called before the first frame update
    void Start()
    {
        restrictionList = new List<bool>();
        restrictionList.Add(true);
        restrictionList.Add(true);
        restrictionList.Add(true);
        restrictionList.Add(true);
        rb = gameObject.GetComponent<Rigidbody2D>();

        Invoke("SnaptoGrid", 0.001f);

    }

    // Update is called once per frame
    void Update()
    {
        if(canMove == true)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1 && isMoving == false)
            {
                if (Input.GetAxisRaw("Horizontal") == 1 && restrictionList[0] == false)
                {
                    return;
                }
                else if (Input.GetAxisRaw("Horizontal") == -1 && restrictionList[1] == false)
                {
                    return;
                }
                rb.AddForce(velocityOfMovement * Vector2.right * Time.deltaTime * Input.GetAxisRaw("Horizontal") * 100, ForceMode2D.Impulse);


                Pointers.transform.parent = null;

                isMoving = true;
            }
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1 && isMoving == false)
            {
                if (Input.GetAxisRaw("Vertical") == 1 && restrictionList[2] == false)
                {
                    return;
                }
                else if (Input.GetAxisRaw("Vertical") == -1 && restrictionList[3] == false)
                {
                    return;
                }
                rb.AddForce(velocityOfMovement * Vector2.up * Time.deltaTime * Input.GetAxisRaw("Vertical") * 100, ForceMode2D.Impulse);

                Pointers.transform.parent = null;

                isMoving = true;
            }
        }
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "MovePoint_Player")
        {
            rb.velocity = Vector2.zero;

            float elapsedTime = 0;

            Vector2 origPos = transform.position;
            Vector2 targetPos = collision.transform.position;

            Destroy(instantiatedPointers);

            while (elapsedTime < timeOfSnap)
            {
                transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeOfSnap));
                elapsedTime += Time.deltaTime;
            }

            transform.position = targetPos;


            StartCoroutine(turnOffMoving());
        }
    }
    void ValidateMovement()
    {

        if (ValidateMovement_CheckOneDirection(new Vector3(1, 0, 0), 1))
        {
            restrictionList[0] = true;
        }
        else if (CheckForImmovables(new Vector2(1, 0)))
        {
            restrictionList[0] = false;
        }
        else
        {
            restrictionList[0] = false;
            bool hasMOvableEmptySpace = false;
            for (int i = 2; i <= HorizontalMax - 2; i++)
            {
                restrictionList[0] = ValidateMovement_CheckOneDirection(new Vector3(i, 0, 0), i) || restrictionList[0];

                if(ValidateMovement_CheckOneDirection(new Vector3(i, 0, 0), i))
                {
                    hasMOvableEmptySpace = true;
                }

                if (CheckForImmovables(new Vector2(i, 0)) && !hasMOvableEmptySpace)
                {
                    restrictionList[0] = false;
                    break;
                }
            }
        }

        if (ValidateMovement_CheckOneDirection(new Vector3(-1, 0, 0), 1))
        {
            restrictionList[1] = true;
        }
        else if (CheckForImmovables(new Vector2(-1, 0)))
        {
            restrictionList[1] = false;
        }
        else
        {
            restrictionList[1] = false;
            bool hasMOvableEmptySpace = false;

            for (int i = 2; i <= HorizontalMax - 2; i++)
            {
                restrictionList[1] = ValidateMovement_CheckOneDirection(new Vector3(-i, 0, 0), i) || restrictionList[1];

                if (ValidateMovement_CheckOneDirection(new Vector3(-i, 0, 0), i))
                {
                    hasMOvableEmptySpace = true;
                }

                if (CheckForImmovables(new Vector2(-i, 0)) && !hasMOvableEmptySpace)
                {
                    restrictionList[1] = false;
                    break;
                }
            }
        }

        if (ValidateMovement_CheckOneDirection(new Vector3(0, 1, 0), 1))
        {
            restrictionList[2] = true;
        }
        else if (CheckForImmovables(new Vector2(0, 1)))
        {
            restrictionList[2] = false;
        }
        else
        {
            restrictionList[2] = false;
            bool hasMOvableEmptySpace = false;

            for (int i = 2; i <= VerticalMax - 2; i++)
            {
                restrictionList[2] = ValidateMovement_CheckOneDirection(new Vector3(0, i, 0), i) || restrictionList[2];


                if (ValidateMovement_CheckOneDirection(new Vector3(0, i, 0), i))
                {
                    hasMOvableEmptySpace = true;
                }

                if (CheckForImmovables(new Vector2(0, i)) && !hasMOvableEmptySpace)
                {
                    restrictionList[2] = false;
                    break;
                }
            }
        }

        if (ValidateMovement_CheckOneDirection(new Vector3(0, -1, 0), 1))
        {
            restrictionList[3] = true;
        }
        else if (CheckForImmovables(new Vector2(0, -1)))
        {
            restrictionList[3] = false;
        }
        else
        {
            restrictionList[3] = false;
            bool hasMOvableEmptySpace = false;

            for (int i = 2; i <= VerticalMax - 2; i++)
            {
                restrictionList[3] = ValidateMovement_CheckOneDirection(new Vector3(0, -i, 0), i) || restrictionList[3];


                if (ValidateMovement_CheckOneDirection(new Vector3(0, -i, 0), i))
                {
                    hasMOvableEmptySpace = true;
                }

                if (CheckForImmovables(new Vector2(0, -i)) && !hasMOvableEmptySpace)
                {
                    restrictionList[3] = false;
                    break;
                }
            }
        }

        /*restrictionList[0] = ValidateMovement_CheckOneDirection(new Vector3(1, 0, 0), 1) && ValidateMovement_CheckOneDirection(new Vector3(2, 0, 0), 2);
        restrictionList[1] = ValidateMovement_CheckOneDirection(new Vector3(-1, 0, 0), 1) && ValidateMovement_CheckOneDirection(new Vector3(-2, 0, 0), 2);
        restrictionList[2] = ValidateMovement_CheckOneDirection(new Vector3(0, 1, 0), 1) && ValidateMovement_CheckOneDirection(new Vector3(0, 2, 0), 2);
        restrictionList[3] = ValidateMovement_CheckOneDirection(new Vector3(0, -1, 0), 1) && ValidateMovement_CheckOneDirection(new Vector3(0, -2, 0), 2);*/


        

    }
    bool ValidateMovement_CheckOneDirection(Vector3 direction, int checkForBlocks)
    {
        if (checkForBlocks == 1)
        {
            Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position + new Vector3(direction.x, direction.y, 0), transform.localScale / 2, 0);
            int i = 0;
            //Check when there is a new collider coming into contact with the box
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].CompareTag("Walls") || hitColliders[i].CompareTag("MovableBlock"))
                {
                    return false;
                }

                i++;
            }
            return true;
        }
        else {
            Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position + new Vector3(direction.x, direction.y, 0), transform.localScale / 2, 0);
            int i = 0;
            //Check when there is a new collider coming into contact with the box
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].CompareTag("Walls") || hitColliders[i].CompareTag("MovableBlock"))
                {
                    return false;
                }

                i++;
            }
            return true;
        }
    }
    bool CheckForImmovables(Vector2 direction) {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position + new Vector3(direction.x, direction.y, 0), transform.localScale / 2, 0);
        int i = 0;
        //Check when there is a new collider coming into contact with the box
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].CompareTag("Walls"))
            {
                return true;
            }

            i++;
        }
        return false;
    }
    IEnumerator turnOffMoving() {

        yield return new WaitForSeconds(timeDelay);
        SnaptoGrid();
        isMoving = false;

    }
    public void PlaySound(AudioClip soundToPlay)
    {
        gameObject.GetComponent<AudioSource>().clip = soundToPlay;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public AudioClip stepSoundEffect;
    void SnaptoGrid() {

        rb.velocity = Vector2.zero;
        transform.position = GetClosestObject(GameObject.FindGameObjectsWithTag("Blocks")).position;

        ValidateMovement();
        PlaySound(stepSoundEffect);

        instantiatedPointers = Instantiate(Pointers, transform.position, Quaternion.identity);

    }
    Transform GetClosestObject(GameObject[] objects)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in objects)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin;
    }
}
