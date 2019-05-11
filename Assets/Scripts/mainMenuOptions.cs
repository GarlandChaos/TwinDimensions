using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuOptions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newGame()
    {
        menuManager.instance.GetComponent<menuManager>().newGame();
    }

    public void configuration()
    {
        menuManager.instance.GetComponent<menuManager>().configuration();
    }

    public void credits()
    {
        menuManager.instance.GetComponent<menuManager>().credits();
    }

    public void quitGame()
    {
        menuManager.instance.GetComponent<menuManager>().quitGame();
    }
}
