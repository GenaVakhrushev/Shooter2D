using Shooter.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.HP
{
    public class HPView : View
    {
        [SerializeField] private Image hpImage;
        [SerializeField] private Color fullHPColor = Color.green;
        [SerializeField] private Color noHPColor = Color.red;
        
        private HPModel model;

        private void Update()
        {
            transform.rotation = Quaternion.identity;
        }

        public void SetModel(HPModel newModel)
        {
            if (model != null)
            {
                model.HpChanged -= ModelOnHPChanged;
            }
            
            model = newModel;
            
            if (model != null)
            {
                UpdateHPImage();
                model.HpChanged += ModelOnHPChanged;
            }
        }

        private void ModelOnHPChanged(float hp)
        {
            UpdateHPImage();
        }

        private void UpdateHPImage()
        {
            var hpPercent = model.CurrentHP / model.MaxHP;
            hpImage.fillAmount = hpPercent;
            hpImage.color = Color.Lerp(noHPColor, fullHPColor, hpPercent);
        }
    }
}