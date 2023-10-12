using DG.Tweening;
using UnityEngine;

namespace CardGame
{
    public class PlayArea : MonoBehaviour
    {
        [SerializeField] private Transform[] _cardsTargets;
        private int _firstAvailableTarget = 0;
        
        public void PlayCard(Transform card)
        {
            if(_firstAvailableTarget < 0 || _firstAvailableTarget >= _cardsTargets.Length) return;

            var target = _cardsTargets[_firstAvailableTarget];
            
            card.parent = target;
            
            card.DOLocalMove(Vector3.zero, 0.2f);
            card.DOLocalRotate(Vector3.zero, 0.2f);
            ((RectTransform)card).DOSizeDelta(((RectTransform)target).sizeDelta, 0.2f);
            
            _firstAvailableTarget++;
        }

        public void CollectCards(Transform player)
        {
            player.DOScale(1.2f, 0.2f).SetLoops(2, LoopType.Yoyo).SetDelay(0.25f);
            for (int i = 0; i < _cardsTargets.Length; i++)
            {
                if (_cardsTargets[i].childCount > 0)
                {
                    var card = _cardsTargets[i].GetChild(0);
                    card.DOMove(player.position, 0.25f);
                    card.DOScale(0, 0.25f).OnComplete(()=>Destroy(card.gameObject));
                }
            }
            _firstAvailableTarget = 0;
        }
    }
}
