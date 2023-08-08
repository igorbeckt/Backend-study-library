using System.Runtime.Serialization;

namespace RentCarSys.Enums
{
    public enum ClienteStatus
    {
        [EnumMember(Value = "Online")]
        Online = 1,
        [EnumMember(Value = "Running")]
        Running = 1,
        [EnumMember(Value = "Offline")]
        Offline = 1,
    }
}
