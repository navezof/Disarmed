using UnityEngine;
using System.Collections;
using System;

public class Dec_IsStatus : ADecorator
{
    public PawnAI.EStatus status;

    public override bool Try()
    {
        print("Try : " + name);
        if (controller.GetPawn().GetStatus() != status)
            return bInvert ? true : false;
        return bInvert ? false : true;
    }
}
