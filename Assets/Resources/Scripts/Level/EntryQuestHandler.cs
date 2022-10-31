using System.Collections.Generic;
using PathGame.Data;
using PathGame.Interface;
using PathGame.Service.Quest;
using UnityEngine;
using Zenject;

public class EntryQuestHandler : MonoBehaviour
{
    private IEntryPoint _entryPoint;
    private IQuestService _questService;
    private bool _complete;
    private Quest[] _quests;
    
    [Inject]
    private void Construct(IQuestService questService)
    {
        _questService = questService;
    }
    private void Start()
    {
        _entryPoint = GetComponent<IEntryPoint>();
        _entryPoint.pathUpdate += Quest;
    }
    
    private void Quest(List<ListEntryPoint> path, bool isPathFind)
    {
        if (_quests != null && _quests.Length > 0)
            foreach (var quest in _quests)
                quest.Down();
        
        if (path.Count > 0 && !isPathFind)
        {
            _quests = _questService.GetQuest<EntryQuestHandler>();
            foreach (var quest in _quests) 
                quest.Up();
            
        }
    }
}
