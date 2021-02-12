using UnityEngine;
using Unity.Mathematics;
using TMPro;


namespace PixelBattleText
{
	[CreateAssetMenu(fileName = "newTextAnimation", menuName = "PixelBattleText/TextAnimation")]
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
		public float transitionDuration = .5f;
		public float perLetterDelay = 0.05f;
		public bool invertAnimationOrder = false;
		
		[Header("Spacing Animation")]
		public float initialSpacing = 9;
		public float endSpacing = 9;
		public AnimationCurve spacingCurve = AnimationCurve.Constant(0, 1, 1);

		[Header("Offset Animation")]
		public int2 initialOffset = int2.zero;
		public int2 endOffset = int2.zero;
		public AnimationCurve offsetCurve = AnimationCurve.Constant(0, 1, 1);

		[Header("Color Animation")]
		public Gradient fillColorInTime;
		public bool haveBorder;
		public Gradient borderColorInTime;
	}
}