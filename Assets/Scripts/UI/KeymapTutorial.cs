using UnityEngine;

namespace Spark.FusionTest
{
    public class KeymapTutorial : MonoBehaviour
    {
        private void OnGUI()
        {
            string instructions = "Arrows to move\n Space Bar: Color\n 1 to Switch Authority";
            Vector2 lableSize = new GUIStyle().CalcSize(new GUIContent(instructions));
            Rect r = new Rect(10, Screen.height - 70, lableSize.x * 1.1f, lableSize.y * 1.2f);
            GUI.contentColor = Color.white;
            GUI.Box(r, instructions);
        }
    }
}