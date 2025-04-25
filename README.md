# Animated Battle Text

> A simple, yet flexible Unity package for displaying animated text bursts directly in screen.
> Is designed for short, vibrant messages (damage counters, victory visual fanfare, status effects, etc)

## Usage

For displaying animated text using the package, you'll need to have a **PixelBattleTextController** instance in your scene referencing the **Canvas** that you want to display the text into.

Just by calling:

**PixelBattleTextController.DisplayText(** *message* **,** *animation* , *canvasPosition* **);**

### Example

```c#
using UnityEngine;
using PixelBattleText;


public class TestingPixelBattleText: MonoBehaviour {
    
    public TextAnimation textAnimation;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        PixelBattleTextController.DisplayText( "Hello World!" , textAnimation , Vector3.one * 0.5f);
    }

}
```

### Parameters
* **message**: A **string** containing the message to be displayed.
* **textAnimation**: The **TextAnimation** preset. A **ScriptableObject** with the animation configuration.
* **canvasPosition**: The *normalized* position for the text to be displayed relative to the UI canvas.

## Documentation
A more detailed explanation of the full extent of the package can be found in PDF format at:

https://github.com/ultraKelevra/AnimatedBattleText/blob/main/Assets/PixelBattleText/Documentation/PixelBattleText%20-%20Manual.pdf
