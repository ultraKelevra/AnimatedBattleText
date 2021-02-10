using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using Unity.Mathematics;
using UnityEngine.UI;
using static Unity.Mathematics.math;


namespace PixelBattleText
{
	public class PixelBattleTextController : MonoBehaviour
	{
		private int _texelOffset_id = Shader.PropertyToID("_TexelOffset");
		public static PixelBattleTextController singleton;
		// public GameObject textPrefab;
		public Canvas canvas;

		private Shader borderShader;
		private List<TMP_Text[]> letters;
		private Queue<TMP_Text[]> unusedLetters;
		private List<AnimatedTextInstace> animatedTexts = new List<AnimatedTextInstace>();
		private Dictionary<TextAnimation, Material> fontMaterials = new Dictionary<TextAnimation, Material>();
		
		private TMP_Text[] GetNewText()
		{
			TMP_Text[] text;
			if (unusedLetters.Count == 0)
			{
				var i = Instantiate(textPrefab, canvas.transform, false);
				text = i.GetComponentsInChildren<TMP_Text>();
			}
			else
			{
				text = unusedLetters.Dequeue();
			}
			letters.Add(text);
			return text;
		}

		///<summary>
		///Displays and animates an efimeral text UI element at a given position in 2D world space
		///</summary>
		///<param name="word"> The string to display</param>
		///<param name="textAnimation"> Parameters for animating every letter</param>
		///<param name="position"> Position for where to display the text (world space)</param>
		private Material GetFontMaterial(TextAnimation textAnimation)
		{
#if UNITY_EDITOR
			//if no material is related to this TextAnimation => generate it and realte to this TextAnimation
			if(!fontMaterials.ContainsKey(textAnimation))
				fontMaterials[textAnimation] = new Material(borderShader);
			//Setup material (this is done every time the material is requested for picking up changes made in edit mode)
			var mat = fontMaterials[textAnimation];
			//update material with font texture
			mat.mainTexture = textAnimation.font.atlasTexture;
			//set the texel relative size for keeping a constant 1px line (in font scale) at any text size
			//(this is different to _MainTex_TexelSize as it has to rescale independent from TextSize field in TMPro_Text)
			mat.SetFloat(_texelOffset_id, 1.0f / textAnimation.font.atlasHeight
			* textAnimation.font.faceInfo.lineHeight / (float) textAnimation.textSize);
			return mat;
#else

			if(!fontMaterials.ContainsKey(textAnimation))
			{
				var mat = new Material(borderShader);
				mat.mainTexture = textAnimation.font.atlasTexture;
				mat.SetFloat(_texelOffset_id, 1.0f / textAnimation.font.atlasHeight
				* textAnimation.font.faceInfo.lineHeight / (float) textAnimation.textSize);
				fontMaterials[textAnimation] = mat;
			}
			return fontMaterials[textAnimation];
#endif
		}
		
		public static void DisplayText(string word, TextAnimation textAnimation, float3 position){
			singleton._DisplayText(word, textAnimation, position);
		}
		
		private void _DisplayText(string word, TextAnimation textAnimation, float3 position)
		{
			position.x *= canvas.pixelRect.width;
			position.y *= canvas.pixelRect.height;

			Transform[] letterTransforms = new Transform[word.Length];
			TMP_Text[][] wordGraphics = new TMP_Text[word.Length][];
			for (int i = 0; i < word.Length; i++)
			{
				string character = word[i].ToString();
				wordGraphics[i] = GetNewText();

				if(textAnimation.font)
				{
					wordGraphics[i][0].font = textAnimation.font;
					wordGraphics[i][1].font = textAnimation.font;
				}

				wordGraphics[i][0].fontMaterial = GetFontMaterial(textAnimation);

				wordGraphics[i][0].text = character;
				wordGraphics[i][1].text = character;

				var alignmentConfig = textAnimation.alignment == TextAnimation.TextAnimationAlignment.Center?
						HorizontalAlignmentOptions.Center
						: textAnimation.alignment == TextAnimation.TextAnimationAlignment.Right?
							HorizontalAlignmentOptions.Right
							: HorizontalAlignmentOptions.Left;

				wordGraphics[i][0].horizontalAlignment = alignmentConfig;
				wordGraphics[i][1].horizontalAlignment = alignmentConfig;

				letterTransforms[i] = wordGraphics[i][0].transform.parent;
			}

			var alignmentOffset = textAnimation.alignment == TextAnimation.TextAnimationAlignment.Center?
						-(textAnimation.finalLetterSpacing * (word.Length-1))/2.0f
						: textAnimation.alignment == TextAnimation.TextAnimationAlignment.Right?
							-textAnimation.finalLetterSpacing * (word.Length - 1)
							: 0;

			var animatedText = new AnimatedTextInstace()
			{
				letterTransforms = letterTransforms,
				letters = wordGraphics,
				props = textAnimation,
				startTime = Time.time,
				pos = position.xy + new float2(alignmentOffset, 0),
				active = false,
			};

			animatedTexts.Add(animatedText);
		}

		private void RemoveText(int index)
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

		private void Update()
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

					letters[j][1].color = props.fillColorInTime.Evaluate(t);
					letters[j][1].fontSize = props.textSize;
					
					if(props.haveBorder)
					{
						letters[j][0].gameObject.SetActive(true);
						letters[j][0].color = props.borderColorInTime.Evaluate(t);
						letters[j][0].fontSize = props.textSize;
					}
					else
						letters[j][0].gameObject.SetActive(false);

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

		private void Awake()
		{
			if (singleton)
				Destroy(this);
			singleton = this;
		}
		
		private GameObject textPrefab;
		// Start is called before the first frame update
		private  void Start()
		{
			letters = new List<TMP_Text[]>();
			unusedLetters = new Queue<TMP_Text[]>();
			animatedTexts = new List<AnimatedTextInstace>();
			borderShader = Shader.Find("Hidden/PixelBorder");
			textPrefab = Resources.Load("pixel_text") as GameObject;
		}

		private void Destroy(){
			//destroy all created materials
			foreach (var item in fontMaterials)
				Destroy(item.Value);
		}
	}
}