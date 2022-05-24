using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RandomButtonCreator : MonoBehaviour
{
    public static float xMax, xMin, yMax, yMin, ourButtonWidth, ourButtonWidthOff, megaOffset, spaceBtwButtons, twoButtonOff;
    public Button ourButton, bonusTimeButton, megaButton, mistakeButton;
    public float x, y;
    public float rFloat, gFloat, bFloat, aFloat;
    private Vector2 bonusPos, megaPos;
    private bool bb;

   
    public void Start()
    {
        //Vector2 pos = new Vector2(0, 0);
        //ourButton.transform.position = pos;
        /*rFloat = ourButton.colors.normalColor.r;
        gFloat = ourButton.colors.normalColor.g;
        bFloat = ourButton.colors.normalColor.b;
        aFloat = ourButton.colors.normalColor.a;*/
        bb = true;
        ourButtonWidth = ourButton.image.rectTransform.rect.width;  //The width of the plus button
        spaceBtwButtons = 0.05f * ourButtonWidth;  //The desired minimum space between two buttons (0.08 is the max value in our case)
        ourButtonWidthOff = (ourButtonWidth/2) +  spaceBtwButtons;  //Width offset from the plus button's center to walls
        twoButtonOff = ourButtonWidth + spaceBtwButtons;  //Width offset from one button to another
        megaOffset = (ourButtonWidth * 1.5f) + spaceBtwButtons;  //Width offset when mega is involved
        yMax = mistakeButton.image.rectTransform.offsetMax.y - ourButtonWidthOff;  //Most upper coordinate where button can appear (in normal cases)
        xMax = mistakeButton.image.rectTransform.offsetMax.x - ourButtonWidthOff;  //Most right coordinate where button can appear (in normal cases)
        yMin = mistakeButton.image.rectTransform.offsetMin.y + ourButtonWidthOff;  //Most lower coordinate where button can appear (in normal cases)
        xMin = mistakeButton.image.rectTransform.offsetMin.x + ourButtonWidthOff;  //Most left coordinate where button can appear (in normal cases)
        /*
        Debug.Log("xmx" + xMax.ToString());
        Debug.Log("ymx" + yMax.ToString());
        Debug.Log("xmn" + xMin.ToString());
        Debug.Log("ymn" + yMin.ToString());
        */
    }

    //Randomly put the plus button into the game
    public void ButtonRandomizer()
    {
        bonusPos = bonusTimeButton.image.rectTransform.anchoredPosition;  //The selected positon of the bonus button
        megaPos = megaButton.image.rectTransform.anchoredPosition;  //The selected positon of the mega button
        if (Button_Add.bonusTimeButtonIsActive)
        {
            while (bb)
            {              
                if (Button_Add.megaBool || Button_Add.isMega)
                {
                    x = Random.Range(xMin + (ourButtonWidth / 2), xMax - (ourButtonWidth / 2));  //Even less freedom on walls
                    y = Random.Range(yMin + (ourButtonWidth / 2), yMax - (ourButtonWidth / 2));  //Even less freedom on walls
                    if (Button_Add.megaBool)
                    {
                        if (!((megaPos.x - megaOffset) < x && x < (megaPos.x + megaOffset) && (megaPos.y - megaOffset) < y && y < (megaPos.y + megaOffset)))  //
                        {
                            bb = false;
                        }
                    }
                    else
                    {
                        if (!((bonusPos.x - megaOffset) < x && x < (bonusPos.x + megaOffset) && (bonusPos.y - megaOffset) < y && y < (bonusPos.y + megaOffset)))
                        {
                            bb = false;
                        }
                    }                   
                }
                else
                {                  
                    x = Random.Range(xMin, xMax);
                    y = Random.Range(yMin, yMax);
                    if (((bonusPos.x - twoButtonOff) > x || x > (bonusPos.x + twoButtonOff)) && ((bonusPos.y - twoButtonOff) > y || y > (bonusPos.y + twoButtonOff)))
                    {
                        bb = false;
                    }
                }   
            }
            bb = true;
            ourButton.image.rectTransform.anchoredPosition = new Vector2(x, y);
        }
        else
        {
            if (Button_Add.isMega || Button_Add.megaBool)
            {
                x = Random.Range(xMin + (ourButtonWidth/2), xMax - (ourButtonWidth / 2));
                y = Random.Range(yMin + (ourButtonWidth / 2), yMax - (ourButtonWidth / 2));
            }
            else
            {
                x = Random.Range(xMin, xMax); 
                y = Random.Range(yMin, yMax);              
            }            
            ourButton.image.rectTransform.anchoredPosition = new Vector2(x, y);
        }      
    }
    

    public void ButtonColorChanger()
    {
        /*rFloat = Random.Range(0, 255);
        gFloat = Random.Range(0, 255);
        bFloat = Random.Range(0, 255);
        rFloat /= 255;
        gFloat /= 255;
        bFloat /= 255;
        ColorBlock buttonColors = ourButton.colors;
        buttonColors.normalColor = new Color(rFloat, gFloat, bFloat, aFloat); ;
        buttonColors.highlightedColor = new Color(rFloat, gFloat, bFloat, aFloat);
        buttonColors.pressedColor = new Color(rFloat, gFloat, bFloat, aFloat);
        ourButton.colors = buttonColors;
        ourButton.image.color = new Color(rFloat, gFloat, bFloat, aFloat);*/
    }
}
