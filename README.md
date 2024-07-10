# PlayerCustomization for Zumbi Blocks 2

PlayerCustomization is a framework for adding custom player skins in [Zumbi Blocks 2](https://store.steampowered.com/app/1941780/Zumbi_Blocks_2_Open_Alpha/), all without touching a single line of code.

If playing on multiplayer, all players must have the mod and skin files to see each other's skins.

## Adding a Skin

PlayerCustomization allows for two different ways to add a skin: a JSON file containing hexadecimal colors, and a PNG file containing a color palette.  
Skins made with JSON files will be listed before skins made with PNG files, due to them retaining the game's vanilla style.  
Unlike JSON files, PNG files allow for customization of the eye. You can add a design or keep it simple and flat.

Once done making your skin, create a folder for your mod in the BepInEx `plugins` folder, and place your skin files inside it.

### Using a JSON file

JSON files take the form of `"key": "value"`, where key is the name of the body you wish to customize, and value is a hexadecimal color.  
The file starts with an open brace (`{`) and ends with a closed brace (`}`). The key-value pairs are separated by commas (`,`).  
File names must follow the format `*.skin.json`, where `*` can be anything.

The available keys are:
- `Skin`: the player's skin color
- `Mouth`: the player's mouth color
- `ShirtPrimary`: the base color for the shirt
- `ShirtSecondary`: the color for the shirt sleeves
- `BottomPrimary`: the base color for the pants (M) or shorts (F)
- `BottomSecondary`: the bottom of the pants (M) or base for the trouser/pants leg (F)
- `BeltPrimary`: the base color for the belt
- `BeltSecondary`: the color for the buckle
- `HairPrimary`: main hair color (F)
- `HairSecondary`: accented hair color (F), usually used for shading
- `BootsPrimary`: the base color for the boots (F)
- `BootsSecondary`: the accented color for the boots and bottom (F)
- `Eye`: color of the sclera (white part of the eye)
- `Pupil`: color of the pupil (main eye color)

All keys are optional. If a color isn't provided for a part, a default color is used. This color comes from the default black and blue skin, Kuba.  

#### Examples

If you'd like to change the player's skin color and nothing else, you could write a file like this:

```json
{
  "Skin": "AA8E8A"
}
```

If you'd like to customize the player's shirt and bottom, you could write one like so:

```json
{
  "ShirtPrimary": "#fdc4ff",
  "ShirtSecondary": "#fdc4ff",
  "BottomPrimary": "#b38ef3",
  "BottomSecondary": "#b38ef3"
}
```

As you can see, the color is case-insensitive and can even be written without the `#` character.  
Do note that all lines **except** the last one must end with a comma.

### Using a PNG file

PNG files are 128px wide and 128px tall. They are divided into 16 squares, with 4 rows and 4 columns. Each square is 32px wide and 32px tall.  
File names must follow the format `*.skin.png`, where `*` can be anything.

|           |  **Column 1**   | **Column 2**  | **Column 3**  |  **Column 4**  |
|:---------:|:---------------:|:-------------:|:-------------:|:--------------:|
| **Row 1** |      Skin       |     Mouth     | ShirtPrimary  | ShirtSecondary |
| **Row 2** | BottomSecondary | BottomPrimary | BeltSecondary |  BeltPrimary   |
| **Row 3** |   HairPrimary   | HairSecondary | BootsPrimary  | BootsSecondary |
| **Row 4** |     Unused      |    Unused     |    Unused     |  Eye & Pupil   |

All squares (minus the unused and Eye & Pupil ones) must be a flat, solid color.  
You cannot have a custom design, unfortunately. The player model's UV wrapping is far too small to allow any intricate designs.  
The only square that allows for a custom design is Eye & Pupil.
