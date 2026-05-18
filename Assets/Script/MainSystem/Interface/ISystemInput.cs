using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MainSystem
{
    public interface ISystemInput
    {
        event Action<bool> OnSubmit;
    }
}