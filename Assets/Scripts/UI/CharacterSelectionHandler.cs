using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionHandler : SinglitonObjects<CharacterSelectionHandler>
{

    public Buttons[] btns;

    private void Start()
    {
        for (int i = 0; i < btns.Length; i++)
        {
            Buttons btn = btns[i];
            btn.btn.onClick.AddListener(delegate { OnClick(btn.Id); });
        }
    }


    public void OnClick(int index)
    {
        print(index);
        PlayerController.Instance.OnChangeCharacter(index);
    }

    [System.Serializable]
    public struct Buttons
    {
        public Button btn;
        public int Id;
    }

}
