namespace Console3D
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Debug.Log("Start");
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
            finally
            {
                Debug.Log("End");
            }
        }
    }

}

// TASKS:
// Debug
// Console Window
// Translate BitArray
// FPS
// Loop
// Loop Events