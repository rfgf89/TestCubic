
namespace PathGame.Service.Scene
{
    public interface ISceneService
    {
        public void ToScene(SceneServiceData sceneServiceEvent);
        public void ToScene(SceneServiceState sceneServiceState);
        public SceneServiceData GetLastSceneData();
    }
}