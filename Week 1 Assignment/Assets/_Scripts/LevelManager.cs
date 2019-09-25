using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Subject
{
    public static int level = 0;
    public Observer displayLevel;

    private void Start()
    {
        registerObserver(displayLevel);
    }

    public void updateLevel(int levelNum)
    {
        level += levelNum;
        Notify(level, NotificationType.LevelUpdated);
    }
}
