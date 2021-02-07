using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedSpeedUI : MonoBehaviour
{
    [SerializeField] InputField manualInputField;
    [SerializeField] InputField mechanicalInputField;
    float manualFeedSpeed;
    float mechanicalFeedSpeed;
    int resultManual = 0;
    int resultMechanical = 0;

    private void Start()
    {
         if (ChiselMovement.maxDragSpeed > FeedSpeedButton.resultNumber / 1000f - 0.001 && ChiselMovement.maxDragSpeed < FeedSpeedButton.resultNumber / 1000f + 0.001f)
         {
             GameManager.chiselStrenght = 6f;
         }
         else
         {
             GameManager.chiselStrenght = 2f;
         }
    }

    private void Update()
    {
            if(!FeedLeaverConfirm.isFeedConfirmLeaverOnPlace)
            {
                if (manualFeedSpeed > FeedSpeedButton.resultNumber / 1000f - 0.001f && manualFeedSpeed < FeedSpeedButton.resultNumber / 1000f + 0.001f)
                {
                    GameManager.chiselStrenght = 6f;
                }
                else
                {
                    GameManager.chiselStrenght = 2f;
                }
            }
            else
            {
                if (mechanicalFeedSpeed > FeedSpeedButton.resultNumber / 1000f - 0.001 && mechanicalFeedSpeed < FeedSpeedButton.resultNumber / 1000f + 0.001f)
                {
                    GameManager.chiselStrenght = 6f;
                }
                else
                {
                    GameManager.chiselStrenght = 2f;
                }
            }
    }

    public void ManualFeedSpeed(string newSpeedManual) //Funkcja pobierająca wprowadzoną wysokość przypisuje nową
    {
        if(newSpeedManual == null)
        {
            return;
        }
        else
        {
            int.TryParse(newSpeedManual, out resultManual);

            manualFeedSpeed = resultManual/1000f;

            if (manualFeedSpeed > 0.15f)
            {
                manualFeedSpeed = 0.15f;
                manualInputField.text = "150f";
            }
            else if (manualFeedSpeed < 0.025f)
            {
                manualFeedSpeed = 0.025f;
                manualInputField.text = "25f";
            }

            ChiselMovement.maxDragSpeed = manualFeedSpeed;
            ChiselMovement.maxDragSpeedStatic = manualFeedSpeed;
            ChiselMovement.maxDragSpeedNormal = manualFeedSpeed;
            ChiselMovement.maxDragSpeedContact = manualFeedSpeed - 0.02f;
        }
      
    }

    public void MechanicalFeedSpeed(string newSpeedMechanical) //Funkcja pobierająca wprowadzoną wysokość przypisuje nową
    {
        if (newSpeedMechanical == null)
        {
            return;
        }
        else
        {
            int.TryParse(newSpeedMechanical, out resultMechanical);

            mechanicalFeedSpeed = resultMechanical / 1000f;

            if (mechanicalFeedSpeed > 0.025f)
            {
                mechanicalFeedSpeed = 0.025f;
                mechanicalInputField.text = "25";
            }
            else if (mechanicalFeedSpeed < 0.0001f)
            {
                mechanicalFeedSpeed = 0.0001f;
                mechanicalInputField.text = "0.1";
            }

            ChiselMovement.feedSpeed = mechanicalFeedSpeed;
        }
      
    }
}
