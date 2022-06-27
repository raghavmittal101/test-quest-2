using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;
    PlayerController playerController;

    // GameObject collidedGate;

    // Start is called before the first frame update
    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        playerController = GetComponent<PlayerController>();
        SpawnObstacleGate();
    }

    public void OnTriggerEnter(Collider other) 
    {
        if(!(other.gameObject.tag == "Disc"))
        {
            groundSpawner.spawnTile();
            Destroy(gameObject,4);
        }
        
        // if(!playerController.isGatePassed && collidedGate == null){
        //     PlayerManager.gameOver = true;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject forcefield_1,forcefield_duck,forcefield_jump,gateOrange_1,gateBlue_1,turretOrange,turretBlue,disc_coin;

    void SpawnObstacleGate()
    {
        int obstacleType = Random.Range(0,4); // obstacleType = 0(means different forcefields), = 1(means different gates), = 2(means turrets), = 3(means coins)

        if(obstacleType == 0)
        {
            SpawnForceField();
        }
        else if(obstacleType == 1){
            SpawnGate();
        }
        else if(obstacleType == 2){
            SpawnTurret();
        }
        else{
            SpawnCoin();
        }
    }

    void SpawnForceField()
    {
        int forceFieldType = Random.Range(0,2); // forceFieldType = 0(means forcefield_1), = 1(means forcefield_duck or forcefield_jump)
        int obstacleNumber, obstacleIndex; // obstacleNumber(either 1,2 or 3 obstacles), obstacleIndex(either 4,5 or 6) 
        Transform spawnPoint;

        obstacleIndex = Random.Range(4,7); // if obstacleNumber = 1 choose this index, else if obstacleNumber = 2 choose other 2 indices
        if(forceFieldType == 0)
        {
            obstacleNumber = Random.Range(1,3);
            if(obstacleNumber == 1)
            {
                spawnPoint = transform.GetChild(obstacleIndex).transform;
                spawnPoint.position = new Vector3(spawnPoint.position.x,5.5f,spawnPoint.position.z);
                Instantiate(forcefield_1,spawnPoint.position,Quaternion.identity,transform);
            }
            else{
                for(int i=4;i<=6;i++)
                {
                    if(obstacleIndex != i)
                    {
                        spawnPoint = transform.GetChild(i).transform;
                        spawnPoint.position = new Vector3(spawnPoint.position.x,5.5f,spawnPoint.position.z);
                        // Instantiate(forcefield_1,spawnPoint.position,Quaternion.identity,transform);
                    }
                }
            }
        }
        else
        {
            obstacleNumber = Random.Range(1,4);
            int obstacleSubType; // obstacleSubType = 0(means forcefield_duck), = 1(means forcefield_jump)
            if(obstacleNumber == 1)
            {
                obstacleSubType = Random.Range(0,2);
                spawnPoint = transform.GetChild(obstacleIndex).transform;
                if(obstacleSubType == 0)
                {
                    spawnPoint.position = new Vector3(spawnPoint.position.x,5.5f,spawnPoint.position.z);
                    Instantiate(forcefield_duck,spawnPoint.position,Quaternion.identity,transform);
                }
                else
                {
                    spawnPoint.position = new Vector3(spawnPoint.position.x,5.5f,spawnPoint.position.z);
                    Instantiate(forcefield_jump,spawnPoint.position,Quaternion.identity,transform);
                }
                
            }
            else if(obstacleNumber == 2){
                for(int i=4;i<=6;i++)
                {
                    if(obstacleIndex != i)
                    {
                        obstacleSubType = Random.Range(0,2);
                        spawnPoint = transform.GetChild(i).transform;
                        if(obstacleSubType == 0)
                        {
                            spawnPoint.position = new Vector3(spawnPoint.position.x,5.5f,spawnPoint.position.z);
                            // Instantiate(forcefield_duck,spawnPoint.position,Quaternion.identity,transform);
                        }
                        else
                        {
                            spawnPoint.position = new Vector3(spawnPoint.position.x,5.5f,spawnPoint.position.z);
                            // Instantiate(forcefield_jump,spawnPoint.position,Quaternion.identity,transform);
                        }
                    }
                }
            }
            else
            {
                for(int i=4;i<=6;i++)
                {
                    obstacleSubType = Random.Range(0,2);
                    spawnPoint = transform.GetChild(i).transform;
                    if(obstacleSubType == 0)
                    {
                        spawnPoint.position = new Vector3(spawnPoint.position.x,5.5f,spawnPoint.position.z);
                        // Instantiate(forcefield_duck,spawnPoint.position,Quaternion.identity,transform);
                    }
                    else
                    {
                        spawnPoint.position = new Vector3(spawnPoint.position.x,5.5f,spawnPoint.position.z);
                        // Instantiate(forcefield_jump,spawnPoint.position,Quaternion.identity,transform);
                    }
                }
            }
        }
    }

    void SpawnGate()
    {
        int gateType = Random.Range(0,2); // gateType = 0(means gateOrange_1), = 1(means gateBlue_1)
        int obstacleNumber, obstacleIndex;
        GameObject gate;
        Transform spawnPoint;

        obstacleNumber = Random.Range(1,4);
        obstacleIndex = Random.Range(4,7);

        if(gateType == 0)
        {
            gate = gateOrange_1;

        }
        else
        {
            gate = gateBlue_1;
        }

        if(obstacleNumber == 1)
        {
            spawnPoint = transform.GetChild(obstacleIndex).transform;
            spawnPoint.position = new Vector3(spawnPoint.position.x,5.5f,spawnPoint.position.z);
            Instantiate(gate,spawnPoint.position,Quaternion.identity,transform);    
        }
        else if(obstacleNumber == 2){
            for(int i=4;i<=6;i++)
            {
                if(obstacleIndex != i)
                {
                    spawnPoint = transform.GetChild(i).transform;
                    spawnPoint.position = new Vector3(spawnPoint.position.x,5.5f,spawnPoint.position.z);
                    // Instantiate(gate,spawnPoint.position,Quaternion.identity,transform);
                }
            }
        }
        else
        {
            for(int i=4;i<=6;i++)
            {
                spawnPoint = transform.GetChild(i).transform;
                spawnPoint.position = new Vector3(spawnPoint.position.x,5.5f,spawnPoint.position.z);
                // Instantiate(gate,spawnPoint.position,Quaternion.identity,transform);
            }
        }
    }


    void SpawnTurret()
    {
        int turretColor = Random.Range(0,2);// turretColor = 0(means turrentOrange), = 1(means turretBlue)
        int obstacleNumber, obstacleIndex;  
        GameObject turret;
        Transform spawnPoint;

        obstacleNumber = Random.Range(1,4);
        obstacleIndex = Random.Range(4,7);


        if(turretColor == 0)
        {
            turret = turretOrange;

        }
        else
        {
            turret = turretBlue;
        }

        if(obstacleNumber == 1)
        {
            spawnPoint = transform.GetChild(obstacleIndex).transform;
            spawnPoint.position = new Vector3(spawnPoint.position.x,0f,spawnPoint.position.z);
            Instantiate(turret,spawnPoint.position,Quaternion.identity,transform);    
        }
        else if(obstacleNumber == 2){
            for(int i=4;i<=6;i++)
            {
                if(obstacleIndex != i)
                {
                    spawnPoint = transform.GetChild(i).transform;
                    spawnPoint.position = new Vector3(spawnPoint.position.x,0f,spawnPoint.position.z);
                    Instantiate(turret,spawnPoint.position,Quaternion.identity,transform);
                }
            }
        }

    }

    void SpawnCoin()
    {
        Transform spawnPoint;
        int obstacleNumber, obstacleIndex;
        obstacleNumber = Random.Range(1,4);
        obstacleIndex = Random.Range(4,7);

        if(obstacleNumber == 1)
        {
            spawnPoint = transform.GetChild(obstacleIndex).transform;
            spawnPoint.position = new Vector3(spawnPoint.position.x,3f,spawnPoint.position.z);
            Instantiate(disc_coin,spawnPoint.position,Quaternion.identity,transform);    
        }
        else if(obstacleNumber == 2){
            for(int i=4;i<=6;i++)
            {
                if(obstacleIndex != i)
                {
                    spawnPoint = transform.GetChild(i).transform;
                    spawnPoint.position = new Vector3(spawnPoint.position.x,3f,spawnPoint.position.z);
                    Instantiate(disc_coin,spawnPoint.position,Quaternion.identity,transform);
                }
            }
        }
        else
        {
            for(int i=4;i<=6;i++)
            {
                spawnPoint = transform.GetChild(i).transform;
                spawnPoint.position = new Vector3(spawnPoint.position.x,3f,spawnPoint.position.z);
                Instantiate(disc_coin,spawnPoint.position,Quaternion.identity,transform);
            }
        }
    }


}
