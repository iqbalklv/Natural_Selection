using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureConsumption : MonoBehaviour
{
    CreatureBrain myBrain;

    public float energyGained;

    private void Start()
    {
        myBrain = this.gameObject.GetComponent<CreatureBrain>();
        energyGained = 15f;

    }

    public void Eat(Vector2 myPos)
    {
        

        if (FoodSystem.foodLocs.Contains(myPos))
        {
            FoodSystem.foodLocs.Remove(myPos);
            this.gameObject.GetComponent<CreatureBrain>().energy += this.energyGained;
        }
        if (this.gameObject.GetComponent<CreatureBrain>().foodMemory.Contains(myPos))
        {
            myBrain.foodMemory.Remove(myPos);
        }

    }
}
