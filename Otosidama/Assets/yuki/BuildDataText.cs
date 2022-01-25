using UnityEngine;
using UnityEngine.UI;

namespace MyProject
{
    public class BuildDataText : MonoBehaviour
    {
        [SerializeField] private Text _BuildDateText;

        private void Start() => _BuildDateText.text = $"ver.{Resources.Load<TextAsset>("BuildDate").text}";
    }
}