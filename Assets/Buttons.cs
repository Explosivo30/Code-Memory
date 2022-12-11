using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPlay()
    {
        SceneManager.LoadScene("Level1");
    }
    
    public void OnOpciones()
    {
        SceneManager.LoadScene(1);
    }
    
    public void OnExit()
    {
        Application.Quit();
    }

}
