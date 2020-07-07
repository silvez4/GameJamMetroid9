
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelTranspiler : MonoBehaviour
{
    public Texture2D levelDesign;
    private Tilemap tmGround;
    public TileBase[] tileBases;
    public GameObject[] objects;

    private int[,] map;
    private bool testDoor = false;
    private bool playerSpawned = false;

    private GameObject playerObj;

    //==
    private Vector2Int coord;
    // Start is called before the first frame update
    void Start()
    {
        map = new int[levelDesign.width, levelDesign.height];

        tmGround = GameObject.Find("Tilemap@Ground").GetComponent<Tilemap>();
        for (int x=0; x<levelDesign.width;x++){
            for (int y=levelDesign.height-1; y>=0; y--){
                if (map[x,y] != 1){
                    ColorDeal(new Vector2Int(x,y), levelDesign.GetPixel(x,y));
                }
            }
        }
    }

    private void ColorDeal(Vector2Int coord, Color color){
        this.coord = coord;

        if (color == FindColor("#000")) {
            SetTile(0);
        }
        if (color == FindColor("#a2f3a2") && !playerSpawned){

            GameObject.Find("Player@GameObject").transform.position = new Vector3(coord.x, coord.y,0);

            playerSpawned = true;
        }
        /*if (color == FindColor("#a2f3a2") && !playerSpawned){

            SetObject(0);

            playerSpawned = true;
        }*/

        if (color == FindColor("#797979") && !testDoor) {
            SetObject(1);
            testDoor = true;
        }

        if (color == FindColor("#305182")){
            SetObject(2);
        }

        if (color == FindColor("#794100")){
            SetObject(3);
        }
        map[coord.x, coord.y] = 1; 
    }

    //0 = ground
    private void SetTile( int type){
        tmGround.SetTile(new Vector3Int(coord.x,coord.y,0), tileBases[0]);
    }

    //0 = door
    private void SetObject( int obj){
        Instantiate(objects[obj], new Vector3(coord.x+0.5f, coord.y+0.5f,0), Quaternion.identity);


        /*for (int i=coord.x; i<coord.x+2; i++){
            for (int j=(coord.y+4)-1; j>=coord.y; j--){
              //  map[i,j] = 1;
            }
        }*/
    }

    private Color FindColor(string hex){
        Color colOut;
        ColorUtility.TryParseHtmlString(hex, out colOut);
        return colOut;
     }
}