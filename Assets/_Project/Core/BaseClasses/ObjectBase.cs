//
// ObjectBase: base class hỗ trợ override các toán tử ==, != và bool.
// Dùng để các class kế thừa có thể so sánh và check null an toàn.
//
public class ObjectBase
{
    /// <summary>
    /// Cho phép dùng cú pháp "if(obj)" thay vì "if(obj != null)".
    /// </summary>

    public static implicit operator bool(ObjectBase obj)
    {
        return obj != null;
    }

    /// <summary>
    /// Toán tử so sánh == (override để kiểm soát null check).
    /// </summary>
    public static bool operator ==(ObjectBase obja, ObjectBase objb)
    {
        if( ReferenceEquals(obja, objb) ) return true;
        if((object)obja == null  || (object)objb == null) return false;

        return obja.Equals(objb);
    }

    /// <summary>
    /// Toán tử so sánh !=
    /// </summary>
    public static bool operator !=(ObjectBase obja, ObjectBase objb)
    {
        return !(obja == objb);
    }

    /// <summary>
    /// HashCode mặc định theo object.
    /// </summary>
    public override int GetHashCode()
    {
        return base.GetHashCode(); //GetHashCode() là một hàm đặc biệt trong .NET, nó trả về một số nguyên (hash code) đại diện cho object.
    }

    /// <summary>
    /// So sánh object với nhau.
    /// </summary>
    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    protected ObjectBase()
    {
    }
}
