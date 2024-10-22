using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Deterministic;
using System.Data;
using System;

namespace Quantum
{
    public unsafe abstract partial class CharacterSpec : AssetObject
    {

        public FP BasePower = 10;

        public virtual void UpdateStats(Frame f,ref CharacterSystem.Filter filter)
        {
           
        }

    }
}
