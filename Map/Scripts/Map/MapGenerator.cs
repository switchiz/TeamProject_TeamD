using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{

    public GameObject map; // 첫 맵 사이즈 체크용
    public GameObject line; // 나눠진 공간 체크용
    public GameObject roomLine; //방 사이즈 체크용
    public Vector2Int mapSize; //만들고 싶은 맵의 크기
    public float minimumDevidedRate; // 공간이 나눠지는 최소 비율
    public float maximumDevidedRate; // 최대 비율
    public int maximumDepth; // 트리의 높이, 맵을 2^높이 만큼 나눔



    public LineRenderer lineRenderer;
    //에디터에서 맵 다시그릴때 렌더러 전부 지우고 다시 그리게하기 위한 리스트
    private List<LineRenderer> lineRenderers = new List<LineRenderer>(); 


    void Start()
    {


        Node root = new Node(new RectInt(0, 0, mapSize.x, mapSize.y)); //전체 맵 크기의 루트노드를 만듬
        DrawMap(0, 0);
        Divide(root, 0);
        GenerateRoom(root, 0);
    }




    private void DrawMap(int x, int y) //x y는 화면의 중앙위치를 뜻한다.
    {
        // -mapsize/2를 하는 이유는 화면의 중앙에서 화면의 크기의 반을 빼줘야 좌측 하단좌표를 구할 수 있기 때문이다.
        lineRenderer = Instantiate(map).GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, new Vector2(x, y) - mapSize / 2); //좌측 하단
        lineRenderer.SetPosition(1, new Vector2(x + mapSize.x, y) - mapSize / 2); //우측 하단
        lineRenderer.SetPosition(2, new Vector2(x + mapSize.x, y + mapSize.y) - mapSize / 2);//우측 상단
        lineRenderer.SetPosition(3, new Vector2(x, y + mapSize.y) - mapSize / 2); //좌측 상단

        lineRenderers.Add(lineRenderer);
    }


    void Divide(Node tree, int n)
    {
        if (n == maximumDepth) return; // 최대높이에 도달하면 더 나누지 않는다.
                                       //아니면

        //가로와 세로중 더 긴것을 구하고, 가로가 길면 위쪽의 왼쪽,오른쪽, 세로가 길면 위,아래로 나눠준다.
        int maxLength = Mathf.Max(tree.nodeRect.width, tree.nodeRect.height);
        //최소길이와 최대 길이 사이에서 랜덤으로 값 구하기
        int split = Mathf.RoundToInt(Random.Range(maxLength * minimumDevidedRate, maxLength * maximumDevidedRate));

        //가로가 더 길면 좌 우로 나눔, 세로의 길이는 그대로다.
        if (tree.nodeRect.width >= tree.nodeRect.height)
        {   //왼쪽 노드의 정보, 위치는 좌측 하단 기준이라 변하지않고, 가로 길이는 위에서 구한 랜덤값을 넣어준다.
            tree.leftNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y, split, tree.nodeRect.height));
            //오른쪽 노드의 정보, 위치는 좌측 하단에서 오른쪽으로 가로 길이만큼 이동한 위치,
            //가로길이는 기존 가로길이 - 새로 구한 가로길이
            tree.rightNode = new Node(new RectInt(tree.nodeRect.x + split, tree.nodeRect.y,
                                                  tree.nodeRect.width - split, tree.nodeRect.height));
            //위 두개의 노드를 나눠준 선을 그리는 함수
            DrawLine(new Vector2(tree.nodeRect.x + split, tree.nodeRect.y),
                     new Vector2(tree.nodeRect.x + split, tree.nodeRect.y + tree.nodeRect.height));
        }

        else //세로가 더 길다면,
        {    //위의 식을 y버전으로 바꿔주기만 하면 된다.
            tree.leftNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y, tree.nodeRect.width, split));
            tree.rightNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y + split,
                                                 tree.nodeRect.width, tree.nodeRect.height - split));
            DrawLine(new Vector2(tree.nodeRect.x, tree.nodeRect.y + split),
                     new Vector2(tree.nodeRect.x + tree.nodeRect.width, tree.nodeRect.y + split));
        }
        tree.leftNode.parNode = tree; // 자식노드들의 부모노드를 나누기전 노드로 설정한다.
        tree.rightNode.parNode = tree;

        Divide(tree.leftNode, n + 1); //왼쪽,오른쪽 자식 노드들도 나눠주기
        Divide(tree.rightNode, n + 1); //n+1해서 최대 높이에 도달시 종료


    }

    private void DrawLine(Vector2 from, Vector2 to)
    {
        lineRenderer = Instantiate(line).GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, from - mapSize / 2);
        lineRenderer.SetPosition(1, to - mapSize / 2);


        lineRenderers.Add(lineRenderer);
    }

    private RectInt GenerateRoom(Node tree, int n)
    {
        RectInt rect;

        if (n == maximumDepth) // 나뉘어진 구역이면 방생성
        {
            rect = tree.nodeRect; 
            int width = Random.Range(rect.width / 2 ,rect.width - 1);
            int height = Random.Range(1, rect.width - width);
            int x = rect.x + Random.Range(1, rect.width - width);
            int y = rect.y + Random.Range(1, rect.height - height);
            rect = new RectInt(x, y, width, height);
            DrawRectangle(rect);
        }
        else
        {
            tree.leftNode.roomRect = GenerateRoom(tree.leftNode, n + 1);
            tree.rightNode.roomRect = GenerateRoom(tree.rightNode, n + 1);
            rect = tree.leftNode.roomRect;
        }
        return rect;

    }

    private void DrawRectangle(RectInt rect)
    {
        LineRenderer lineRenderer = Instantiate(roomLine).GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, new Vector2(rect.x, rect.y) - mapSize / 2); //좌측 하단
        lineRenderer.SetPosition(1, new Vector2(rect.x + rect.width, rect.y) - mapSize / 2); //우측 하단
        lineRenderer.SetPosition(2, new Vector2(rect.x + rect.width, rect.y + rect.height) - mapSize / 2);//우측 상단
        lineRenderer.SetPosition(3, new Vector2(rect.x, rect.y + rect.height) - mapSize / 2); //좌측 상단

        lineRenderers.Add(lineRenderer);
    }


    public void ReDrow()
    {
        foreach (LineRenderer lineRenderer in lineRenderers)
        {
            if (lineRenderer != null)
            {
                Destroy(lineRenderer.gameObject);
            }
        }
        lineRenderers.Clear();
        Start();
    }




}