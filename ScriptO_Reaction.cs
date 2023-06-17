using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rection", menuName = "ScriptableObjects/NewReaction")]
public class ScriptO_Reaction : ScriptableObject
{
    public string chemicalA;
    public string chemicalB;

    public string catalyst;

    public GameObject pf_Product;
    public GameObject pf_effect;
    public AudioClip sfx;

    public bool isTempRequired;
    public bool isLightRequired;

    public Sprite reactionImage;
}
