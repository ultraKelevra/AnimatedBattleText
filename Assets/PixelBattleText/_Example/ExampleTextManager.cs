using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using PixelBattleText;
using TMPro;

namespace AnimatedBattleText.Examples
{
	public class ExampleTextManager : MonoBehaviour
	{
		public float3 textSpawnPosition;
		private TextAnimation lastUsed;
		public TextAnimation missAnimatedBattleTextProps;
		public TextAnimation damageAnimatedBattleTextProps;
		public TextAnimation healAnimatedBattleTextProps;
		public TextAnimation criticalDamageAnimatedBattleTextProps;
		public TextAnimation criticalAnimatedBattleTextAnimatedBattleTextProps;
		public TextAnimation KOProps;
		public TextAnimation LvlUpProps;
		public TMP_InputField input;
		
		void Start()
		{
			lastUsed = LvlUpProps;
		}

#region UI DUMMY CONTROLS

		public void ShowInputText()
		{
			var text = input.text == ""? "JUST TYPE SOMETHING..." : input.text;
			PixelBattleTextController.DisplayText(text, lastUsed, textSpawnPosition);
		}

		public void DisplayHealing()
		{
			DisplayHealing(textSpawnPosition, 999);
			lastUsed = healAnimatedBattleTextProps;
		}
		
		public void DisplayCritalDamage(){
			DisplayCritalDamage(textSpawnPosition, 9999);
				lastUsed = criticalDamageAnimatedBattleTextProps;

		}

		public void DisplayDamage(){
			DisplayDamage(textSpawnPosition, UnityEngine.Random.Range(100,999));
				lastUsed = damageAnimatedBattleTextProps;
		}

		public void DisplayMiss(){
			DisplayMiss(textSpawnPosition);
			lastUsed = missAnimatedBattleTextProps;
		}

		public void DisplayLvlUp()
		{
			DisplayLvlUp(textSpawnPosition);
			lastUsed = LvlUpProps;
		}

		public void DisplayKO(){
			DisplayKO(textSpawnPosition);
			lastUsed = KOProps;
		}

#endregion

		public void DisplayCritalDamage(float3 p, int damage)
		{
			PixelBattleTextController.
				DisplayText("CRITICAL!", criticalAnimatedBattleTextAnimatedBattleTextProps, p + new float3(0, 0.13f, 0));
			PixelBattleTextController.
				DisplayText(damage.ToString(), criticalDamageAnimatedBattleTextProps, p);
		}

		public void DisplayDamage(float3 p, int damage)
		{
			PixelBattleTextController.
				DisplayText(damage.ToString(), damageAnimatedBattleTextProps, p);
		}

		public void DisplayHealing(float3 p, int healing)
		{
			PixelBattleTextController.
				DisplayText(healing.ToString(), healAnimatedBattleTextProps, p);
		}

		public void DisplayMiss(float3 p)
		{
			PixelBattleTextController.
				DisplayText("MISS", missAnimatedBattleTextProps, p);
		}

		public void DisplayLvlUp(float3 p)
		{
			PixelBattleTextController.
				DisplayText("LEVEL UP!", LvlUpProps, p);
		}

		public void DisplayKO(float3 p)
		{
			PixelBattleTextController.
				DisplayText("K.O", KOProps, p);
		}
	}
}