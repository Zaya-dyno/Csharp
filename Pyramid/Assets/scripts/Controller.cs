using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject ball;
    private int baseSize = 4;
    private GameObject[] Placeholder;
    private Dictionary<string, float> sizeOfBall = new Dictionary<string, float> { { "height", 1},{ "width", 1 },{ "length", 1 } };
    // Start is called before the first frame update
    void Start()
    {
        int totalSize = calTotalSize(baseSize);
        Placeholder = new GameObject[totalSize];
        for (int i=0; i < totalSize; i++)
        {
            Placeholder[i] = Instantiate(ball);
        }
        drawPlaceHolder();
    }

    int calTotalSize(int size)
    {
        if (size <= 0)
        {
            return 0;
        } else
        {
            return (size * size) + calTotalSize(size - 1);
        }
    }

    void drawPlaceHolder()
    {
        int level = 0;
        int baseSide = baseSize;
        drawLevel(sizeOfBall["height"] / 2, baseSize, 0);
    }

    void drawLevel(float height, int side, int index)
    {
        if (side <= 0)
        {
            return;
        }
        bool to_pos_x = true;
        int i = 0;
        float x = 0 - (((float)side) / 2) * sizeOfBall["width"] + sizeOfBall["width"]/2;
        float z = 0 - (((float)side) / 2) * sizeOfBall["length"] + sizeOfBall["length"]/2;
        Vector3 cur = new Vector3(x, height, z);
        while (i < side * side)
        {
            Debug.Log(i);
            Debug.Log(cur);
            Placeholder[index + i].transform.position = cur;
            if ((i+1)%side == 0)
            {
                cur.z += sizeOfBall["length"];
                to_pos_x = !to_pos_x;
            } else if (to_pos_x)
            {
                cur.x += sizeOfBall["width"];
            } else
            {
                cur.x -= sizeOfBall["width"];
            }
            i++;
        }
        drawLevel(height + sizeOfBall["height"], side - 1, index + i);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
