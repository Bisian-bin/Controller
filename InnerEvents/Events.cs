using Microsoft.Practices.Prism.Events;
using System;
using System.Threading.Tasks;

namespace InnerEvents
{
    public class SECSGEMEvent : CompositePresentationEvent<object> { }

    public class MainEvent : CompositePresentationEvent<object> { }

    public class UserEvent : CompositePresentationEvent<object> { }
}
