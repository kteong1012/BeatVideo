
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;


namespace Game.Cfg
{
public partial struct vector3
{
    public vector3(ByteBuf _buf) 
    {
        X = _buf.ReadFloat();
        Y = _buf.ReadFloat();
        Z = _buf.ReadFloat();
    }

    public static vector3 Deserializevector3(ByteBuf _buf)
    {
        return new vector3(_buf);
    }

    public readonly float X;
    public readonly float Y;
    public readonly float Z;
   

    public  void ResolveRef(Tables tables)
    {
        
        
        
    }

    public override string ToString()
    {
        return "{ "
        + "x:" + X + ","
        + "y:" + Y + ","
        + "z:" + Z + ","
        + "}";
    }
}

}
