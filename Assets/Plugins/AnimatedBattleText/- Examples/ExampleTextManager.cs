using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using AnimatedBattleText;
using TMPro;

namespace AnimatedBattleText.Examples
{
	public class ExampleTextManager : MonoBehaviour
	{
		public float3 textSpawnPosition;
		public TextAnimation lastUsed;
		// public TextAnimation defaultTextProps;
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

		public void ShowInputText()
		{
			var text = input.text == ""? "JUST TYPE SOMETHING..." : input.text;
			AnimatedBattleTextController.singleton.DisplayText(text, lastUsed, textSpawnPosition);
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
			DisplayDamage(textSpawnPosition, 125);
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

		public void DisplayCritalDamage(float3 p, int damage)
		{
			AnimatedBattleTextController.singleton.
				DisplayText("CRITICAL!", criticalAnimatedBattleTextAnimatedBattleTextProps, p + new float3(0, .6f, 0));
			AnimatedBattleTextController.singleton.
				DisplayText(damage.ToString(), criticalDamageAnimatedBattleTextProps, p);
		}

		public void DisplayDamage(float3 p, int damage)
		{
			AnimatedBattleTextController.singleton.
				DisplayText(damage.ToString(), damageAnimatedBattleTextProps, p);
		}

		public void DisplayHealing(float3 p, int healing)
		{
			AnimatedBattleTextController.singleton.
				DisplayText(healing.ToString(), healAnimatedBattleTextProps, p);
		}

		public void DisplayMiss(float3 p)
		{
			AnimatedBattleTextController.singleton.
				DisplayText("MISS", missAnimatedBattleTextProps, p);
		}

		public void DisplayLvlUp(float3 p)
		{
			AnimatedBattleTextController.singleton.
				DisplayText("LEVEL UP!", LvlUpProps, p);
		}

		public void DisplayKO(float3 p)
		{
			AnimatedBattleTextController.singleton.
				DisplayText("KO", KOProps, p);
		}
	}
}