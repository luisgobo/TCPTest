using TcpTestLN.Enums;

//ESTO SE VA A MOVER A LA LOGICA DE NEGOCIOS

namespace TcpTestLN.Generic
{
    public class EventPackage<T> where T : class
    {
        public string ClientId { get; set; }
        public ActionTypes ActionType { get; set; }
        public T GenericInstance { get; set; }
    }
}