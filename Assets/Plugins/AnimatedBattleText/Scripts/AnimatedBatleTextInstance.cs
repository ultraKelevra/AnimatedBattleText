using UnityEngine;
using System;
using TMPro;
using Unity.Mathematics;


namespace AnimatedBattleText
{
	[Serializable]
    public class AnimatedTextInstace
    {
        public Transform[] letterTransforms;
        public TMP_Text[][] letters;
        public TextAnimation props;
        public float startTime;
        public float2 pos;
        public bool animationFinished;
        public bool active;
    }
}