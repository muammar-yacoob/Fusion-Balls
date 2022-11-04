using Fusion;
using TMPro;
using UnityEngine;

namespace Born.FusionTest.Player
{
    public class PlayerNickname : NetworkBehaviour
    {
        [SerializeField] private TMP_Text nickNameUI;

        [Networked(OnChanged = nameof(OnNicknameChanged))]
        string nickName { get; set; }

        public override void Spawned()
        {
            SetNick();
        }

        private void Update()
        {
            if (Object == null) return;
            if (!Object.HasStateAuthority) return;

            if (Input.GetKeyDown(KeyCode.C))
            {
                FetchPlayers();
            }
        }

        private void FetchPlayers()
        {
            foreach (var player in Runner.ActivePlayers)
            {
            }
        }

        public static void OnNicknameChanged(Changed<PlayerNickname> changed) => changed.Behaviour.SetNick();

        //Updates Color Internally
        private void SetNick()
        {
        }
    }
}