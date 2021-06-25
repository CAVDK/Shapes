using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{
    
    public Animator mainMenuAnimator;
    public Animator spin;
    public Animator bg;
    
    //play Butoon
    public void Play()
    {
        //for now just load the nec
        SceneManager.LoadScene(1);
    }

    //setting window
    public void OpenSetting()
    {
        ButtonPressed("setting");


    }

    public void OpenShop()
    {
        ButtonPressed("shop");

    }

    public void OpenSocials()
    {
        ButtonPressed("social");
    }

    public void ButtonPressed(string buttonNmae)
    {
        bool openWindow = mainMenuAnimator.GetBool(buttonNmae);
        openWindow = !openWindow;
        RearranceBagckground(openWindow);
        mainMenuAnimator.SetBool(buttonNmae, openWindow);
    }
    ///Rearranging the back ground elemnet like name --- movinf it up and decreasing its size
    private void RearranceBagckground(bool opClicked)
    {
        

        if (opClicked)
        {
            bg.SetBool("optionopen", true);
        }
        else
        {
            bg.SetBool("optionopen", false);

        }

        
    }
    public void Quit()
    {
        //save the data before quitting
        Application.Quit();
    }

    
   
        

}
