using Unity.Mathematics;
using UnityEngine;

public class LevelMaker : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject pathPrefab;
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private GameObject playerPrefab;
    private LevelManager _levelManager;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private ShipPlaneImg shipPlane;


    private void Start()
    {
        _levelManager = LevelManager.Instance;

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
                    Debug.Log("plane");
                    shipPlane.ChangeShip(ship);
                });


            foreach (var point in enemy.pathPoints)
            {
                var p = Instantiate(pointPrefab, point, quaternion.identity, path.transform);
            }

            path.pathType = PathType.Path;
            ship.ShipData.SetMoveSpeed(enemy.shipMoveSpeed);
            ship.SetPath(path);
            ship.SetShipData(enemy.shipData);
        }
    }
}