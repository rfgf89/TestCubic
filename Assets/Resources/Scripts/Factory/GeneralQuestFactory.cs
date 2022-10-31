using PathGame.Interface;
using PathGame.Level;
using PathGame.Service.Quest;
using UnityEngine;
using Zenject;

namespace PathGame.Factory
{
    public class GeneralQuestFactory : MonoBehaviour, IQuestFactory
    {
        private readonly DiContainer _diContainer;
        [SerializeField] private QuestView _counerView;
        
        public GeneralQuestFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public QuestView Create(QuestViewType type, Quest quest, Vector3 at)
        {
            switch (type)
            {
                case QuestViewType.Counter :
                    return _diContainer
                        .InstantiatePrefab(_counerView.gameObject, at, Quaternion.identity, null)
                        .GetComponent<QuestView>().Init(quest);
            }
            
            return null;
        }
        
    }
}
