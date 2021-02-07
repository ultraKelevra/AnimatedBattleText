using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using Unity.Mathematics;
using UnityEngine.UI;
using static Unity.Mathematics.math;


namespace AnimatedBattleText
{
	public class AnimatedBattleTextController : MonoBehaviour
	{
		public static AnimatedBattleTextController singleton;
		public GameObject textPrefab;
		public Transform canvas;

		private List<TMP_Text[]> letters;
		private Queue<TMP_Text[]> unusedLetters;
		private List<AnimatedTextInstace> animatedTexts = new List<AnimatedTextInstace>();

		public TMP_Text[] GetNewText()
		{
			TMP_Text[] text;
			if (unusedLetters.Count == 0)
			{
				var i = Instantiate(textPrefab, canvas, false);
				text = i.GetComponentsInChildren<TMP_Text>();
			}
			else
			{
				text = unusedLetters.Dequeue();
			}

			letters.Add(text);
			return text;
		}

		public void DisplayText(string word, TextAnimation props, float3 position)
		{
			Transform[] letterTransforms = new Transform[word.Length];
			TMP_Text[][] wordGraphics = new TMP_Text[word.Length][];
			for (int i = 0; i < word.Length; i++)
			{
				string character = word[i].ToString();
				wordGraphics[i] = GetNewText();
				foreach (var tmp in wordGraphics[i])
				{
					tmp.font = props.font;
					tmp.text = character;
				}
				letterTransforms[i] = wordGraphics[i][0].transform.parent;
			}

			var alignmentOffset = props.alignment == TextAnimation.TextAnimationAlignment.Center?
						-(props.finalLetterSpacing * word.Length)/2.0f
						: props.alignment == TextAnimation.TextAnimationAlignment.Right?
							-props.finalLetterSpacing * word.Length
							: 0;

			var animatedText = new AnimatedTextInstace()
			{
				letterTransforms = letterTransforms,
				letters = wordGraphics,
				props = props,
				startTime = Time.time,
				pos = position.xy + new float2(alignmentOffset, 0),
				active = false,
			};

			animatedTexts.Add(animatedText);
		}

		void RemoveText(int index)
		{
			var text = animatedTexts[index];
			animatedTexts.RemoveAt(index);
			foreach (var letter in text.letters)
			{
				letters.Remove(letter);
				unusedLetters.Enqueue(letter);
				letter[0].transform.parent.gameObject.SetActive(false);
			}
		}

		void Update()
		{
			for (int i = 0; i < animatedTexts.Count; i++)
			{
				var text = animatedTexts[i];

				if (text.animationFinished)
				{
					RemoveText(i);
					i--;

					continue;
				}

				var props = text.props;
				var letters = text.letters;
				var transforms = text.letterTransforms;
				var start = text.startTime;
				var pos = text.pos;
				var duration = props.transitionInDuration;
				var delay = props.perLetterDelay;

				if (!text.active)
				{
					for (int j = 0; j < transforms.Length; j++)
					{
						transforms[j].gameObject.SetActive(true);
					}

					text.active = true;
				}

				var allEnded = true;

				for (int j = 0; j < letters.Length; j++)
				{
					var letterStart =   start + delay * (props.invertDelay ? letters.Length - j : j);
					var letterEnd = letterStart + duration;

					var t = saturate(unlerp(letterStart, letterEnd, Time.time));

					var pivotT = props.pivotCurve.Evaluate(t);
					var letterPivot = new float2(lerp(props.initialLetterSpacing * j, props.finalLetterSpacing * j, pivotT),
						0);

					var additivePosT = props.additivePosCurve.Evaluate(t);
					var letterAdditivePos =
						lerp(props.initialLetterAdditivePos, props.finalLetterAdditivePos, additivePosT);

					transforms[j].position = float3(pos + letterPivot + letterAdditivePos, 0);

					letters[j][4].color = props.fillColorInTime.Evaluate(t);
					letters[j][4].fontSize = props.textSize;
					
					for (int k = 0; k < 4; k++)
					{
						letters[j][k].color = props.borderColorInTime.Evaluate(t);
						letters[j][k].fontSize = props.textSize;
					}

					if (t < 1)
					{
						allEnded = false;
					}
				}

				if (allEnded)
				{
					text.animationFinished = true;
					text.startTime = Time.time;
					animatedTexts[i] = text;
				}
			}
		}

		void Awake()
		{
			if (singleton)
				Destroy(this);
			singleton = this;
		}

		// Start is called before the first frame update
		void Start()
		{
			letters = new List<TMP_Text[]>();
			unusedLetters = new Queue<TMP_Text[]>();
			animatedTexts = new List<AnimatedTextInstace>();
		}
	}
}