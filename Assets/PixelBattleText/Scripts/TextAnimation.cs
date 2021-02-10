using UnityEngine;
using Unity.Mathematics;
using TMPro;


namespace PixelBattleText
{
	[CreateAssetMenu(fileName = "newTextAnimation", menuName = "BattleText/TextAnimation")]
	public class TextAnimation: ScriptableObject
	{
		public enum TextAnimationAlignment
		{
			Left,
			Center,
			Right
		}

		[Header("Font Controls")]
		public TMP_FontAsset font;
		public int textSize = 16;
		public TextAnimationAlignment alignment;

		[Header("Time Controls")]
		public float transitionInDuration = .5f;
		public float perLetterDelay = 0.05f;
		public bool invertDelay = false;
		
		[Header("Spacing Animation")]
		public float initialLetterSpacing = (1 / 32.0f) * 8;
		public float finalLetterSpacing = (1 / 32.0f) * 8;
		public AnimationCurve pivotCurve = AnimationCurve.Constant(0, 1, 1);

		[Header("Displace Animation")]
		public int2 initialLetterAdditivePos = int2.zero;
		public int2 finalLetterAdditivePos = int2.zero;
		public AnimationCurve additivePosCurve = AnimationCurve.Constant(0, 1, 1);

		[Header("Color Animation")]
		public Gradient fillColorInTime;
		public bool haveBorder;
		public Gradient borderColorInTime;

	}
}