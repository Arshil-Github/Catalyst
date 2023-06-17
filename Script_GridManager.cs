using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_GridManager : MonoBehaviour
{
    public GameObject pf_blocks;
    public Transform blocks_Parents;
    public float blockSize;
    public Vector2 numberOfBlocks;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfBlocks.x; i++)
        {
            GameObject instantiatedBlock = Instantiate(pf_blocks, (transform.position + new Vector3(i * blockSize, 0, 0)), Quaternion.identity);
            instantiatedBlock.transform.parent = transform;

            for (int j = 1; j < numberOfBlocks.y; j++)
            {
                GameObject instantiatedBlock_Further = Instantiate(pf_blocks, (instantiatedBlock.transform.position + new Vector3(0, j * blockSize, 0)), Quaternion.identity);
                instantiatedBlock_Further.transform.parent = instantiatedBlock.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
