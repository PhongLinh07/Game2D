using System;

//
// SingletonBase: lớp generic base để tạo Singleton cho mọi class kế thừa.
// T : class kế thừa SingletonBase<T>
//

public abstract class SingletonBase<T> : ObjectBase where T : SingletonBase<T>, new()
{
    // Instance duy nhất của Singleton
    private static T sInstance;

    // Lock object để đảm bảo thread-safe khi tạo instance
    private static readonly object sysLock = new();

    /// <summary>
    /// Lấy instance (tạo mới nếu chưa có).
    /// </summary>
    public static T GetInstance
    {
        get
        {
            if(sInstance == null)
            {
                lock (sysLock)
                {
                    if(sInstance == null)
                    {
                        sInstance = new T();
                        sInstance.onCreate();
                    }
                }
            }
            return sInstance;
        }
    }

    /// <summary>
    /// Thử lấy instance (trả về null nếu chưa tạo).
    /// </summary>
    public static T TryGetInstance()
    {
        return sInstance;
    }

    /// <summary>
    /// Hủy instance hiện tại (reset singleton).
    /// </summary>
    public static void DoDestroyInstance()
    {
        if (sInstance != null)
        {
            sInstance.OnDestory();
            sInstance = null;
        }
    }

    // <summary>
    /// Gọi khi tạo instance (có thể override).
    /// </summary>
    protected virtual void onCreate()
    {
    }

    /// <summary>
    /// Gọi khi destroy instance (có thể override).
    /// </summary>
    protected virtual void OnDestory()
    {
    }
}
