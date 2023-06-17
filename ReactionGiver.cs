using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionGiver : MonoBehaviour
{
    public ScriptO_Reaction reaction;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            globalObject.Instance.ChangeCanPlayerMove();
            globalObject.Instance.OpenReactionGiverUI(reaction);
        }
    }
}
