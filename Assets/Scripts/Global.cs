using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global
{
    static int score;
    static bool isFirstPlay = true;

    public static int Score { get => score; set => score = value; }
    public static bool IsFirstPlay { get => isFirstPlay; set => isFirstPlay = value; }

}
