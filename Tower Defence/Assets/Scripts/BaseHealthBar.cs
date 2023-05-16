using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class BaseHealthBar : MonoBehaviour
    {
        [SerializeField] protected Image image;

        // Start is called before the first frame update
        public virtual void UpdateHealthBar(float currentValue, float maxValue)
        {
            if(image != null)
                image.fillAmount = currentValue / maxValue;
        }

    }
}