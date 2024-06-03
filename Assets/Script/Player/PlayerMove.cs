using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public MoveDate Move(MoveDate date, float WalkSpeed, float RunSpeed)
    {
        MoveDate Result = new MoveDate();
        Transform ResultTransform = date.TargetTransform;

        Result.Forward = Input.GetKey(KeyCode.W);
        Result.Back = Input.GetKey(KeyCode.S);
        Result.Right = Input.GetKey(KeyCode.D);
        Result.Left = Input.GetKey(KeyCode.A);
        Result.Run = Input.GetKey(KeyCode.LeftShift);

        if (Result.Forward)
        {
            if (Result.Run)
            {
                ResultTransform.position += ResultTransform.forward * RunSpeed * Time.deltaTime;
            }
            else
            {
                ResultTransform.position += ResultTransform.forward * WalkSpeed * Time.deltaTime;
            }
        }
        else if (Result.Back)
        {
            ResultTransform.position -= ResultTransform.forward * WalkSpeed * Time.deltaTime;
        }
        if (Result.Right)
        {
            ResultTransform.position += ResultTransform.right * WalkSpeed * Time.deltaTime;
        }
        else if (Result.Left)
        {
            ResultTransform.position -= ResultTransform.right * WalkSpeed * Time.deltaTime;
        }

        Result.Stop = (!Result.Forward && !Result.Back && !Result.Right && !Result.Left && !Result.Run);
        Result.TargetTransform = ResultTransform;
        return Result;
    }
    public MoveDate Assignment(Transform tras, MoveDate move = null)
    {
        MoveDate Result = move;
        if (move == null)
        {
            Result = new MoveDate();
        }
        Result.TargetTransform = tras;
        return Result;
    }
}
