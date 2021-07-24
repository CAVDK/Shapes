using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public Vector3 buttonPressedPosition;
    public Vector3 hidePosition;
    public Vector2 BgSize;
    public RectTransform[] _buttons;
    public RectTransform names_Obj;
    public RectTransform buttonBg;

    public float moveButtonTime;
    public float moveNammTime;
    public float moveButtonBgTime;

    bool buttonPressed = false;

    [SerializeField] Vector3 lastMovePos;

    private void Start()
    {
        buttonPressed = false;
    }

    public void PlayPressed()
    {
        //do async loading so no lag
        SceneManager.LoadScene(1);
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SettingPressed()
    {
        //  LeanTween.rotateAround(_buttons[1], Vector3.forward, 360f, 2f).setEasePunch();
        _buttons[1].LeanRotateZ(540f, 2f).setEasePunch();
        OnButtonPressed(1);
    }

    public void ShopPressed()
    {
        _buttons[2].LeanRotateY(720, 1.5f).setEasePunch();

        OnButtonPressed(2);
    }

    public void SocialPressed()
    {
        _buttons[3].LeanScale(Vector3.zero, 2f).setEasePunch();
        OnButtonPressed(3);
    }



    void OnButtonPressed(int j)
    {
        buttonPressed = !buttonPressed;
        
        if(buttonPressed)
        {
            MoveButtonToPoint(j);
            ResizeNameAndBg();
            
        }
        else
        {
            MoveButtonToHome(j);
            DefaultNameAndBg();
        }
    }


    void MoveButtonToPoint(int j)
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            if (i == j)
            {
                lastMovePos = _buttons[i].localPosition;
                _buttons[i].LeanMove(buttonPressedPosition, moveButtonTime);

            }
            else
            {
                _buttons[i].LeanMove(_buttons[i].localPosition + hidePosition, moveButtonTime);
            }
        }


    }

    void MoveButtonToHome(int j)
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            if (i == j)
            {
               
                _buttons[i].LeanMove(lastMovePos, moveButtonTime);

            }
            else
            {
                _buttons[i].LeanMove(_buttons[i].localPosition - hidePosition, moveButtonTime);
            }
        }
    }

    void ResizeNameAndBg()
    {
        buttonBg.LeanSize(BgSize, moveButtonBgTime);
        buttonBg.LeanMoveLocal(Vector3.up * -195f, moveButtonBgTime);

         names_Obj.LeanScale(Vector3.one * 0.8f, moveNammTime);

        names_Obj.LeanMove(Vector3.up * 190f, moveNammTime);

        
        
        
        
        //s names_Obj.(Vector3.up*181)
    }

    

    void DefaultNameAndBg()
    {
        buttonBg.LeanSize(new Vector2(500,645), moveButtonBgTime);
        buttonBg.LeanMoveLocal(Vector3.up * -262f, moveButtonBgTime);

        names_Obj.LeanScale(Vector3.one , moveNammTime);
        names_Obj.LeanMove(Vector3.zero, moveNammTime);
    }

}
