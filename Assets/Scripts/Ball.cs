using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;
using static Helpers;
using static GameStates;

public class Ball : MonoBehaviour
{
    Rigidbody r_ball;

    public int[] OneHotStartPos { get; private set; } = new int[3];

    bool did_reset = false;

    // Start is called before the first frame update
    void Start()
    {
        r_ball = GetComponent<Rigidbody>();
    }

    void Awake()
    {
        SetStartPosFromIndex(0);
    }

    // Update is called once per frame
    void Update()
    {
        // print(state);
        if (state == "reset" && !did_reset)
        {
            print("resetting ball");
            InitBallPosResult ball_init = get_ball_init_pos();
            SetStartPosFromIndex(ball_init.Index);
            transform.localPosition = ball_init.Pos;
            transform.rotation = ball_init_rot;
            did_reset = true;
        }

        if (state == "testreset" && !did_reset)
        {
            print("Test resetting ball");
            transform.localPosition = get_ball_test_init_pos();
            transform.rotation = ball_init_rot;
            did_reset = true;
        }

        if (state == "try_game" && is_done)
        {
            InitBallPosResult ball_init = get_ball_init_pos();
            SetStartPosFromIndex(ball_init.Index);
            transform.localPosition = ball_init.Pos;
            transform.rotation = ball_init_rot;
            is_done = false;
            episode_started = DateTime.Now;
        }
        // set y axis to zero
        if (transform.localPosition.y >= 0.027f && !is_done)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 0.027f, transform.localPosition.z);
        }

        if (state == "step")
        {
            did_reset = false;
        }

        if (state == "step_two_agents")
        {
            did_reset = false;
        }

        if (freeze_game)
            freeze();
        else
            unfreeze();
    }

    void SetStartPosFromIndex(int init_index)
    {
        // clear then set
        Array.Clear(OneHotStartPos, 0, OneHotStartPos.Length);
        if ((uint)init_index < 3u) OneHotStartPos[init_index] = 1;
    }

    void freeze()
    {
        r_ball.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |
                             RigidbodyConstraints.FreezePositionY;
    }

    void unfreeze()
    {
        r_ball.constraints = RigidbodyConstraints.None;
    }
}