using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureDetection : MonoBehaviour
{
    Vector2 myPos;
    Vector2 startPos;
    CreatureBrain myBrain;

    private void Start()
    {
        myBrain = this.gameObject.GetComponent<CreatureBrain>();

    }

    public void StartDetection(Vector2 myPos)
    {
        this.myPos = myPos;
        startPos.y = myPos.y + 1f;
        startPos.x = myPos.x - 1f;


        DetectFood();
    }


    void DetectFood()
    {
        int phase = 1;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (FoodSystem.foodLocs.Contains(startPos))
                {
                    myBrain.foodMemory.Add(startPos);
                }
                if (phase % 2 != 0)
                    startPos.x++;
                else
                    startPos.x--;
            }
            startPos.y--;
            phase++;
           // Debug.Log("i = " + i);
              
        }
    }

}
