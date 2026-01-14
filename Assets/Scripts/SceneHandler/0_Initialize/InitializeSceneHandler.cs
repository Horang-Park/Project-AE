namespace SceneHandler._0_Initialize
{
    public class InitializeSceneHandler : BaseSceneHandler
    {
        private void Awake()
        {
            Networking.TheBackend.Initializer.BackendInitialize();
        }
    }
}
