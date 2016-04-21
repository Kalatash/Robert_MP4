﻿using UnityEngine;
using System.Collections;

public class SpriteUpdateEnemy : MonoBehaviour
{
    #region Sprite variables
    enum Facing
    {
        Unknown = 0,
        South = 1,
        West = 2,
        East = 3,
        North = 4
    }

    private Animator animateComp;
    private Facing currentFacing;
    private Vector3 oldPos;
    #endregion

    private EnemyBehavior.EnemyState curState;

    // Use this for initialization
    void Start()
    {
        animateComp = GetComponent<Animator>();
        currentFacing = Facing.South;
        oldPos = transform.position;
        curState = GetComponent<EnemyBehavior>().currentState;
    }

    // Update is called once per frame
    void Update()
    {
        //determine delta
        float xAxis = transform.position.x - oldPos.x;
        float yAxis = transform.position.y - oldPos.y;
        oldPos = transform.position;

        //animate the sprite
        //updateSprite(xAxis, yAxis);
        curState = GetComponent<EnemyBehavior>().currentState;
        updateSprite(xAxis, Mathf.Abs(xAxis) + Mathf.Abs(yAxis));
    }

    #region Sprite functions
    //update the sprite facing, and animation speed
    private void updateSprite(float x, float y)
    {
        //determine strongest directional from state
        Facing newFacing = Facing.Unknown;
        string trigger;
        if (curState == EnemyBehavior.EnemyState.Run)
        {
            if (x > 0)
                newFacing = Facing.East;
            else
                newFacing = Facing.West;

            trigger = "Run";
        }
        else if (curState == EnemyBehavior.EnemyState.Stunned)
        {
            newFacing = Facing.South;
            trigger = "Stunned";
        }
        else
        {
            newFacing = Facing.North;
            trigger = "Walk";
        }

        if (newFacing != currentFacing)
        {
            animateComp.SetTrigger(trigger);
            currentFacing = newFacing;
        }

        //set animation speed
        animateComp.SetFloat("Horizontal", x);
        animateComp.SetFloat("Vertical", y);
    }
    #endregion
}