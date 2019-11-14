using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour
{
    public static Text thisText;
    static int initGen = 0;

    private void Start()
    {
        thisText = this.gameObject.GetComponent<Text>();

    }

    public static void UpdateText(int gen, int steps, float walkRate, float reproRate, float eW, float eR)
    {
        if(gen >= initGen)
        {
            initGen = gen;
            thisText.text = "Generation: " + gen + "\nStep(s) Taken: " + steps + "\nMovement Energy Mutation: " + walkRate +"%"
                             + "\nReproduction Energy Mutation: " + reproRate + "%" 
                             + "\nEnergy Req To Walk: " + eW + "\nEnergy Req To Reproduce: " + eR;

        }
    }
}
