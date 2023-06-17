using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_MovableBlock : MonoBehaviour
{
    public float snapAfterTime;
    public string chemicalName;

    public bool isInLight = false;

    bool canDetect = false;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Invoke("SnaptoGrid", 0.2f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            canDetect = true;
        }
        if (collision.gameObject.CompareTag("MovableBlock") && canDetect == true)
        {
            foreach (ScriptO_Reaction r in globalObject.Instance.reactions)
            {

                if (isInLight == false && r.isLightRequired == true)
                {

                }
                else if ((isInLight == true && r.isLightRequired == true) || (isInLight == false))
                {
                    string reqCatalyst = r.catalyst;

                    if (r.catalyst == "")
                    {
                        reqCatalyst = globalObject.Instance.player.GetComponent<Script_Player>().catalyst;
                    }
                    if (r.chemicalA == chemicalName && r.chemicalB == collision.gameObject.GetComponent<Script_MovableBlock>().chemicalName && reqCatalyst == globalObject.Instance.player.GetComponent<Script_Player>().catalyst)
                    {
                        globalObject.Instance.ChangeCanPlayerMove();
                        collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                        collision.gameObject.GetComponent<Script_MovableBlock>().SnaptoGrid();

                        SpawnProductBlock(r);

                        Destroy(gameObject, 0.001f);
                        Destroy(collision.gameObject, 0.001f);

                        globalObject.Instance.ChangeCanPlayerMove();


                    }
                    else if (r.chemicalB == chemicalName && r.chemicalA == collision.gameObject.GetComponent<Script_MovableBlock>().chemicalName && reqCatalyst == globalObject.Instance.player.GetComponent<Script_Player>().catalyst)
                    {
                        globalObject.Instance.ChangeCanPlayerMove();

                        SpawnProductBlock(r);

                        collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                        collision.gameObject.GetComponent<Script_MovableBlock>().SnaptoGrid();


                        Destroy(gameObject, 0.01f);
                        Destroy(collision.gameObject, 0.01f);

                        globalObject.Instance.ChangeCanPlayerMove();
                    }

                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Invoke("SnaptoGrid", snapAfterTime);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LightArea"))
        {
            isInLight = true;
        }
        else {
            isInLight = false;
        }
    }
    private void SpawnProductBlock(ScriptO_Reaction r)
    {
        Instantiate(r.pf_effect, transform.position, Quaternion.identity);
        Instantiate(r.pf_Product, transform.position, Quaternion.identity);
        globalObject.Instance.PlaySound(r.sfx);
    }
    void SnaptoGrid()
    {
        rb.velocity = Vector2.zero;
        transform.position = GetClosestObject(GameObject.FindGameObjectsWithTag("Blocks")).position;
        canDetect = false;
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
