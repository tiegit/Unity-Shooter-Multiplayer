using Colyseus.Schema;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyCharacter _enemy;

    private readonly Stopwatch _stopwatch = new Stopwatch();
    private float tactTime;

    internal void OnChange(List<DataChange> changes)
    {
        Vector3 position = transform.position;

        _stopwatch.Start();

        foreach (var dataChange in changes)
        {
            switch (dataChange.Field)
            {
                case "x":
                    position.x = (float)dataChange.Value;
                    break;
                case "y":
                    position.z = (float)dataChange.Value;
                    break;
                default:
                    UnityEngine.Debug.LogWarning("Не обрабатывается изменение поля " + dataChange.Field);
                    break;
            }  
        }
         _stopwatch.Stop();

        tactTime = (float)_stopwatch.Elapsed.TotalSeconds;        
        
        _enemy.SetMoveData(position, tactTime);
    }
}
