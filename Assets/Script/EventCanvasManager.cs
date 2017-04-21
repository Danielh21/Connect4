using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventCanvasManager : MonoBehaviour
{
    public delegate void ClickAction(int playerNumber, int row);
    public static event ClickAction OnClicked;
    private int playerTurn = 0;

    public void ClickedPlace()
    {
        if (OnClicked != null)
        {
            GameObject placeDropDown = GameObject.Find("PlaceDropDown");
            List<Dropdown.OptionData> menuOptions = placeDropDown.GetComponent<Dropdown>().options;
            int menuIndex = placeDropDown.GetComponent<Dropdown>().value;
            string selected = (menuOptions[menuIndex].text);
            int parsedInt = Int32.Parse(selected);
            if (playerTurn == 1) playerTurn = 0;
            else playerTurn = 1;
            OnClicked(playerTurn, menuIndex);
        }
    }


}