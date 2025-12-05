# Untitled Ant Game (UAG)

Untitled Ant Game (UAG) is a 2D, top-down survival game in which you play as an ant. Your goal is to escape a spider-infested cave by breaking and adding blocks while having limited vision.

We used techniques such as chunking, Marching Squares and a solution to the Boundary Edge Detection problem to generate terrain and collisions. We also used the stencil buffer to create a Fog of War effect with a raycast-built vision polygon, along with shader-based gaussian blur and vignette as post-processing effects, to make it look very very cool.

The spider behaviour uses breadcrumb pathfinding, i.e., spiders store a trail of the player's previous positions, which they follow if they lose sight of the player. They can be aggroed by proximity, and deaggroed with the same logic.

## How to build/run?

To play and/or build Untitled Ant Game (UAG), you'll need to use the Unity Editor. During development,
the specific version we used was Unity ```6000.0.60f1```. We recommend using the same version.

Nothing *should* break if you decide to use a different version, but if something does, please try
using the recommended version. Unity packages might get updated between editor releases, and this could cause problems.

## Inside Unity

Inside the Unity Editor, you can test the game right away by opening the ```Menu``` scene found in the ```Scenes/``` folder.
If you want to skip the main menu, load the ```Game``` scene instead. Pressing the play button in the top center of the Unity Editor will run it in an embedded window.

## Building

To build the game as a Windows executable, press the ```File``` button in the top navigation toolbar, followed by the ````Build Profiles``` button. The menu and game scenes should already be added to the scene list.

From here, press ```Build``` and select the desired output folder. Unity will take care of building and generating an executable.

The game's input system is configured to use the keyboard and mouse by default. You can also technically configure the build system for other platforms and input devices, but this requires
a bit of extra work, and is not included in the scope of this project.

## I just want to play!

If you don't want to go through the hassle of building the game yourself, you can find the latest build at the [itch.io page](https://danee2108.itch.io/untitled-ant-game).
