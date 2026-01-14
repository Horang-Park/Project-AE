using BackEnd;
using Horang.HorangUnityLibrary.Utilities;

namespace Networking.TheBackend
{
    public static class Initializer
    {
        public static void BackendInitialize()
        {
            var bro = Backend.Initialize();

            if(bro.IsSuccess())
            {
                Log.Print($"뒤끝 초기화 완료 -> {bro}");
            }
            else
            {
                Log.Print($"뒤끝 초기화 실패 -> {bro}", LogPriority.Error);
            }
        }
    }
}
