using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class LearnQueue : MonoBehaviour
{
    private Queue<string> Bullets = new Queue<string>();
    private string bullString;

    [Button]
    private void RunCode()
    {
        Bullets.Clear();
        Console.Clear();
        for (int i = 0; i < 6; i++)
        {
            Bullets.Enqueue($"{i}");
        }
        int num = Bullets.Count;
        for (int i = 0; i < num; i++)
        {
            bullString = Bullets.Peek();
            Debug.Log(Bullets.Dequeue());
            Bullets.Enqueue(bullString);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        RunCode();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
