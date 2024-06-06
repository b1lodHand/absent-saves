

namespace com.absence.savesystem
{
    [System.Serializable]
    public class GameSaveData
    {
        #region Singleton
        private static GameSaveData m_current;
        public static GameSaveData Current
        {
            get
            {
                if (m_current == null) Reset();
                return m_current;
            }

            internal set { m_current = value; }
        }

        public static void Reset()
        {
            m_current = new GameSaveData();
        }
        #endregion

        /* DATA */
        public int ID1;
        public int ID2;
    }

}