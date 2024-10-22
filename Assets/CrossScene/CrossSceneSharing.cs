using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Quantum.SessionRunner;

namespace com.crossscene
{
    public class CrossSceneSharing
    {

        public static CrossSceneSharing Instance;

        public Arguments SessionRunnerArguments;


        public static CrossSceneSharing GetInstace()
        {
            if (Instance == null)
            {
                Instance = new CrossSceneSharing();

            }
            return Instance;
        }



    }
}
