using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMutation : MonoBehaviour
{
    Color color;

    public void Mutate(float parentG, int generation, float pWalkEnergy, float pReproEnergy)
    {
        this.gameObject.GetComponent<CreatureBrain>().myGeneration = (generation + 1);
        MutateColor(parentG);
        MutateEnergy();
        MutateEnergyUse(pWalkEnergy, pReproEnergy);
    }

    void MutateColor(float parentG)
    {
        color = this.gameObject.GetComponent<SpriteRenderer>().color;
        color.g += (parentG * 2 / 100f);
        this.gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    void MutateEnergy()
    {
        this.gameObject.GetComponent<CreatureBrain>().energy -= 50;
    }

    void MutateEnergyUse(float pW, float pR)
    {
        int randomize = (int)Random.Range(0, 3);
        switch (randomize)
        {
            case 0:
                this.gameObject.GetComponent<CreatureBrain>().energyToWalk = pW + (pW * 1 / 100f);
                this.gameObject.GetComponent<CreatureBrain>().energyToReproduce = pR - (pR * 2 / 100f);
                break;
            case 1:
                this.gameObject.GetComponent<CreatureBrain>().energyToWalk = pW - (pW * 2 / 100f);
                this.gameObject.GetComponent<CreatureBrain>().energyToReproduce = pR + (pR * 1 / 100f);
                break;
            case 2:
                this.gameObject.GetComponent<CreatureBrain>().energyToWalk = pW;
                this.gameObject.GetComponent<CreatureBrain>().energyToReproduce = pR;
                break;
        }

    }
}
