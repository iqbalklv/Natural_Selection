using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMovement : MonoBehaviour
{
    public float energyUsed;

        Vector2 thisPos;
        Vector2 targetPos;
        int EucDist;
        int steps = 0;

    private void Start()
    {
        energyUsed = this.gameObject.GetComponent<CreatureBrain>().energyToWalk;
        PopulationSystem.generation++;
    }

    public Vector2 roam()
    {
        string dir = null;

        if ((Vector2)this.gameObject.transform.position == targetPos)
        {
            this.gameObject.GetComponent<CreatureBrain>().targetAchieved = false;
        }

        if (this.gameObject.GetComponent<CreatureBrain>().foodMemory.Count > 0)
        {
            if (!this.gameObject.GetComponent<CreatureBrain>().targetAchieved)
            {
                int foodMemoryLength = this.gameObject.GetComponent<CreatureBrain>().foodMemory.Count;
                int targetIndex = (int)Random.Range(0, foodMemoryLength);

                targetPos = this.gameObject.GetComponent<CreatureBrain>().foodMemory[targetIndex];
                this.gameObject.GetComponent<CreatureBrain>().targetAchieved = true;
            }

            EucDist = EuclideanDistance(targetPos);

            thisPos = this.gameObject.transform.position;
            dir = DirectionDecisor(targetPos);
        }
        else if (BoundCheck())
        {
            dir = RandomizeDirectionBounded();
        }
        else
        {
            dir = RandomizeDirection();
        }

       

        if (dir != null)
        {
            MoveAction(dir);
            this.gameObject.GetComponent<CreatureBrain>().energy -= this.energyUsed;
        }

        return this.gameObject.transform.position;
    }

    bool BoundCheck()
    {
        Vector2 curPos = this.gameObject.transform.position;

        return (curPos.x == FoodSystem.bounds[0].transform.position.x || curPos.y == FoodSystem.bounds[0].transform.position.y
          || curPos.x == FoodSystem.bounds[1].transform.position.x || curPos.y == FoodSystem.bounds[1].transform.position.y);
    }

    string RandomizeDirectionBounded()
    {
        Vector2 curPos = this.gameObject.transform.position;
        int dec = (int)Random.Range(0, 3);
        Debug.Log(dec);

        //pojok
        if (curPos.x == FoodSystem.bounds[0].transform.position.x && curPos.y == FoodSystem.bounds[1].transform.position.y)
        {
            switch (dec)
            {
                case 0:
                    return "up";
                case 1:
                    return "right";
                case 2:
                    return "upright";
            }
        }
        if (curPos.x == FoodSystem.bounds[0].transform.position.x && curPos.y == FoodSystem.bounds[0].transform.position.y)
        {
            switch (dec)
            {
                case 0:
                    return "down";
                case 1:
                    return "right";
                case 2:
                    return "downright";
            }
        }
        if (curPos.x == FoodSystem.bounds[1].transform.position.x && curPos.y == FoodSystem.bounds[0].transform.position.y)
        {
            switch (dec)
            {
                case 0:
                    return "down";
                case 1:
                    return "left";
                case 2:
                    return "downleft";
            }
        }
        if (curPos.x == FoodSystem.bounds[1].transform.position.x && curPos.y == FoodSystem.bounds[1].transform.position.y)
        {
            switch (dec)
            {
                case 0:
                    return "up";
                case 1:
                    return "left";
                case 2:
                    return "upleft";
            }
        }

        //sisi


        if (curPos.x == FoodSystem.bounds[0].transform.position.x || curPos.x == FoodSystem.bounds[1].transform.position.x)
        {
            if (curPos.x == FoodSystem.bounds[0].transform.position.x)
            {
                if (dec == 2)
                    return "right";
            }
            if (curPos.x == FoodSystem.bounds[1].transform.position.x)
            {
                if (dec == 2)
                    return "left";
            }
            switch (dec)
            {
                case 0:
                    return "up";
                case 1:
                    return "down";
            }
        }

        if (curPos.y == FoodSystem.bounds[0].transform.position.y || curPos.y == FoodSystem.bounds[1].transform.position.y)
        {
            if (curPos.y == FoodSystem.bounds[0].transform.position.y)
            {
                if (dec == 2)
                    return "down";
            }
            if (curPos.y == FoodSystem.bounds[1].transform.position.y)
            {
                if (dec == 2)
                    return "up";
            }

            switch (dec)
            {
                case 0:
                    return "right";
                case 1:
                    return "left";
            }
        }

        Debug.Log("NULL DIRECTION");
        return null;
    }

    string RandomizeDirection()
    {

        switch((int)Random.Range(0, 9))
        {
            case 1:
                return "up";
            case 2:
                return "upright";
            case 3:
                return "right";
            case 4:
                return "downright";
            case 5:
                return "down";
            case 6:
                return "downleft";
            case 7:
                return "left";
            case 8:
                return "upleft";
        }

        return null;
    }

    string DirectionDecisor(Vector2 target)
    {
        int vertDist = Mathf.Abs((int)(target.y - thisPos.y));
        int horzDist = Mathf.Abs((int)(target.x - thisPos.x));

        // float xDiag = thisPos.y - 1;
        switch (Quadran(target))
        {
            case 1:
                {
                    if (vertDist > horzDist)
                    {
                        return "down";
                    }
                    else if (horzDist > vertDist)
                    {
                        return "left";
                    }
                    else
                    {
                        return "downleft";
                    }
                }

            case 2:
                {
                    if (vertDist > horzDist)
                    {
                        return "down";
                    }
                    else if (horzDist > vertDist)
                    {
                        return "right";
                    }
                    else
                    {
                        return "downright";
                    }
                }

            case 3:
                {
                   // Debug.Log(vertDist + " & " + horzDist);

                    if(vertDist > horzDist)
                    {
                        return "up";
                    }else if(horzDist > vertDist)
                    {
                        return "right";
                    }
                    else
                    {
                        return "upright";
                    }
                }

            case 4:
                {
                    if (vertDist > horzDist)
                    {
                        return "up";
                    }
                    else if (horzDist > vertDist)
                    {
                        return "left";
                    }
                    else
                    {
                        return "upleft";
                    }
                }

            case 5:
                {
                    return "left";
                }

            case 6:
                {
                    return "down";
                }

            case 7:
                {
                    return "right";
                }

            case 8:
                {
                    return "up";
                }


        }

        return null;
    }

    int Quadran(Vector2 target)
    {

        if (thisPos.x > target.x && thisPos.y > target.y)
        {
            return 1;
        }
        else if (thisPos.x < target.x && thisPos.y > target.y)
        {
            return 2;
        }
        else if (thisPos.x < target.x && thisPos.y < target.y)
        {
            return 3;
        }
        else if (thisPos.x > target.x && thisPos.y < target.y)
        {
            return 4;
        }
        else if (thisPos.y == target.y && thisPos.x > target.x)
        {
            return 5;
        }
        else if (thisPos.x == target.x && thisPos.y > target.y)
        {
            return 6;
        }
        else if (thisPos.y == target.y && thisPos.x < target.x)
        {
            return 7;
        }else if (thisPos.x == target.x && thisPos.y < target.y)
        {
            return 8;
        }

        return 0;

    }

    int EuclideanDistance(Vector2 target)
    {
        thisPos = this.gameObject.transform.position;

        float X = Mathf.Pow(target.x - thisPos.x, 2);
        float Y = Mathf.Pow(target.y - thisPos.y, 2);

        int dist = (int)(Mathf.Sqrt(X + Y));

        return dist;
    }

    void MoveAction(string dir)
    {
        if (dir != null)
        {
            switch (dir.ToLower())
            {
                case "up":
                    {
                        transform.Translate(Vector2.up);
                        break;
                    }
                case "upright":
                    {
                        transform.Translate(1f, 1f, 0f);
                        break;

                    }
                case "right":
                    {
                        transform.Translate(Vector2.right);
                        break;
                    }
                case "downright":
                    {
                        transform.Translate(1, -1, 0);
                        break;
                    }
                case "down":
                    {
                        transform.Translate(Vector2.down);
                        break;
                    }
                case "downleft":
                    {
                        transform.Translate(-1, -1, 0);
                        break;
                    }
                case "left":
                    {
                        transform.Translate(Vector2.left);
                        break;
                    }
                case "upleft":
                    {
                        transform.Translate(-1, 1, 0);
                        break;
                    }
            }
            steps++;

            this.gameObject.GetComponent<CreatureBrain>().stepsTaken = steps;
        }

    }
}
