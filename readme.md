# Limitless Path Generation System for Locomotion in VR
## How to setup?
* Download the Unity package from [here](https://github.com/raghavmittal101/test-quest-2/releases).
* Import the package into Unity 2018.x or above.
* Open `Scenes > ui_scene.unity`
* Go to build settings and add active scenes in the following sequence 
``` 
1. ui_scene.unity 
2. SampleScene.unity
```
## What is a document ID and how to generate it?
The limitless path generation system consists of two user interfaces: 1. Inside VR, for the participant and 2. a web interface for the developer
to enter environment configuration details a.k.a. metadata. `Metadata input system` provides this interface. The details entered are stored onto a MongoDB
cloud server. The system assigns and returns a document ID for to the developer which can be shared with the participants. When participant enters this ID in the VR interface, the path generation system fetches the configurations from the metadata input system.

For more details regarding metadata input system, visit [here](https://github.com/raghavmittal101/metadataInputSystem).

* The package consists of the following components:
```
Assets/
    Scenes/
        SampleScene.unity
        ui_scene.unity
    my scripts/
        Spawner.cs
        MetadataInputContext.cs
        InputDeviceContext.cs
        DataLogger.cs
        entryScene.cs
        trying scripts/
                DetectBoundaryTestScript.cs
                DetectBoundaryTestScriptDynamic.cs
                DetectBoundaryFixedDirections.cs
                WallsSpawner.cs

```

## How to experience?
After you are done with setting up and have loaded the build onto your Oculus Quest headset, below are the further instructions.
* Before running the application, make sure that you have setup the room-scale guardian in a rectangular room. Space requirements may vary based on the path properties set by the scene designer.
* Stand in the middle of the room space.
* Start the app
* Enter the `document ID` provided by the scene designer in the text box and hit `Go` button.
* Wait for sometime as the system will download the scene assets. (waiting duration may vary based on the Internet speed)
* After the scene is loaded, you can walk in forward direction for as long as you want.
* You can terminate the scene anytime by clicking on `B` button on the right controller.

## Releases
### vAlpha1 19/dec/2020
- online resource fetching system integration for fetching metadata and images from cloud

#### Copyright Raghav Mittal 2021
