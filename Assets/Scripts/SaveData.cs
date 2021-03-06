﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData
{
    private static int[] level;
    private static bool[] completed;
    private static float[] completionTime;

    private static string[] saveData;

    private static string fileName = Application.persistentDataPath + "saveData.txt";

    public static int Setup(int numberOfLevels)
    {
        try
        {
            if (!File.Exists(fileName))
            {
                level = new int[numberOfLevels];
                completionTime = new float[numberOfLevels];
                completed = new bool[numberOfLevels];
                saveData = new string[numberOfLevels];

                Debug.Log("Opened file!");

                for (int i = 0; i < numberOfLevels; i++)
                {
                    level[i] = i + 1;
                    completionTime[i] = 0;
                    completed[i] = false;

                    saveData[i] = level[i] + " " + 0 + " " + completionTime[i];
                }
                Debug.Log("Writing in file");
                File.WriteAllLines(fileName, saveData);
                return 1;
            }
            else
            {
                Debug.Log("File exists!");
                return loadFile(numberOfLevels);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }

        return 1;
    }

    public static void SaveLevelInfo(int lvl, float completeTime)
    {
        completionTime[lvl - 1] = completeTime;
        completed[lvl - 1] = true;
        saveData[lvl - 1] = level[lvl - 1] + " " + 1 + " " + completionTime[lvl - 1];
        File.WriteAllLines(fileName, saveData);
    }

    public bool isComplete(int i) { return completed[i]; }

    public int levels(int i) { return level[i - 1] + 1; }

    public float timeScore(int i) { return completionTime[i]; }


    public static int loadFile(int numberOfLevels)
    {
        Debug.Log("Reading");
        string[] loadData = File.ReadAllLines(fileName);
        

        int levelsUnlocked = 1;
        int num = loadData.Length;

        bool reWrite = false;

        if (loadData.Length != numberOfLevels)
        {
            //The total number of levels have changed
            num = numberOfLevels;
            reWrite = true;
        }

        saveData = new string[num];
        level = new int[num];
        completionTime = new float[num];
        completed = new bool[num];

        for (int i = 0; i < num; i++)
        {
            if (i >= loadData.Length)
            {
                level[i] = i + 1;
                completionTime[i] = 0;
                completed[i] = false;

                saveData[i] = level[i] + " " + 0 + " " + completionTime[i];
            }
            else
            {
                string str = loadData[i];
                string[] words = str.Split(' ');

                level[i] = int.Parse(words[0]);

                int f = int.Parse(words[1]);
                if (f == 0)
                {
                    completed[i] = false;
                }
                else
                {
                    completed[i] = true;
                    levelsUnlocked = i + 2;
                }
                completionTime[i] = (float)float.Parse(words[2]);
                saveData[i] = level[i] + " " + f + " " + completionTime[i];
            }
        }

        if (reWrite)
        {
            File.WriteAllLines(fileName, saveData);
        }

        return levelsUnlocked;
    }

    static void ResetData()
    {
        for (int i = 0; i < level.Length; i++)
        {
            completionTime[i] = 0;
            completed[i] = false;

            saveData[i] = level[i] + " " + 0 + " " + completionTime[i];
        }
        Debug.Log("Reseting Data");
        File.WriteAllLines(fileName, saveData);
    }
}