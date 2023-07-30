using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _player;
    [SerializeField] private const float OutFPSModifier = 0.05f;

    private void Start()
    {
        InvokeRepeating("SendMove", 0, OutFPSModifier); //Отправка координат на сервер 20 раз/сек не грузит сервер, дает имитацию подвисания
    }
    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        _player.SetInput(h, v);
        //SendMove(); // Слишком часто отправляет сообщения на сервер >60 раз/сек
    }
    
    private void SendMove()
    { 
        _player.GetMoveInfo(out Vector3 position);
        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            { "x", position.x },
            { "y", position.z }
        };

        MultiplayerManager.Instance.SendMessage("move", data); // Все модификации выше для контроля этого вызова
    }
}
