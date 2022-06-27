using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTileOrange, groundTileBlue;
    public GameObject rightWallOrange, leftWallOrange, rightWallBlue,leftWallBlue;
    public Material orangeLight, blueLight;
    public bool orangeTileType = true; // orangeTileType = true(for orange tile), = false(for blue tile)
    Vector3 nextSpawnPoint;
    GameObject[] heirarchy;

    // MeshRenderer[] mesh;
    // SpriteRenderer[] sprite;

    public void spawnTile()
    {
        if(orangeTileType)
        {
            GameObject temp = Instantiate(groundTileOrange,nextSpawnPoint,Quaternion.identity);
            nextSpawnPoint = temp.transform.GetChild(3).transform.position;
        }
        else
        {
            GameObject temp = Instantiate(groundTileBlue,nextSpawnPoint,Quaternion.identity);
            nextSpawnPoint = temp.transform.GetChild(3).transform.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            spawnTile();
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            orangeTileType = !orangeTileType;
            heirarchy = GameObject.FindGameObjectsWithTag("Wall");
            
            if(orangeTileType){
                foreach(GameObject g in heirarchy){
                    if(g.transform.GetChild(1).gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial == blueLight){    
                        GameObject rightWall = g.transform.GetChild(1).gameObject;
                        GameObject leftWall = g.transform.GetChild(2).gameObject;

                        MeshRenderer[] meshR = rightWall.GetComponentsInChildren<MeshRenderer>();
                        SpriteRenderer[] spriteR = rightWall.GetComponentsInChildren<SpriteRenderer>();

                        MeshRenderer[] meshL = leftWall.GetComponentsInChildren<MeshRenderer>();
                        SpriteRenderer[] spriteL = leftWall.GetComponentsInChildren<SpriteRenderer>();

                        foreach(MeshRenderer m in meshR){
                            if(!(m.gameObject.tag == "BlackWall")){
                                m.sharedMaterial = orangeLight;
                            }
                        }

                        foreach(SpriteRenderer s in spriteR){
                            s.sharedMaterial = orangeLight;
                        }

                        foreach(MeshRenderer m in meshL){
                            if(!(m.gameObject.tag == "BlackWall")){
                                m.sharedMaterial = orangeLight;
                            }
                        }

                        foreach(SpriteRenderer s in spriteL){
                            s.sharedMaterial = orangeLight;
                        }
                    }
                }
            }
            else{
                foreach(GameObject g in heirarchy){
                    if(g.transform.GetChild(1).gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial == orangeLight){
                        GameObject rightWall = g.transform.GetChild(1).gameObject;
                        GameObject leftWall = g.transform.GetChild(2).gameObject;

                        MeshRenderer[] meshR = rightWall.GetComponentsInChildren<MeshRenderer>();
                        // Renderer[] meshR = rightWall.GetComponentsInChildren<Renderer>();
                        SpriteRenderer[] spriteR = rightWall.GetComponentsInChildren<SpriteRenderer>();

                        MeshRenderer[] meshL = leftWall.GetComponentsInChildren<MeshRenderer>();
                        // Renderer[] meshL = leftWall.GetComponentsInChildren<Renderer>();
                        SpriteRenderer[] spriteL = leftWall.GetComponentsInChildren<SpriteRenderer>();

                        foreach(MeshRenderer m in meshR){
                            if(!(m.gameObject.tag == "BlackWall")){
                                m.sharedMaterial = blueLight;
                            }
                        }

                        foreach(SpriteRenderer s in spriteR){
                            s.sharedMaterial = blueLight;
                        }

                        foreach(MeshRenderer m in meshL){
                            if(!(m.gameObject.tag == "BlackWall")){
                                m.sharedMaterial = blueLight;
                            }
                        }

                        foreach(SpriteRenderer s in spriteL){
                            s.sharedMaterial = blueLight;
                        }
                    }
                }
            }

            // Not good method as it destroy and instantiates new objects which could cause errors/bugs and cause fps drop
            // if(orangeTileType){
            //     foreach(GameObject g in heirarchy){
            //         Debug.Log("Inside orangeTileType");
            //         if(g.transform.name == "GroundTileBlue(Clone)"){
            //             GameObject g1,g2,rightWall,leftWall;
            //             g1 = rightWallOrange;
            //             g2 = leftWallOrange;

            //             rightWall = Instantiate(g1,g.transform.GetChild(1).transform.position,g.transform.GetChild(1).rotation);
            //             leftWall = Instantiate(g2,g.transform.GetChild(2).transform.position,g.transform.GetChild(2).rotation);
            //             Destroy(g.transform.GetChild(1).gameObject,0);
            //             Destroy(g.transform.GetChild(2).gameObject,0);

            //             Destroy(rightWall,4);
            //             Destroy(leftWall,4);

            //             // rightWall.transform.SetParent(g.transform.parent);
            //             // leftWall.transform.SetParent(g.transform.parent);


            //         }
            //         else if(g.transform.name == "RightWallBlue(Clone)"){
            //             GameObject g1 = rightWallOrange;
            //             GameObject rightWall = Instantiate(g1,g.transform.position,g.transform.rotation);
            //             Destroy(g.gameObject,0);
            //             Destroy(rightWall,4);

            //         }
            //         else if(g.transform.name == "LeftWallBlue(Clone)"){
            //             GameObject g1 = leftWallOrange;
            //             GameObject leftWall = Instantiate(g1,g.transform.position,g.transform.rotation);
            //             Destroy(g.gameObject,0);
            //             Destroy(leftWall,4);
            //         }
            //     }
            // }
            // else{
            //     foreach(GameObject g in heirarchy){
            //         Debug.Log("Inside !orangeTileType");
            //         if(g.transform.name == "GroundTileOrange(Clone)"){
            //             GameObject g1,g2,rightWall,leftWall;
            //             g1 = rightWallBlue;
            //             g2 = leftWallBlue;

            //             rightWall = Instantiate(g1,g.transform.GetChild(1).transform.position,g.transform.GetChild(1).rotation);
            //             leftWall = Instantiate(g2,g.transform.GetChild(2).transform.position,g.transform.GetChild(2).rotation);
            //             Destroy(g.transform.GetChild(1).gameObject,0);
            //             Destroy(g.transform.GetChild(2).gameObject,0);

            //             Destroy(rightWall,4);
            //             Destroy(leftWall,4);

            //             // rightWall.transform.SetParent(g.transform.parent);
            //             // leftWall.transform.SetParent(g.transform.parent);
            //         }
            //         else if(g.transform.name == "RightWallOrange(Clone)"){
            //             GameObject g1 = rightWallBlue;
            //             GameObject rightWall = Instantiate(g1,g.transform.position,g.transform.rotation);
            //             Destroy(g.gameObject,0);
            //             Destroy(rightWall,4);
            //         }
            //         else if(g.transform.name == "LeftWallOrange(Clone)"){
            //             GameObject g1 = leftWallBlue;
            //             GameObject leftWall = Instantiate(g1,g.transform.position,g.transform.rotation);
            //             Destroy(g.gameObject,0);
            //             Destroy(leftWall,4);
            //         }
            //     }
            // }




            // Not working, directly changing the material of prefab doesnt change it in the scene
            // if(orangeTileType){
            //     mat = orangeLight;
            // }
            // else{
            //     mat = blueLight;
            // }

            // GameObject rightWall = groundTileOrange.transform.GetChild(1).gameObject;
            // GameObject leftWall = groundTileOrange.transform.GetChild(2).gameObject;

            // MeshRenderer[] meshR = rightWall.GetComponentsInChildren<MeshRenderer>();
            // SpriteRenderer[] spriteR = rightWall.GetComponentsInChildren<SpriteRenderer>();

            // MeshRenderer[] meshL = leftWall.GetComponentsInChildren<MeshRenderer>();
            // SpriteRenderer[] spriteL = leftWall.GetComponentsInChildren<SpriteRenderer>();

            // foreach(MeshRenderer m in meshR){
            //     if(!(m.gameObject.tag == "BlackWall")){
            //         m.sharedMaterial = mat;
            //     }
            // }
            // foreach(SpriteRenderer s in spriteR){
            //     s.sharedMaterial = mat;
            // }

            // foreach(MeshRenderer m in meshL){
            //     if(!(m.gameObject.tag == "BlackWall")){
            //         m.sharedMaterial = mat;
            //     }
            // }
            // foreach(SpriteRenderer s in spriteL){
            //     s.sharedMaterial = mat;
            // }





            // //extra
            // if(!orangeTileType){
            //     mat = blueLight;
            // }
            // else{
            //     mat = orangeLight;
            // }

            // mesh = rightWallOrange.GetComponentsInChildren<MeshRenderer>();
            // sprite = rightWallOrange.GetComponentsInChildren<SpriteRenderer>();
            // foreach(MeshRenderer m in mesh){
            //     if(!(m.gameObject.tag == "BlackWall"))
            //         m.sharedMaterial = mat;
            // }
            // foreach(SpriteRenderer s in sprite){
            //         s.sharedMaterial = mat;
            // }

            // mesh = leftWallOrange.GetComponentsInChildren<MeshRenderer>();
            // sprite = leftWallOrange.GetComponentsInChildren<SpriteRenderer>();
            // foreach(MeshRenderer m in mesh){
            //     if(!(m.gameObject.tag == "BlackWall"))
            //         m.sharedMaterial = mat;
            // }
            // foreach(SpriteRenderer s in sprite){
            //     s.sharedMaterial = mat;
            // }

        }
    }
}
