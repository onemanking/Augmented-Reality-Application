using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARObjectManager : MonoBehaviour
{
    private static ARObjectManager _instance = null;
    public static ARObjectManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ARObjectManager> ();
                if (_instance == null)
                {
                    GameObject go = new GameObject ();
                    go.AddComponent<ARObjectManager> ();
                    go.name = "ARObjectController";
                }
            }
            return _instance;
        }
    }

    [SerializeField]
    private List<Color> colorList;

    public List<GameObject> gameObjectList;

    float delayTime = 2;
    float currentTime;
    void Update ()
    {
        if (gameObjectList.Count < 2)
        {
            return;
        }

        currentTime += Time.deltaTime;
        if (currentTime >= delayTime)
        {
            currentTime = 0;

            for (int i = 0; i < gameObjectList.Count; i++)
            {
                if (i % 2 == 0)
                {
                    gameObjectList[i].transform.position = Vector3.MoveTowards (gameObjectList[i].transform.position, gameObjectList[i + 1].transform.parent.position, Time.deltaTime);
                }
                else
                {
                    gameObjectList[i].transform.position = Vector3.MoveTowards (gameObjectList[i].transform.position, gameObjectList[i - 1].transform.parent.position, Time.deltaTime);
                }
            }
        }
    }

    public Color RandomColor ()
    {
        Color randomColor = colorList[Random.Range (0, colorList.Count)];

        colorList.Remove (randomColor);

        return randomColor;
    }

    public void AddColor (Color color)
    {
        colorList.Add (color);
    }

    public void AddGameObject (GameObject go)
    {
        gameObjectList.Add (go);
    }

    public void RemoveGameObject (GameObject go)
    {
        gameObjectList.Remove (go);
    }
}