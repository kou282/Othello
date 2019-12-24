using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  //10*10のint型２次元配列を定義
  private int[,] squares = new int[8, 8];
  private const int EMPTY = 0;
  private const int WHITE = 1;
  private const int BLACK = -1;
    // Start is called before the first frame update
    private int currentPlayer = BLACK;

        //カメラ情報
        private Camera camera_obj;
        private RaycastHit hit;

        //prefabs
        public GameObject whiteStone;
        public GameObject blackStone;

        // Start is called before the first frame update
        void Start()
        {
            //カメラ情報を取得
            camera_obj = GameObject.Find("Main Camera").GetComponent<Camera>();
            //配列初期化
            InitArray();
            //デバッグ
            //DebugArray();
            //石初期位置
            //右上
            squares[4, 4] = WHITE;
            Instantiate(whiteStone,new Vector3(4,0,4),Quaternion.identity);
            //左下
            squares[3, 3] = WHITE;
            Instantiate(whiteStone,new Vector3(3,0,3),Quaternion.identity);
            //左上
            squares[4, 3] = BLACK;
            Instantiate(blackStone,new Vector3(3,0,4),Quaternion.identity);
            //右下
            squares[3, 4] = BLACK;
            Instantiate(blackStone,new Vector3(4,0,3),Quaternion.identity);
        }
        // Update is called once per frame
        void Update()
        {
            //マウスがクリックされたとき
            if (Input.GetMouseButtonDown(0)){
                //マウスのポジションを取得してRayに代入
                Ray ray = camera_obj.ScreenPointToRay(Input.mousePosition);
                //マウスのポジションからRayを投げて何かに当たったらhitに入れる
                if (Physics.Raycast(ray, out hit)){
                    //x,zの値を取得
                    int x = (int)hit.collider.gameObject.transform.position.x;
                    int z = (int)hit.collider.gameObject.transform.position.z;
                    //マスが空のとき
                    if(squares[z,x] == EMPTY){
                        //白のターンのとき
                        if(currentPlayer == WHITE){
                            //マスの値を更新して石を出力
                            PutWhite(x,z);
                            //Playerを交代
                            currentPlayer = BLACK;
                        }
                        //黒のターンのとき
                        else if(currentPlayer == BLACK){
                            //マスの値を更新して石を出力
                            PutBlack(x,z);
                            //Playerを交代
                            currentPlayer = WHITE;
                        }
                    }
                }
            }
        }

        //黒を置く
        private void PutBlack(int x,int z){
          //値の更新
          squares[z, x] = BLACK;
          //石の出力
          Instantiate(blackStone,new Vector3(x,0,z),Quaternion.identity);
        }

        //白を置く
        private void PutWhite(int x,int z){
          //値の更新
          squares[z, x] = WHITE;
          //石の出力
          Instantiate(whiteStone,new Vector3(x,0,z),Quaternion.identity);
        }
        
    //配列の初期化
    private void InitArray(){
        for (int i = 0; i < 8;i++){
            for (int j = 0; j < 8;j++){
                squares[i, j] = EMPTY;
            }
        }
    }

    //デバッグ用
    private void DebugArray(){
        for (int i = 0; i < 8; i++){
            for (int j = 0; j < 8; j++){
                Debug.Log("(i,j) = (" + i + "," + j + ") = " + squares[i, j]);
            }
        }
    }
}
