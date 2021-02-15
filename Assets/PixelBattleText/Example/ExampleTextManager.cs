using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UI;
using PixelBattleText;
using TMPro;

namespace AnimatedBattleText.Examples
{
	public class ExampleTextManager : MonoBehaviour
	{
		public float3 textSpawnPosition;
		private TextAnimation lastUsed;

		public TextAnimation ko;
		public TextAnimation lvlUp;
		public TextAnimation premium;
		public TextAnimation spooky;
		public TextAnimation venom;

		public TextAnimation pyro;
		public TextAnimation shock;
		public TextAnimation freeze;

		public TextAnimation metallic;
		public TextAnimation criticalNumber;
		public TextAnimation criticalText;
		public TextAnimation damage;
		public TextAnimation heal;

		public TMP_InputField input;
		
		public GameObject pallete_0;
		public GameObject pallete_1;
		private bool displayingPallete_0 = true;
		private Image lastButton;
		
		void Start()
		{
			lastUsed = lvlUp;
		}

		public Color[] outlineColors;
		public Image textbox;
		public Image button;
#region UI DUMMY CONTROLS
		public void SetColor(int col){
			textbox.color = outlineColors[col];
			button.color = outlineColors[col];
		}
		private void SwapColors(Image source, Color color)
		{
			if(lastButton)
				lastButton.color = Color.white;
			source.color = color;
		}

		public void SwapEffectPallete()
		{
			displayingPallete_0 = ! displayingPallete_0;
			pallete_0.SetActive(displayingPallete_0);
			pallete_1.SetActive(!displayingPallete_0);
		}

		public void ShowInputText()
		{
			var text = input.text == ""? "JUST TYPE SOMETHING..." : input.text;
			PixelBattleTextController.DisplayText(text, lastUsed, textSpawnPosition);
		}

		public void DisplayPremium()
		{
			PixelBattleTextController.DisplayText(
				"PREMIUM",
				premium,
				textSpawnPosition);

			lastUsed = premium;
		}

		public void DisplaySpooky()
		{
			PixelBattleTextController.DisplayText(
				"SPOOKY...",
				spooky,
				textSpawnPosition);

			lastUsed = spooky;
		}

		public void DisplayPyro()
		{
			PixelBattleTextController.DisplayText(
				UnityEngine.Random.Range(100,999).ToString(),
				pyro,
				textSpawnPosition);
			
			lastUsed = pyro;
		}

		public void DisplayMetallic()
		{
			PixelBattleTextController.DisplayText(
				UnityEngine.Random.Range(100,999).ToString(),
				metallic,
				textSpawnPosition);

			lastUsed = metallic;
		}
		
		public void DisplayFreeze()
		{
			PixelBattleTextController.DisplayText(
				UnityEngine.Random.Range(100,999).ToString(),
				freeze,
				textSpawnPosition);

			lastUsed = freeze;
		}

		public void DisplayShock()
		{
			PixelBattleTextController.DisplayText(
				UnityEngine.Random.Range(100,999).ToString(),
				shock,
				textSpawnPosition);

			lastUsed = shock;
		}

		public void DisplayLvlUp()
		{
			PixelBattleTextController.DisplayText(
				"LEVEL UP!",
				lvlUp,
				textSpawnPosition);

			lastUsed = lvlUp;
		}
		
		public void DisplayDamage()
		{
			PixelBattleTextController.DisplayText(
				UnityEngine.Random.Range(100,999).ToString(),
				damage,
				textSpawnPosition);

			lastUsed = damage;
		}

		public void DisplayKO()
		{
			PixelBattleTextController.DisplayText(
				"KO",
				ko,
				textSpawnPosition);

			lastUsed = ko;
		}

		public void DisplayVenom()
		{
			PixelBattleTextController.DisplayText(
				"VENOM",
				venom,
				textSpawnPosition);

			lastUsed = venom;
		}

		public void DisplayHeal()
		{
			PixelBattleTextController.DisplayText(
				UnityEngine.Random.Range(100,999).ToString(),
				heal,
				textSpawnPosition);

			lastUsed = heal;
		}

		public void DisplayCrit()
		{
			PixelBattleTextController.DisplayText(
				UnityEngine.Random.Range(6780,9999).ToString(),
				criticalNumber,
				textSpawnPosition);

			PixelBattleTextController.DisplayText(
				"CRITICAL!",
				criticalText,
				textSpawnPosition + new float3(0,0.25f,0));

			lastUsed = criticalNumber;
		}
		public delegate void DisplayText();

		void Update(){
			if(Input.GetKeyDown(KeyCode.Space)){
				var randomPos = UnityEngine.Random.insideUnitCircle * new Vector2(.75f,.75f)+new Vector2(.5f,.5f);
				textSpawnPosition = new float3(randomPos,0);
				var randomMethod = UnityEngine.Random.Range(0,12);

				switch(randomMethod)
				{
					case 0:{
						DisplayCrit();
					break;
					}
					case 1:{
						DisplayDamage();
					break;
					}

					case 2:{
						DisplayFreeze();
					break;
					
					}
					case 3:{
						DisplayHeal();
					break;
					}
					case 4:{
						DisplayKO();
					break;

					}
					case 5:{
						DisplayLvlUp();
					break;
					}
					case 6:{
						DisplayMetallic();
					break;
					}
					case 7:{
						DisplayPremium();
					break;
					}
					case 8:{
						DisplayPyro();
					break;
					}
					case 9:{
						DisplayShock();
					break;
					}
					case 10:{
						DisplaySpooky();
					break;
					}
					case 11:{
						DisplayVenom();
					break;
					}

				}

			}
		}
#endregion
	}
}