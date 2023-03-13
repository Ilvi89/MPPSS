using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelMaker : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject pathPrefab;
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject endPrefab;
    private LevelManager _levelManager;
    private UIPlayer _uiPlayer;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private ShipPlaneImg shipPlane;
    [SerializeField] private GetSideByVector getSideByVector;
    [SerializeField] private GameObject pointer;
    [SerializeField] private Window_QuestPointer _windowQuestPointer;


    private void Start()
    {
        _levelManager = LevelManager.Instance;
        _uiPlayer = FindObjectOfType<UIPlayer>();
        pointer.transform.position = _levelManager.lvlData.endPosition;
        

        foreach (var enemy in _levelManager.lvlData.enemies)
        {
            var ship = Instantiate(enemyPrefab, enemy.pathPoints[0], enemy.shipRotation).GetComponent<Ship>();
            var path = Instantiate(pathPrefab).GetComponent<Path>();

            ship.onCrash.AddListener(() =>
            {
                Debug.Log("crash");
                gameManager.EndGame();
            });
            ship.GetComponent<ShipMouseHandler>().onClick
                .AddListener(() =>
                {
                    Debug.Log(ship.ShipData.ShipType);
                    shipPlane.ChangeShip(ship);
                });


            foreach (var point in enemy.pathPoints)
            {
                var p = Instantiate(pointPrefab, point, quaternion.identity, path.transform);
            }

            path.pathType = PathType.Path;
            ship.shipMoveSpeed = enemy.shipMoveSpeed;
            ship.SetPath(path);
            ship.SetShipData(_levelManager.GetShipData(enemy.shipType));
            
        }


        var player = Instantiate(playerPrefab, _levelManager.lvlData.playerPosition, Quaternion.identity);
        var end = Instantiate(endPrefab, _levelManager.lvlData.endPosition, Quaternion.identity);
        
        
        var mc = player.GetComponent<MovementController>();
        _uiPlayer.movementController = mc;
        end.GetComponent<Endpoint>().windowQuestPointer = _windowQuestPointer;
        end.GetComponent<Endpoint>().onPlayerEnter.AddListener(delegate
        {
            gameManager.EndGame();
        });
        getSideByVector.player = mc.transform;

        Camera.main.GetComponent<SmoothFollow>().SetTarget(player.gameObject);
    }

    
}