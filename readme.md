# Limitless Path Generation System for Locomotion in VR 🥽
<pre>
<img src="https://github.com/raghavmittal101/test-quest-2/raw/master/Demo_of_limitless_art_gallery.gif" alt="Demo video" style="height:200px;"/>         <img src="https://github.com/raghavmittal101/test-quest-2/raw/master/pathGenWithBoundaryGIF.gif" alt="Path Simulation" style="width:250px;"/>

</pre>
### [Demo video 📺](https://www.youtube.com/watch?v=tmBYfRN74mk) 

### [Releases](https://github.com/raghavmittal101/test-quest-2/releases)

### [Publication](https://www.springerprofessional.de/en/designing-limitless-path-in-virtual-reality-environment/19324810)

Walking in a Virtual Environment is a bounded task. It is challenging for a subject to navigate a large virtual environment designed in a limited physical space. External hardware support may be required to achieve such an act in a concise physical area without compromising navigation and virtual scene rendering quality. This paper proposes an algorithmic approach to let a subject navigate a limitless virtual environment within a limited physical space with no additional external hardware support apart from the regular Head-Mounted-Device (HMD) itself. As part of our work, we developed a Virtual Art Gallery as a use-case to validate our algorithm. We conducted a simple user-study to gather feedback from the participants to evaluate the ease of locomotion of the application. The results showed that our algorithm could generate limitless paths of our use-case under predefined conditions and can be extended to other use-cases.

---

#### Project structure
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
6. To build the enviroment, go to build settings and add active scenes in the following sequence 
``` 
1. ui_scene.unity 
2. SampleScene.unity
```

### Settings
Choose the `ScriptObject` asset in the hierarchy window. In the inspector panel, you will find many customisation options.
1. **`Input Device Type`**: let's you choose the medium through which you want to experience the environment.
    1. `Manual Input`: choose this option to run the environment inside Unity. This is helpful in testing and simulation for rapid prototyping.
    2. `Oculus VR`: choose this option to run the environment on Oculus Quest/Quest 2 VR. You need to build the enviroment after choosing this option. 
2. **`Metadata Input Context`**: let's you choose how you decide the source of metadata properties and static assets for environment.
    1. `Manual Input` : If chosen, the system will fetch the metadata values and assets entered in the inspector panel.
    2. `Online Input` : The system will use the `Metadata API URL` and `Doc Id` to fetch the metadata and assets from an online database.
3. **`Configuration ID Entering Method`**: decides the medium for fetching the metadata if `Metadata Input Context` is set to `Online Input`.
    1. `Enter in Editor` : The system will fetch the value entered in `Doc Id` field in the inspector panel.
    2. `Enter in UI` : The system will ask you to enter the `Doc_Id` when inside the VR environment on a headset.

NOTE: Choosing `Metadata Input Context > Manual Input` will deactivate the `Configuration ID Entering Method` settings as that setting won't be required to set.

NOTE: We have developed an online metadata input application which consists of an API and MongoDB database. Use [http://pathgen-input.herokuapp.com/metadata](http://pathgen-input.herokuapp.com/metadata) as `Metadata API URL`. To create a configuration document, visit [http://pathgen-input.herokuapp.com/](http://pathgen-input.herokuapp.com/). For more details regarding its implementation, visit [https://github.com/raghavmittal101/metadataInputSystem](https://github.com/raghavmittal101/metadataInputSystem).

<img src="https://github.com/raghavmittal101/test-quest-2/raw/master/limitless_path_gen%20steps.png" alt="Demo video"/> 

## How to experience?
After you are done with setting up and have loaded the build onto your Oculus Quest headset, below are the further instructions.
* Before running the application, make sure that you have setup the room-scale guardian in a rectangular room. Space requirements may vary based on the path properties set by the scene designer.
* Stand in the middle of the room space.
* Start the app
* Enter the `document ID` provided by the scene designer in the text box and hit `Go` button.
* Wait for sometime as the system will download the scene assets. (waiting duration may vary based on the Internet speed)
* After the scene is loaded, you can walk in forward direction for as long as you want.
* You can terminate the scene anytime by clicking on `B` button on the right controller.


#### Copyright Raghav Mittal 2021
