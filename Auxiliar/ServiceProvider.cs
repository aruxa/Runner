namespace runner
{
    public sealed class Provider
    {
        private Provider()
        {
        }

        public static Provider Instance { get { return Singleton.SingletonInstance; } }

        class Singleton
        {
            static Singleton()
            {
                
            }

            internal static readonly Provider SingletonInstance = new Provider(); 
        }
    }
}
