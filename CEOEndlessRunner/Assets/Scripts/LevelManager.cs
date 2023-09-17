using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance => s_Instance;
    static LevelManager s_Instance;

    [SerializeField] List<GameObject> terrainPieces;
    [SerializeField] GameObject terrainPiecePrefab;
    [SerializeField] GameObject[] obstacles;
    [SerializeField] GameObject[] collectibles;
    [SerializeField] GameObject[] targets;

    private float speed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;

    float traveledDistance = 0;

    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            bool spawnObjects = true;

            if (i < 3)
            {
                spawnObjects = false;
            }
            AddNewPiece(spawnObjects);
        }

        speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.playerState == GameManager.PlayerState.PLAYING)
        {
            if (traveledDistance >= 10)
            {
                traveledDistance = 0;
                AddNewPiece(true);
                RemovePiece();
            }

            traveledDistance += speed * Time.deltaTime;

            speed += Time.deltaTime * 0.25f;

            PlayerController.Instance.playerAnimator.SetFloat("Velocity", Mathf.InverseLerp(walkSpeed, runSpeed, speed));

            GameManager.Instance.AddScore(speed * Time.deltaTime);
        }

    }

    private void LateUpdate()
    {
        if (GameManager.Instance.playerState == GameManager.PlayerState.PLAYING)
        {
            foreach (GameObject terrainPiece in terrainPieces)
            {
                terrainPiece.transform.Translate(Vector3.forward * -Time.deltaTime * speed);
            }
        }
    }

    private void AddNewPiece(bool spawnObjects)
    {
        GameObject lastPiece = terrainPieces[terrainPieces.Count - 1];
        GameObject newPiece;

        if (lastPiece != null)
        {
            newPiece = Instantiate(terrainPiecePrefab, lastPiece.transform.position + Vector3.forward * 10, Quaternion.identity);
        }
        else
        {
            newPiece = Instantiate(terrainPiecePrefab, Vector3.zero + Vector3.forward * 10, Quaternion.identity);
        }

        if (spawnObjects)
        {
            SpawnObjects(newPiece);
        }

        terrainPieces.Add(newPiece);
    }

    private void RemovePiece()
    {
        GameObject piece = terrainPieces[0];
        terrainPieces.Remove(terrainPieces[0]);
        Destroy(piece);
    }

    private void SpawnObjects(GameObject terrainPiece)
    {
        float seed = Random.Range(1, 100);

        if (seed < 50)
        {
            GameObject collectible = collectibles[(int)Random.Range(0, collectibles.Length)];

            Instantiate(collectible, terrainPiece.transform.position + Vector3.up * 1.75f, Quaternion.identity, terrainPiece.transform);
            //Instantiate(collectible, terrainPiece.transform.position + Vector3.up * 1.75f + Vector3.right * 2f, Quaternion.identity, terrainPiece.transform);
            //Instantiate(collectible, terrainPiece.transform.position + Vector3.up * 1.75f + Vector3.right * -2f, Quaternion.identity, terrainPiece.transform);
        }
        else if (seed < 80)
        {
            int randomCase = Random.Range(0, 3);

            GameObject obstacle = obstacles[(int)Random.Range(0, obstacles.Length )];

            if (randomCase == 0)
            {
                Instantiate(obstacle, terrainPiece.transform.position + Vector3.up * 1f, Quaternion.identity, terrainPiece.transform);
                Instantiate(obstacle, terrainPiece.transform.position + Vector3.up * 1f + Vector3.right * 3.25f, Quaternion.identity, terrainPiece.transform);
            }
            else if (randomCase == 1)
            {
                Instantiate(obstacle, terrainPiece.transform.position + Vector3.up * 1f, Quaternion.identity, terrainPiece.transform);
                Instantiate(obstacle, terrainPiece.transform.position + Vector3.up * 1f + Vector3.right * -3.25f, Quaternion.identity, terrainPiece.transform);
            }
            else if (randomCase == 2)
            {
                Instantiate(obstacle, terrainPiece.transform.position + Vector3.up * 1f + Vector3.right * -3.25f, Quaternion.identity, terrainPiece.transform);
                Instantiate(obstacle, terrainPiece.transform.position + Vector3.up * 1f + Vector3.right * 3.25f, Quaternion.identity, terrainPiece.transform);
            }
        }
        else
        {

        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public float GetSpeed()
    {
        return speed;
    }
}
