using System.Linq;
using Fusion;
using UnityEngine;

namespace Born.FusionTest
{
    public class PlayerColor : NetworkBehaviour
    {
        [SerializeField] private Color[] playerColors;
        private Material mat;
        private Color targetColor;
        private float lerpSpeed = 2f;

        //https://doc.photonengine.com/en-us/fusion/current/manual/network-object/network-behaviour#allowed_types
        [Networked(OnChanged = nameof(OnColorChanged))] int colorIndex { get; set; }

        public override void Spawned()
        {
            mat = GetComponentInChildren<Renderer>().material;
            colorIndex = Runner.ActivePlayers.Count() - 1; //Trigers Property OnChange for other players
            SetColor();
        }

        private void Update()
        {
            if(Object == null) return;
            if (!Object.HasStateAuthority) return;
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                colorIndex++;
                SetColor();
            }
        }

        public override void Render() //prefered fusion loop for visuals
        {
            if (mat == null) return;
            if(mat.color == targetColor) return;
        
            if (Object.HasInputAuthority)
                mat.color = targetColor;
            else
                mat.color = Color.Lerp(mat.color, targetColor, lerpSpeed* Time.deltaTime);
        }

        public static void OnColorChanged(Changed<PlayerColor> changed) => changed.Behaviour.SetColor();

        //Updates Color Internally
        private void SetColor() 
        {
            colorIndex = (colorIndex >=  playerColors.Length) ? 0 : colorIndex;
            targetColor = playerColors[colorIndex];
        }
    }
}