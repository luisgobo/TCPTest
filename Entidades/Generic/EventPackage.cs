using Entidades.Enums;

namespace Entidades.Generic
{
    public class EventPackage<T> where T : class
    {
        public string ClientId { get; set; }
        public ActionTypes ActionType { get; set; }
        public T GenericInstance { get; set; }        

    }

}

