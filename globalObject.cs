using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class globalObject : MonoBehaviour
{
    #region GlobalCrap
    //For making this a global Script
    public static globalObject Instance;
    public GameObject WinningPanel;
    void Awake()
    {
        if (Instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public GameObject player;
    public List<ScriptO_Reaction> reactions;
    public string winningChecmical;
    public GameObject ReactionGiverUI;
    public int nextLevel;

    public List<string> inSceneChemicals;
    bool won = false;
    private void Update()
    {
        if(!won)
        {
            inSceneChemicals = new List<string>();

            foreach (GameObject g in GameObject.FindGameObjectsWithTag("MovableBlock"))
            {
                inSceneChemicals.Add(g.GetComponent<Script_MovableBlock>().chemicalName);
                if (g.GetComponent<Script_MovableBlock>().chemicalName == winningChecmical)
                {
                    won = true;
                    ChangeCanPlayerMove();

                    WinningPanel.SetActive(true);

                    StartCoroutine(RollInPanelforNext());
                }
            }
        }
        
    }
    public void RestartLevel()
    {
        StartCoroutine(RollInPanelforRestart());
    }
    public void ChangeCanPlayerMove() {
        player.GetComponent<Script_Player>().canMove = !player.GetComponent<Script_Player>().canMove;
    }

    IEnumerator RollInPanelforRestart()
    {
        yield return new WaitForSeconds(0.4f);
        FindObjectOfType<levelloader>().LoadNextLevel(SceneManager.GetActiveScene().buildIndex);


    }
    IEnumerator RollInPanelforNext()
    {
        yield return new WaitForSeconds(2f);
        if (SceneManager.GetActiveScene().buildIndex != 4)
        {
            FindObjectOfType<levelloader>().LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }


    }
    public void PlaySound(AudioClip soundToPlay)
    {
        gameObject.GetComponent<AudioSource>().clip = soundToPlay;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void OpenReactionGiverUI(ScriptO_Reaction r)
    {
        ReactionGiverUI.SetActive(true);
        Image i = ReactionGiverUI.transform.Find("ReactionImage").GetComponent<Image>();

        i.sprite = r.reactionImage;
    }
}
