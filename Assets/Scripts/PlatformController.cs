using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ObjectPosition
{
    public int id;
    public Vector3 objPosition;
    public bool posIsEmpty = true;
}
public class PlatformController : MonoBehaviour
{
    public bool containObstacles = false;
    public List<ObjectPosition> objectPositionsList;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateEmojiOnPlatform(GameObject emojiPrefab, Transform emojiParentTrans)
    {
        //need the type and number to randomly distribute the emoji

       // print("CreateEmojiOnPlatform called ");
        //fill pos array with -1 values
        int[] pos = new int[objectPositionsList.Count];
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = -1;
          //  print("pos[" + i + "] is : " + pos[i]);
        }

        //get number of empty positons
        int emptyPosCount = 0;
        for (int i = 0; i < objectPositionsList.Count; i++)
        {
            
            if (objectPositionsList[i].posIsEmpty)
            {
                pos[i] = i;
                emptyPosCount++;
            }
          //  print("pos[" + i + "] is : " + pos[i] + " with emptyPos count is : " + emptyPosCount);
            
        }
        //save empty pos in positionIndex array
        int[] positionIndex = new int[emptyPosCount];
        for (int i = 0; i < positionIndex.Length; i++)
        {
            if(pos[i] > -1)
            {
                positionIndex[i] = pos[i];
            }
           // print("positionIndex[" + i + "] is : " + positionIndex[i] );
        }



        //reshuffle positionIndex array to randomly get index
        GamePreferences.ReshuffleIntArray(positionIndex);//shuffle the array to randomly get positions

        //instantiate the emojis
        int posIndex = positionIndex[0];
        GameObject newEmoji = Instantiate(emojiPrefab);
        newEmoji.transform.parent = emojiParentTrans;
        newEmoji.transform.localPosition = new Vector3(objectPositionsList[posIndex].objPosition.x, objectPositionsList[posIndex].objPosition.y, objectPositionsList[posIndex].objPosition.z);
        objectPositionsList[posIndex].posIsEmpty = false;

    }


    public bool CanAddEmojiOnPlatform(int numberOfEmoji)
    {
        //check if we can add numberOfEmoji on this platform

        bool result = false;


        return result;


    }
}
