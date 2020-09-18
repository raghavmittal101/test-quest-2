# BetaRangeManager.cs

#### Class declaration
```csharp
public class DetectBoundariesTrigno: MonoBehaviour
```

#### Private variables

##### Vector3[] rayDirectionArray and RaycastHit[] rayArray
`rayDirectionArray` for storing vectors to globle NESW
directions and `rayArray` for storing rays casted into those directions.

|Index|Vector3 RayDirection[]|Direction|
|---|---|---|
|0|(0f, 0f, 1f)|north|
|1|(1f, 0f, 0f)|east|
|2|(0f, 0f, -1f)|south|
|3|(-1f, 0f, 0f)|west|

```csharp
Vector3[] rayDirectionArray = { new Vector3(0f, 0f, 1f),
                                new Vector3(1f, 0f, 0f),
                                new Vector3(0f, 0f, -1f),
                                new Vector3(-1f, 0f, 0f) };
RaycastHit north = new RaycastHit();
RaycastHit east = new RaycastHit();
RaycastHit south = new RaycastHit();
RaycastHit west = new RaycastHit();
RaycastHit[] rayArray = new RaycastHit[4];
```
```csharp
private void Awake()
    {
        rayArray[0] = north;
        rayArray[1] = east; 
        rayArray[2] = south;
        rayArray[3] = west;    
	}
```

### Methods
#### GenerateGamma