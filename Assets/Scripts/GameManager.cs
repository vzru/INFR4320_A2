using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public struct Prop
{
    public float HotNoisySafe;
    public float HotNoisyNSafe;
    public float HotNNoisySafe;
    public float HotNNoisyNSafe;
    public float NHotNoisySafe;
    public float NHotNoisyNSafe;
    public float NHotNNoisySafe;
    public float NHotNNoisyNSafe;
}

public enum DoorType
{
    HotNoisy,
    HotNotNoisy,
    NotHotNoisy,
    NotHotNotNoisy
}

public class GameManager : MonoBehaviour
{
    public DoorProp door;
    private List<DoorProp> doors;
    public Prop DoorProps;

    public string filePath = "probabilities.txt";

    private void OnGUI()
    {
        filePath = GUI.TextField(new Rect(800, 50, 300, 30), filePath, 25);
        if (GUI.Button(new Rect(1100, 50, 200, 30), "Load"))
        {
            if (filePath != "")
            {
                readTextFile(filePath);

                for (int i = 0; i < numOfDoors; i++)
                {
                    DoorProp newInstance = Instantiate(door, new Vector3(-50.0f + (i * 5.0f), 2.0f, 10.0f), Quaternion.identity);
                    Renderer renderer = newInstance.GetComponent<Renderer>();
                    DoorType doorProp = DoorType.NotHotNotNoisy;

                    float randomNum = Random.Range(0.0f, 100.0f);
                    float currentNum = 0.0f;
                    if (randomNum <= (currentNum += DoorProps.HotNoisySafe))
                    {
                        doorProp = DoorType.HotNoisy;
                        newInstance.safe = true;
                    }
                    else if (randomNum <= (currentNum += DoorProps.HotNoisyNSafe))
                    {
                        doorProp = DoorType.HotNoisy;
                        newInstance.safe = false;
                    }
                    else if (randomNum <= (currentNum += DoorProps.HotNNoisySafe))
                    {
                        doorProp = DoorType.HotNotNoisy;
                        newInstance.safe = true;
                    }
                    else if (randomNum <= (currentNum += DoorProps.HotNNoisyNSafe))
                    {
                        doorProp = DoorType.HotNotNoisy;
                        newInstance.safe = false;
                    }
                    else if (randomNum <= (currentNum += DoorProps.NHotNoisySafe))
                    {
                        doorProp = DoorType.NotHotNoisy;
                        newInstance.safe = true;
                    }
                    else if (randomNum <= (currentNum += DoorProps.NHotNoisyNSafe))
                    {
                        doorProp = DoorType.NotHotNoisy;
                        newInstance.safe = false;
                    }
                    else if (randomNum <= (currentNum += DoorProps.NHotNNoisySafe))
                    {
                        doorProp = DoorType.NotHotNotNoisy;
                        newInstance.safe = true;
                    }
                    else if (randomNum <= (currentNum += DoorProps.NHotNNoisyNSafe))
                    {
                        doorProp = DoorType.NotHotNotNoisy;
                        newInstance.safe = false;
                    }

                    Debug.Log(i + ", " + randomNum + ", " + currentNum + ", " + doorProp);

                    switch (doorProp)
                    {
                        case DoorType.HotNoisy:
                            renderer.material.SetColor("_Color", new Color(1.0f, 0.0f, 1.0f, 1.0f));
                            break;
                        case DoorType.HotNotNoisy:
                            renderer.material.SetColor("_Color", new Color(1.0f, 0.0f, 0.0f, 1.0f));
                            break;
                        case DoorType.NotHotNoisy:
                            renderer.material.SetColor("_Color", new Color(0.0f, 0.0f, 1.0f, 1.0f));
                            break;
                        case DoorType.NotHotNotNoisy:
                            renderer.material.SetColor("_Color", new Color(0.5f, 0.5f, 0.5f, 1.0f));
                            break;
                        default:
                            break;
                    }

                    doors.Add(newInstance);
                }

                foreach (DoorProp Door in doors)
                {
                    Door.transform.parent = this.transform;
                }
            }
        }
    }

    public int numOfDoors = 20;
    // Start is called before the first frame update
    void Start()
    {
        doors = new List<DoorProp>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
        }
    }


    void readTextFile(string file_path)
    {
        StreamReader input_file = new StreamReader(file_path);

        while (!input_file.EndOfStream)
        {
            string line = input_file.ReadLine();
            //Debug.Log(line);
            if (line[0] == 'Y' || line[0] == 'N')
            {
                if (line[0] == 'Y')
                {
                    if (line[2] == 'Y')
                    {
                        if (line[4] == 'Y')
                        {
                            //string temp = line.Substring(6, 4);
                            //Debug.Log("YYY " + line.Substring(6, 4));
                            DoorProps.HotNoisySafe = float.Parse(line.Substring(6, 4)) * 100.0f;
                            //Debug.Log(DoorProps.HotNoisySafe);
                        }
                        else if (line[4] == 'N')
                        {
                            //Debug.Log("YYN " + line.Substring(6, 4));
                            DoorProps.HotNoisyNSafe = float.Parse(line.Substring(6, 4)) * 100.0f;
                            //Debug.Log(DoorProps.HotNoisyNSafe);
                        }
                    }
                    else if (line[2] == 'N')
                    {
                        if (line[4] == 'Y')
                        {
                            //Debug.Log("YNY " + line.Substring(6, 4));
                            DoorProps.HotNNoisySafe = float.Parse(line.Substring(6, 4)) * 100.0f;
                            //Debug.Log(DoorProps.HotNNoisySafe);
                        }
                        else if (line[4] == 'N')
                        {
                            //Debug.Log("YNN " + line.Substring(6, 4));
                            DoorProps.HotNNoisyNSafe = float.Parse(line.Substring(6, 4)) * 100.0f;
                            //Debug.Log(DoorProps.HotNNoisyNSafe);

                        }
                    }
                }
                else if(line[0] == 'N')
                {
                    if (line[2] == 'Y')
                    {
                        if (line[4] == 'Y')
                        {
                            //Debug.Log("NYY " + line[6]);
                            DoorProps.NHotNoisySafe = float.Parse(line.Substring(6, 4)) * 100.0f;
                            //Debug.Log(DoorProps.NHotNoisySafe);
                        }
                        else if (line[4] == 'N')
                        {
                            //Debug.Log("NYN " + line[6]);
                            DoorProps.NHotNoisyNSafe = float.Parse(line.Substring(6, 4)) * 100.0f;
                            //Debug.Log(DoorProps.NHotNoisyNSafe);
                        }
                    }
                    else if (line[2] == 'N')
                    {
                        if (line[4] == 'Y')
                        {
                            //Debug.Log("NNY " + line[6]);
                            DoorProps.NHotNNoisySafe = float.Parse(line.Substring(6, 4)) * 100.0f;
                            //Debug.Log(DoorProps.NHotNNoisySafe);
                        }
                        else if (line[4] == 'N')
                        {
                            //Debug.Log("NNN " + line[6]);
                            DoorProps.NHotNNoisyNSafe = float.Parse(line.Substring(6, 4)) * 100.0f;
                            //Debug.Log(DoorProps.NHotNNoisyNSafe);
                        }
                    }
                }
            }
        }

        input_file.Close();
    }
}