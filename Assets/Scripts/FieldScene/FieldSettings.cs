namespace Assets.Scripts.FieldScene
{
    public class FieldSettings
    {
        private static FieldSettings instance;
        public static FieldSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FieldSettings();
                }
                return instance;
            }
        }

        private FieldSettings()
        {
        }

        public int SizeX
        {
            get { return 5; }
        }

        public int SizeY
        {
            get { return 5; }
        }

    }
}