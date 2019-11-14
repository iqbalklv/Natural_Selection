using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBrain : MonoBehaviour
{

    public List<Vector2> foodMemory = new List<Vector2>();
    public float energy;
    public float energyToReproduce;
    public float energyToWalk;

    [HideInInspector]
    public Vector2 myPos;

    CreatureMovement movement;
    CreatureConsumption eat;
    CreatureDetection see;
    CreatureReproduction sex;

    [HideInInspector]
    public bool targetAchieved;
    [HideInInspector]
    public int myGeneration = 1;

    void Start()
    {
        targetAchieved = false;
        movement = this.gameObject.GetComponent<CreatureMovement>();
        eat = this.gameObject.GetComponent<CreatureConsumption>();
        see = this.gameObject.GetComponent<CreatureDetection>();
        sex = this.gameObject.GetComponent<CreatureReproduction>();

        myPos = this.gameObject.transform.position;


        StartCoroutine(cycle());
    }


    IEnumerator cycle()
    {
        while (energy >= energyToWalk)
        {
            yield return new WaitForSeconds(0.1f);

            see.StartDetection(myPos);
            sex.Reproduce(myPos, myGeneration);
            ClearMemory();
            myPos = movement.roam();
            eat.Eat(myPos);
            StoreData();
        }

        Destroy(this.gameObject);


    }

    void ClearMemory()
    {
        int memorySize = foodMemory.Count;

        if (memorySize >= 18)
        {
            for (int i = 0; i < memorySize/2; i++)
            {
                foodMemory.RemoveAt(i);
            }
        }
    }

    [HideInInspector]
    public int stepsTaken;
    [HideInInspector]
    public float walkRate;
    [HideInInspector]
    public float reproRate;

    void StoreData()
    {
        walkRate = 5f / energyToWalk * 100f;
        reproRate = 50f / energyToReproduce * 100f;

        // != null)
      TextUpdate.UpdateText(myGeneration, stepsTaken, walkRate, reproRate, energyToWalk, energyToReproduce);
    }
}

