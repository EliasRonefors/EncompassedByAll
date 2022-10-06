using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaTask : Task
{
    [SerializeField] 
    TeaGamePanel gamePanel;

    protected override void OnStart()
    {

    }

    public override void OnInteract()
    {
        gamePanel.Show();
    }
}
