using System;

public class Singleton<T> where T : Singleton<T> {
    private static T m_Instance;
    private static object m_PadLock = new object();
    public static T instance {
        get {
            lock (m_PadLock) {
                if (m_Instance != null)
                    return m_Instance;
                m_Instance = (T)typeof(T).GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
                return m_Instance;
            }
        }
    }
    protected Singleton() {
    }
}
