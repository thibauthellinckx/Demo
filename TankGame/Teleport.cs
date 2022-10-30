using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
public class Teleport : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
        {
            TeleportOnWall(other);
        }

        private void TeleportOnWall(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                Vector3 teleport = other.GetComponent<WallTP>().teleportValue;
                transform.position = transform.position + teleport;
            }
        }
    }

}