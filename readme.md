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
## How to build the environment?
### Requirements
1. Unity3D 2018 LTS or later with Android build support with Android NDK and SDK tools and OpenJDK options selected.
2. Latest version of the project. Download from [here](https://github.com/raghavmittal101/test-quest-2/releases).
3. Oculus Quest or Quest 2, if you want to experience it. Headset is not required for simulation puposes.
4. Internet connection with wireless LAN if you intend to use a headset.

### Steps
1. Extract the downloaded _limitless path generation_ project.
2. Open the project in Unity.
3. Open `Assets > Scenes > ui_scene.unity`.
4. In the hierarchy window, choose the select `ScriptObject`.
5. There are multiple ways to experience the environment. (refer to settings)
    1. Simulate/test the environment in Unity for development purpose
    2. Build the environment and load it onto headset
6. To build the enviroment, add _ui_scene_ at the 0th position and _SampleScene_ at the 1st position in the build settings.

#### Settings
Choose the `ScriptObject` asset in the hierarchy window. In the inspector panel, under the _Input Device Context_ and _Metadata Input Context_ settings, you will find many customisation options. These settings are valid in certain combinations only (to be discussed later).
1. **Input Device Type**: let's you choose the medium through which you want to experience the environment.
    1. _Manual Input_: choose this option to run the environment inside Unity. This is helpful in testing and simulation for rapid prototyping.
    2. _Oculus VR_: choose this option to run the environment on Oculus Quest/Quest 2 VR. You need to build the enviroment after choosing this option. 
2. **Metadata Input Context**: let's you choose how you decide the source of metadata properties and static assets for environment.
    1. _Manual Input_ : If chosen, the system will fetch the metadata values and assets entered in the inspector panel.
    2. _Online Input_ : The system will use the _Metadata API URL_ and _Doc Id_ to fetch the metadata and assets from an online database.
3. **Configuration ID Entering Method**: decides the medium for fetching the metadata if _Metadata Input Context_ is set to _Online Input_.
    1. _Enter in Editor_ : The system will fetch the value entered in _Doc Id_ field in the inspector panel.
    2. _Enter in UI_ : The system will ask you to enter the _Doc_Id_ when inside the VR environment on a headset.

NOTE: Choosing **Metadata Input Context**>_Manual Input_ will deactivate the **Configuration ID Entering Method** settings as that setting won't be required to set.

NOTE: We have developed an online metadata input application which consists of an API and MongoDB database. Use [http://pathgen-input.herokuapp.com/metadata](http://pathgen-input.herokuapp.com/metadata) as _Metadata API URL_. To create a configuration document, visit [http://pathgen-input.herokuapp.com/](http://pathgen-input.herokuapp.com/).

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
