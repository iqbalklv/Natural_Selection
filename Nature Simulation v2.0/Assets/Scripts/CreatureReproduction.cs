using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureReproduction : MonoBehaviour
{
    public GameObject creature;

    float eneryUsed;

    private void Start()
    {
        eneryUsed = this.gameObject.GetComponent<CreatureBrain>().energyToReproduce;
    }

    public void Reproduce(Vector2 myPos, int generation)
    {
        if(this.gameObject.GetComponent<CreatureBrain>().energy >= eneryUsed*2)
        {
            GameObject myOffspring = (GameObject)Instantiate(creature, gameObject.transform.position, Quaternion.identity);
            float walkEnergy = this.gameObject.GetComponent<CreatureBrain>().energyToWalk;
            float reproEnergy = this.gameObject.GetComponent<CreatureBrain>().energyToReproduce;


            myOffspring.GetComponent<CreatureMutation>().Mutate(this.gameObject.GetComponent<SpriteRenderer>().color.g, generation, walkEnergy, reproEnergy);

            this.gameObject.GetComponent<CreatureBrain>().energy -= eneryUsed;

        }

    }
}
