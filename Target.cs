using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : EnemyController
{
	protected override void Start()
    {
        base.Start();
        health = 1;
	}
}
