using UnityEngine;
using PixelBattleText;

public class TestingBattleText : MonoBehaviour
{
    public TextAnimation textAnimation;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            PixelBattleTextController.DisplayText("Hello World!", textAnimation, Vector3.zero);
    }
}

