using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameInterfaces;

namespace GameInterfaces {
     public interface Damageable
     {
          void ReceiveDamage(int damageAmount,Vector3 attackerPosition);

     }

}
