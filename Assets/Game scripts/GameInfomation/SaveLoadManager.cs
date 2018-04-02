using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace RpgTools.Save{
    public static class SaveLoadManager {

        public static void SavePlayerInfo(PlayerClass.Player player)
        {
            //saving player, opening binary formatter
            BinaryFormatter bf = new BinaryFormatter();
            //creating datapath and file
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);


        }

        [Serializable]
        public class PlayerData {

            public int[] stats;
            public float[] _stats;

            public PlayerData(PlayerClass.Player player)
            {
                /* stats = new int[8];
                 stats[0] = player.Level;
                 stats[1] = player.health;
                 stats[2] = player.AutoAttackDamage;
                 stats[3] = player.Experience;
                 stats[4] = player.Expfunction;
                 stats[5] = player._characterIndex;
                 stats[6] = player.Level;

                 _stats = new float[2];
                 //_stats[0] = player.usermovement.runSpeed;
                 _stats[1] = player.autoAttackCD;

         */
            }
        }
    }
}