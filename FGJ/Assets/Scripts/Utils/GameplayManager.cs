using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public GameObject levelOne;
    private LevelTransition[] levelOneTransitions;
    public GameObject mainMenu;
    void Start()
    {
        mainMenu.GetComponent<Animator>().SetTrigger("fadeIn");
        levelOneTransitions = levelOne.GetComponentsInChildren<LevelTransition>();
    }
    public void StartGame()
    {
        for (int i = 0; i < levelOneTransitions.Length; i++)
        {
            levelOneTransitions[i].ShowMe();
        }
    }
}
